using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFPlugin.DataContract;

namespace WCFPlugin
{
    public interface ILogonManager
    {
        LogonResult Login(string login, string pass, RoleTypes role, out SessionInfo sessionInfo);
        void Logout(Guid sessionGuid);

        PermissionResult CheckPermission(Guid guid, Operations operation);

        bool AddNewLogin(UserParam loginInfo);
        bool RemoveLogin(string login);

        IEnumerable<UserInfo> GetUsersList();

        UserParam GetUserParam(string userLogin);

        bool EditLogin(UserParam loginInfo);

        bool ChangeAdminPass(string newPass);
    }

    public enum LogonResult
    {
        Ok,
        WrongLoginOrPass,
        AlreadyLogined,
        WrongRole
    }

    public enum PermissionResult
    {
        Ok,
        DoesntLogined,
        AccessDeny
    }

    public enum Operations
    {
        GetClientByPhone,
        CreateNewClient,
        PayoutFromAccount,
        PayInToAccount,
        SendPayinCode,
        SendPayoutCode,
        CreateUser,
        RemoveUser,
        UpdateUser,
        ChangeAdminPass,
        GetUsersList
    }
}
