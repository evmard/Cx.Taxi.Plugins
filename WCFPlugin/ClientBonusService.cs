using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using Cx.Client.Taxi.Clients.Data;
using Cx.Client.Utils.Server;
using WCFPlugin.Contract;
using WCFPlugin.CxPlugin;
using WCFPlugin.DataContract;
using WCFPlugin.Stub;

namespace WCFPlugin
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class ClientBonusService : IClientBonusService
    {
        private readonly Dictionary<Guid, SessionInfo> _sessions = new Dictionary<Guid, SessionInfo>();
        private readonly Dictionary<string, SessionInfo> _sessionsByLogin = new Dictionary<string, SessionInfo>();
        private readonly ReaderWriterLockSlim _sessionsLockSlim = new ReaderWriterLockSlim();

        private readonly Dictionary<long, OperationInfo> _payoutCodes = new Dictionary<long, OperationInfo>();
        private readonly Dictionary<long, OperationInfo> _payInCodes = new Dictionary<long, OperationInfo>();

        private readonly Random _rng = new Random(DateTime.Now.Millisecond);

        private static ICxDataProvider _dataProvider;
        private static readonly Object ProviderLock = new object();
        public static ICxDataProvider DataProvider
        {
            get
            {
                lock (ProviderLock)
                {
                    if (_dataProvider == null)
                    {
                        _dataProvider = new DataProviderStub();
                    }
                }

                return _dataProvider;
            }
            set
            {
                lock (ProviderLock)
                {
                    _dataProvider = value;
                }
            }
        }

        private static readonly Object ParamsLock = new object();
        private static PluginParams _params;
        public static PluginParams Params
        {
            get
            {
                lock (ParamsLock)
                {
                    if (_params == null)
                    {
                        _params = new PluginParams();
                    }
                }

                return _params;
            }
            set
            {
                lock (ParamsLock)
                {
                    _params = value;
                }

                DataProvider.PluginParams = Params;
            }
        }

        public Result<LoginInfo> Login(string login, string pass, RoleTypes roleType)
        {
            if (DataProvider == null)
                return Result<LoginInfo>.NotInitialized();

            ICxChildConnection connection;
            string errorMsg;
            var logonResult = DataProvider.TryGetConnection(login, pass, Params.GetRoleId(roleType), out connection, out errorMsg);
            if (logonResult != LogonResult.OK)
            {
                if (logonResult == LogonResult.AlreadyLogined)
                {
                    SessionInfo session;

                    _sessionsLockSlim.EnterReadLock();
                    var hasSession = _sessionsByLogin.TryGetValue(login, out session);
                    _sessionsLockSlim.ExitReadLock();

                    if (hasSession)
                    {
                        Logout(session.LoginInfo);
                        return Login(login, pass, roleType);
                    }
                }

                var result = Result<LoginInfo>.LoginOrPassIsWrong();
                if (!string.IsNullOrWhiteSpace(errorMsg))
                {
                    result.Message = errorMsg;
                }
                return result;
            }

            var loginInfo = new LoginInfo()
            {
                SessionGuid = Guid.NewGuid(),
                UserName = DataProvider.GetUserName(connection.IDUser),
                RoleType = roleType
            };

            var sessionInfo = new SessionInfo
            {
                LoginInfo = loginInfo,
                Connection = connection
            };

            switch (roleType)
            {
                case RoleTypes.Payin:
                    sessionInfo.CanDoPayIn = true;
                    sessionInfo.CanCreateNewClients = true;
                    sessionInfo.CanDoPayout = false;
                    break;
                case RoleTypes.Payout:
                    sessionInfo.CanDoPayIn = false;
                    sessionInfo.CanCreateNewClients = false;
                    sessionInfo.CanDoPayout = true;
                    break;
                default:
                    sessionInfo.CanDoPayIn = false;
                    sessionInfo.CanCreateNewClients = false;
                    sessionInfo.CanDoPayout = false;
                    break;
            }

            _sessionsLockSlim.EnterWriteLock();
            _sessions.Add(loginInfo.SessionGuid, sessionInfo);
            _sessionsByLogin.Add(sessionInfo.Connection.Login, sessionInfo);
            _sessionsLockSlim.ExitWriteLock();
            
            return new Result<LoginInfo>(loginInfo);
        }

        public void Logout(LoginInfo loginInfo)
        {
            var sessionInfo = GetSession(loginInfo);
            if (sessionInfo == null)
                return;

            sessionInfo.Connection.Drop();

            _sessionsLockSlim.EnterWriteLock();
            _sessions.Remove(loginInfo.SessionGuid);
            _sessionsByLogin.Remove(sessionInfo.Connection.Login);
            _sessionsLockSlim.ExitWriteLock();
        }

        public Result<ClientInfo> GetClientByPhone(string phone, LoginInfo loginInfo)
        {
            var result = CheckLoginAndDataProvider(loginInfo);
            if (!result.IsSucssied)
                return result;

            IClient client = DataProvider.GetClient(phone);
            if (client == null)
            {
                return new Result<ClientInfo>(false)
                {
                    Message = string.Format("Клиент с телефоном {0} не найден", phone),
                    ErrorType = ErrorTypes.ClientNotFound
                };
            }

            var clientInfo = new ClientInfo(client, phone);
            return new Result<ClientInfo>(clientInfo);
        }

        public Result<ClientInfo> CreateNewClient(string phone, string name, LoginInfo loginInfo)
        {
            var result = CheckLoginAndDataProvider(loginInfo);
            if (!result.IsSucssied)
                return result;

            var session = GetSession(loginInfo);
            if (!session.CanCreateNewClients)
                return Result<ClientInfo>.AccsessDeny();

            IClient client = DataProvider.CreateNewClient(phone, name);
            if (client == null)
            {
                return new Result<ClientInfo>(false);
            }

            var clientInfo = new ClientInfo(client, phone);
            return new Result<ClientInfo>(clientInfo);
        }

        public Result<ClientInfo> PayoutFromAccount(long clientId, int operationCode, LoginInfo loginInfo)
        {
            var result = CheckLoginAndDataProvider(loginInfo);
            if (!result.IsSucssied)
                return result;

            var session = GetSession(loginInfo);
            if (!session.CanDoPayout)
                return Result<ClientInfo>.AccsessDeny();

            OperationInfo operationInfo;
            lock (_payoutCodes)
            {
                if (!_payoutCodes.TryGetValue(clientId, out operationInfo) || operationInfo.Code != operationCode)
                {
                    var accsessDenyResult = Result<ClientInfo>.AccsessDeny();
                    accsessDenyResult.Message = Result.CodeErrorMsg;
                    return accsessDenyResult;
                }

                _payoutCodes.Remove(clientId);
            }
            ClientInfo clientInfo;
            if (!DataProvider.TryPayoutFromAccount(clientId, operationInfo.Summ, out clientInfo))
                return new Result<ClientInfo>(false);

            return new Result<ClientInfo>(clientInfo);
        }

        public Result<ClientInfo> PayInToAccount(long clientId, int operationCode, LoginInfo loginInfo)
        {
            var result = CheckLoginAndDataProvider(loginInfo);
            if (!result.IsSucssied)
                return result;

            var session = GetSession(loginInfo);
            if (!session.CanDoPayIn)
                return Result<ClientInfo>.AccsessDeny();

            OperationInfo operationInfo;
            lock (_payInCodes)
            {
                if (!_payInCodes.TryGetValue(clientId, out operationInfo) || operationInfo.Code != operationCode)
                {
                    var accsessDenyResult = Result<ClientInfo>.AccsessDeny();
                    accsessDenyResult.Message = Result.CodeErrorMsg;
                    return accsessDenyResult;
                }

                _payInCodes.Remove(clientId);
            }

            ClientInfo clientInfo;
            if (!DataProvider.TryPayInToAccount(clientId, operationInfo.Summ, out clientInfo))
                return new Result<ClientInfo>(false);

            return new Result<ClientInfo>(clientInfo);
        }

        public Result SendPayinCode(long clientId, string phone, double summ, SendMethod sendMethod, LoginInfo loginInfo)
        {
            var result = CheckLoginAndDataProvider(loginInfo);
            if (!result.IsSucssied)
                return result;

            var session = GetSession(loginInfo);
            if (!session.CanDoPayIn)
                return Result<Object>.AccsessDeny();

            OperationInfo info;
            bool hasCode;
            int code;
            lock (_payInCodes)
            {
                hasCode = _payInCodes.TryGetValue(clientId, out info);
            }

            if (hasCode && Math.Abs(info.Summ - summ) < 1.0)
            {
                code = info.Code;
            }
            else
            {
                code = _rng.Next(100, 1000);
                lock (_payInCodes)
                {
                    _payInCodes[clientId] = new OperationInfo { Code = code, Summ = summ };
                }
            }

            var codeMsg = string.Format(_params.PayInCodeNotification, code, summ);
            DataProvider.SendCode(phone, codeMsg, sendMethod);
            return new Result(true);
        }

        public Result SendPayoutCode(long clientId, string phone, double summ, SendMethod sendMethod, LoginInfo loginInfo)
        {
            var result = CheckLoginAndDataProvider(loginInfo);
            if (!result.IsSucssied)
                return result;

            var session = GetSession(loginInfo);
            if (!session.CanDoPayout)
                return Result<Object>.AccsessDeny();

            OperationInfo info;
            bool hasCode;
            int code;
            lock (_payoutCodes)
            {
                hasCode = _payoutCodes.TryGetValue(clientId, out info);
            }

            if (hasCode && Math.Abs(info.Summ - summ) < 1.0)
            {
                code = info.Code;
            }
            else
            {
                code = _rng.Next(100, 1000);
                lock (_payoutCodes)
                {
                    _payoutCodes[clientId] = new OperationInfo { Code = code, Summ = summ };
                }
            }

            var codeMsg = string.Format(_params.PayOutCodeNotification, code, summ);
            DataProvider.SendCode(phone, codeMsg, sendMethod);
            return new Result(true);
        }

        private SessionInfo GetSession(LoginInfo loginInfo)
        {
            SessionInfo sessionInfo;
            _sessionsLockSlim.EnterReadLock();
            bool hasSession = _sessions.TryGetValue(loginInfo.SessionGuid, out sessionInfo);
            _sessionsLockSlim.ExitReadLock();

            if (!hasSession)
                return null;

            return sessionInfo;
        }

        private Result<ClientInfo> CheckLoginAndDataProvider(LoginInfo loginInfo)
        {
            SessionInfo session = GetSession(loginInfo);
            if (session == null)
                return Result<ClientInfo>.DoesntLogined();

            if (DataProvider == null)
                return Result<ClientInfo>.NotInitialized();

            return new Result<ClientInfo>(true);
        }
    }

    internal class OperationInfo
    {
        public int Code { get; set; }
        public double Summ { get; set; }
    }
}
