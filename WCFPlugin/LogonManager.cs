using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using WCFPlugin.DataContract;

namespace WCFPlugin
{
    public class LogonManager: ILogonManager
    {
        private readonly Dictionary<Guid, SessionInfo> _sessions = new Dictionary<Guid, SessionInfo>();
        private readonly Dictionary<string, SessionInfo> _sessionsByLogin = new Dictionary<string, SessionInfo>();
        private readonly ReaderWriterLockSlim _sessionsLockSlim = new ReaderWriterLockSlim();

        private static MD5 _md5Hash = MD5.Create();

        private FileSystemWatcher _watcher;

        private readonly string _basePath;

        private string _adminPass;

        private readonly ReaderWriterLockSlim _usersLock = new ReaderWriterLockSlim();
        private Dictionary<string, UserParam> _users;

        private UsersProvider _userProvider;

        public LogonManager(string path)
        {
            _basePath = path;
            _watcher = new FileSystemWatcher(_basePath, UsersProvider.FileName);
            _watcher.Changed += _watcher_Changed;
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.EnableRaisingEvents = true;
            _users = new Dictionary<string, UserParam>();
            LoadUsers();
        }

        void _watcher_Changed(object sender, FileSystemEventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            _userProvider = UsersProvider.LoadLogins(_basePath);
            _adminPass = _userProvider.AdminPass;
            _usersLock.EnterWriteLock();
            _users.Clear();
            foreach (var item in _userProvider.Logins)
            {
                _users.Add(item.Login, item);
            }
            _usersLock.ExitWriteLock();
        }

        public LogonResult Login(string login, string pass, DataContract.RoleTypes role, out SessionInfo sessionInfo)
        {
            _sessionsLockSlim.EnterReadLock();
            bool logined = _sessionsByLogin.TryGetValue(login, out sessionInfo);
            _sessionsLockSlim.ExitReadLock();

            if (logined)
                return LogonResult.AlreadyLogined;

            UserParam loginParam;

            if ("admin".Equals(login))
            {
                if (!VerifyMd5Hash(pass, _adminPass))
                    return LogonResult.WrongLoginOrPass;
                else
                {
                    loginParam = new UserParam
                    {
                        Login = login,
                        Password = _adminPass,
                        Rate = 1,
                        Roles = new List<RoleTypes> { RoleTypes.Admin },
                        UserName = "Администратор"
                    };

                    sessionInfo = new SessionInfo(loginParam, Guid.NewGuid(), RoleTypes.Admin);
                }
            }
            else
            {
                _usersLock.EnterReadLock();
                bool hasUser = _users.TryGetValue(login, out loginParam);
                _usersLock.ExitReadLock();

                if (!hasUser || !VerifyMd5Hash(pass, loginParam.Password))
                    return LogonResult.WrongLoginOrPass;

                if (!loginParam.Roles.Contains(role))
                    return LogonResult.WrongRole;

                sessionInfo = new SessionInfo(loginParam, Guid.NewGuid(), role);
            }

            _sessionsLockSlim.EnterWriteLock();
            _sessions.Add(sessionInfo.Guid, sessionInfo);
            _sessionsByLogin.Add(login, sessionInfo);
            _sessionsLockSlim.ExitWriteLock();
            return LogonResult.Ok;
        }

        public void Logout(Guid sessionGuid)
        {
            _sessionsLockSlim.EnterWriteLock();
            SessionInfo session;
            if (_sessions.TryGetValue(sessionGuid, out session))
            {
                _sessions.Remove(sessionGuid);
                _sessionsByLogin.Remove(session.LoginParam.Login);
            }
            _sessionsLockSlim.ExitWriteLock();
        }

