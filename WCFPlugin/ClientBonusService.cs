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

        private static ILogonManager _logonManager;
        private static readonly Object LogonManagerLock = new object();
        public static ILogonManager LogonManager
        {
            get
            {
                lock (LogonManagerLock)
                {
                    if (_logonManager == null)
                    {
                        _logonManager = new LogonManager(AppDomain.CurrentDomain.BaseDirectory);
                    }
                }

                return _logonManager;
            }
            set
            {
                lock (LogonManagerLock)
                {
                    _logonManager = value;
                }
            }
        }

        public Result<LoginInfo> Login(string login, string pass, RoleTypes roleType)
        {
            SessionInfo sessionInfo;
            var logonResult = LogonManager.Login(login, pass, roleType, out sessionInfo);
            switch (logonResult)
            { 
                case LogonResult.Ok:
                    return new Result<LoginInfo>(sessionInfo.GetLoginInfo());
                case LogonResult.AlreadyLogined:
                    LogonManager.Logout(sessionInfo.Guid);
                    return Login(login, pass, roleType);
                case LogonResult.WrongLoginOrPass:
                    return new Result<LoginInfo>(Result.LoginOrPassIsWrong());
                case LogonResult.WrongRole:
                    return new Result<LoginInfo>(Result.AccsessDeny());
                default:
                    return new Result<LoginInfo>(false) { Message = "Ошибка: Не известный результат идентификации" };
            }
        }

        public void Logout(Guid guid)
        {
            LogonManager.Logout(guid);
        }

        public Result<ClientInfo> GetClientByPhone(string phone, Guid guid)
        {
            var result = CheckLoginAndDataProvider(guid, Operations.GetClientByPhone);
            if (!result.IsSucssied)
                return new Result<ClientInfo>(result);

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

        public Result<ClientInfo> CreateNewClient(string phone, string name, Guid guid)
        {
            var result = CheckLoginAndDataProvider(guid, Operations.CreateNewClient);
            if (!result.IsSucssied)
                return new Result<ClientInfo>(result);

            IClient client = DataProvider.CreateNewClient(phone, name);
            if (client == null)
            {
                return new Result<ClientInfo>(false);
            }

            var clientInfo = new ClientInfo(client, phone);
            return new Result<ClientInfo>(clientInfo);
        }

        public Result<ClientInfo> PayoutFromAccount(long clientId, int operationCode, Guid guid)
        {
            var result = CheckLoginAndDataProvider(guid, Operations.PayoutFromAccount);
            if (!result.IsSucssied)
                return new Result<ClientInfo>(result);

            OperationInfo operationInfo;
            lock (_payoutCodes)
            {
                if (!_payoutCodes.TryGetValue(clientId, out operationInfo) || operationInfo.Code != operationCode)
                {
                    var accsessDenyResult = Result.AccsessDeny();
                    accsessDenyResult.Message = Result.CodeErrorMsg;
                    return new Result<ClientInfo>(accsessDenyResult);
                }

                _payoutCodes.Remove(clientId);
            }
            ClientInfo clientInfo;
            if (!DataProvider.TryPayoutFromAccount(clientId, operationInfo.Summ, out clientInfo))
                return new Result<ClientInfo>(false);

            return new Result<ClientInfo>(clientInfo);
        }

        public Result<ClientInfo> PayInToAccount(long clientId, int operationCode, Guid guid)
        {
            var result = CheckLoginAndDataProvider(guid, Operations.PayInToAccount);
            if (!result.IsSucssied)
                return new Result<ClientInfo>(result);

            OperationInfo operationInfo;
            lock (_payInCodes)
            {
                if (!_payInCodes.TryGetValue(clientId, out operationInfo) || operationInfo.Code != operationCode)
                {
                    var accsessDenyResult = Result.AccsessDeny();
                    accsessDenyResult.Message = Result.CodeErrorMsg;
                    return new Result<ClientInfo>(accsessDenyResult);
                }

                _payInCodes.Remove(clientId);
            }

            ClientInfo clientInfo;
            if (!DataProvider.TryPayInToAccount(clientId, operationInfo.Summ, out clientInfo))
                return new Result<ClientInfo>(false);

            return new Result<ClientInfo>(clientInfo);
        }

        public Result SendPayinCode(long clientId, string phone, double summ, SendMethod sendMethod, Guid guid)
        {
            var result = CheckLoginAndDataProvider(guid, Operations.SendPayinCode);
            if (!result.IsSucssied)
                return new Result<ClientInfo>(result);

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

            var codeMsg = string.Format(Params.PayInCodeNotification, code, summ);
            DataProvider.SendCode(phone, codeMsg, sendMethod);
            return new Result(true);
        }

        public Result SendPayoutCode(long clientId, string phone, double summ, SendMethod sendMethod, Guid guid)
        {
            var result = CheckLoginAndDataProvider(guid, Operations.SendPayoutCode);
            if (!result.IsSucssied)
                return new Result<ClientInfo>(result);

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

            var codeMsg = string.Format(Params.PayOutCodeNotification, code, summ);
            DataProvider.SendCode(phone, codeMsg, sendMethod);
            return new Result(true);
        }

        public Result<IEnumerable<UserInfo>> GetUsersList(Guid sessionGuid)
        {
            var result = CheckLoginAndDataProvider(sessionGuid, Operations.GetUsersList);
            if (!result.IsSucssied)
                return new Result<IEnumerable<UserInfo>>(result);

            return new Result<IEnumerable<UserInfo>>(_logonManager.GetUsersList());
        }

        public Result CreateUser(UserParam user, Guid sessionGuid)
        {
            var result = CheckLoginAndDataProvider(sessionGuid, Operations.CreateUser);
            if (!result.IsSucssied)
                return result;

            if (_logonManager.AddNewLogin(user))
                return new Result(true);
            else
                return new Result(false) { Message = "Не удалось добавить нового пользователя"};
        }

        public Result<UserParam> GetUser(string userLogin, Guid sessionGuid)
        {
            var result = CheckLoginAndDataProvider(sessionGuid, Operations.GetUsersList);
            if (!result.IsSucssied)
                return new Result<UserParam>(result);

            var user = _logonManager.GetUserParam(userLogin);
            if (user != null)
                return new Result<UserParam>(user);
            else
                return new Result<UserParam>(false) { Message = "Пользователь не найден"};
        }

        public Result UpdateUser(UserParam user, Guid sessionGuid)
        {
            var result = CheckLoginAndDataProvider(sessionGuid, Operations.UpdateUser);
            if (!result.IsSucssied)
                return result;

            return new Result(_logonManager.EditLogin(user));
        }

        public Result RemoveUser(string userLogin, Guid sessionGuid)
        {
            var result = CheckLoginAndDataProvider(sessionGuid, Operations.RemoveUser);
            if (!result.IsSucssied)
                return result;

            return new Result(_logonManager.RemoveLogin(userLogin));
        }

        public Result ChangeAdminPass(string newPass, Guid sessionGuid)
        {
            var result = CheckLoginAndDataProvider(sessionGuid, Operations.ChangeAdminPass);
            if (!result.IsSucssied)
                return result;

            return new Result(_logonManager.ChangeAdminPass(newPass));
        }

        private Result CheckLoginAndDataProvider(Guid guid, Operations operation)
        {
            if (DataProvider == null)
                return Result.NotInitialized();

            var permitionResult = LogonManager.CheckPermission(guid, operation);

            switch (permitionResult)
            { 
                case PermissionResult.Ok:
                    return new Result(true);
                case PermissionResult.AccessDeny:
                    return Result.AccsessDeny();
                case PermissionResult.DoesntLogined:
                    return Result.DoesntLogined();
                default:
                    var message = string.Format("Не известный результат операции проверки доступа Result = {0}", permitionResult);
                    return new Result(false) { Message = message };
            }
        }
    }

    internal class OperationInfo
    {
        public int Code { get; set; }
        public double Summ { get; set; }
    }
}
