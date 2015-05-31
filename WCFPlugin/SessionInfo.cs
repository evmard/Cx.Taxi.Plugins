using Cx.Client.Utils.Server;
using System;
using WCFPlugin.DataContract;

namespace WCFPlugin
{
    public class SessionInfo
    {
        public SessionInfo(UserParam loginParam, Guid guid, RoleTypes currentRole)
        {
            LoginParam = loginParam;
            Guid = guid;
            CurrentRole = currentRole;
        }

        public UserParam LoginParam { get; private set; }

        public Guid Guid { get; private set; }

        public RoleTypes CurrentRole { get; private set; }

        public LoginInfo GetLoginInfo()
        {
            return new LoginInfo
            {
                RoleType = CurrentRole,
                SessionGuid = Guid,
                UserName = LoginParam.UserName,
                Rate = LoginParam.Rate
            };
        }
    }
}