        public bool EditLogin(UserParam loginInfo)
        {
            if (string.IsNullOrWhiteSpace(loginInfo.Login))
                return false;

            _usersLock.EnterUpgradeableReadLock();
            if (!_users.ContainsKey(loginInfo.Login))
            {
                _usersLock.ExitUpgradeableReadLock();
                return false;
            }

            _usersLock.EnterWriteLock();
            _users[loginInfo.Login] = loginInfo;
            _usersLock.ExitWriteLock();
            _usersLock.ExitUpgradeableReadLock();

            try
            {
                _userProvider.Logins.RemoveAll(item => item.Login.Equals(loginInfo.Login));
                _userProvider.Logins.Add(loginInfo);
                _watcher.EnableRaisingEvents = false;
                _userProvider.Save(_basePath);
                _watcher.EnableRaisingEvents = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddNewLogin(UserParam loginInfo)
        {
            if (string.IsNullOrWhiteSpace(loginInfo.Login))
                return false;

            _usersLock.EnterUpgradeableReadLock();
            if (_users.ContainsKey(loginInfo.Login))
            {
                _usersLock.ExitUpgradeableReadLock();
                return false;
            }

            _usersLock.EnterWriteLock();
            _users.Add(loginInfo.Login, loginInfo);
            _usersLock.ExitWriteLock();
            _usersLock.ExitUpgradeableReadLock();

            try
            {
                _userProvider.Logins.Add(loginInfo);
                _watcher.EnableRaisingEvents = false;
                _userProvider.Save(_basePath);
                _watcher.EnableRaisingEvents = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveLogin(string login)
        {
            _usersLock.EnterWriteLock();
            bool removed = _users.Remove(login);
            _usersLock.ExitWriteLock();

            if (removed)
            {
                _userProvider.Logins.RemoveAll(item => item.Login.Equals(login));
                _watcher.EnableRaisingEvents = false;
                _userProvider.Save(_basePath);
                _watcher.EnableRaisingEvents = true;
            }

            return removed;
        }

        public IEnumerable<UserInfo> GetUsersList()
        {
            _usersLock.EnterReadLock();
            var users = _users.Select(
                item => 
                {
                    return new UserInfo
                    {
                        Login = item.Key,
                        Name = item.Value.UserName
                    };
                });
            _usersLock.ExitReadLock();
            return users;
        }

        public UserParam GetUserParam(string userLogin)
        {
            UserParam user;
            _usersLock.EnterReadLock();
            _users.TryGetValue(userLogin, out user);
            _usersLock.ExitReadLock();

            return user;
        }

        public bool ChangeAdminPass(string newPass)
        {
            if (string.IsNullOrWhiteSpace(newPass))
                return false;

            _adminPass = GetMd5Hash(newPass);
            _userProvider.AdminPass = _adminPass;
            _watcher.EnableRaisingEvents = false;
            _userProvider.Save(_basePath);
            _watcher.EnableRaisingEvents = true;
            return true;
        }


        public PermissionResult CheckPermission(Guid guid, Operations operation)
        {
            SessionInfo session;
            _sessionsLockSlim.EnterReadLock();
            bool hasSession = _sessions.TryGetValue(guid, out session);
            _sessionsLockSlim.ExitReadLock();

            if (!hasSession)
                return PermissionResult.DoesntLogined;

            if (HasRights(session.CurrentRole, operation))
                return PermissionResult.Ok;
            else
                return PermissionResult.AccessDeny;
        }

        private bool HasRights(RoleTypes roleTypes, Operations operation)
        {
            switch (operation)
            { 
                case Operations.CreateNewClient:
                    return roleTypes == RoleTypes.Payin || roleTypes == RoleTypes.Admin;
                case Operations.GetClientByPhone:
                    return true;
                case Operations.PayInToAccount:
                    return roleTypes == RoleTypes.Payin || roleTypes == RoleTypes.Admin;
                case Operations.PayoutFromAccount:
                    return roleTypes == RoleTypes.Payout || roleTypes == RoleTypes.Admin;
                case Operations.SendPayinCode:
                    return roleTypes == RoleTypes.Payin || roleTypes == RoleTypes.Admin;
                case Operations.SendPayoutCode:
                    return roleTypes == RoleTypes.Payout || roleTypes == RoleTypes.Admin;
                case Operations.CreateUser:
                    return roleTypes == RoleTypes.Admin;
                case Operations.RemoveUser:
                    return roleTypes == RoleTypes.Admin;
                case Operations.UpdateUser:
                    return roleTypes == RoleTypes.Admin;
                case Operations.ChangeAdminPass:
                    return roleTypes == RoleTypes.Admin;
                case Operations.GetUsersList:
                    return roleTypes == RoleTypes.Admin;
                default:
                    return false;
            }
        }

        public static string GetMd5Hash(string input)
        {
            byte[] data = _md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static bool VerifyMd5Hash(string input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
