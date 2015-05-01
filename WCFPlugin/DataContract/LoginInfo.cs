using System;
using System.Runtime.Serialization;

namespace WCFPlugin.DataContract
{
    [DataContract]
    public class LoginInfo
    {
        [DataMember]
        public Guid SessionGuid { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public RoleTypes RoleType { get; set; }
    }

    public enum RoleTypes
    {
        Payin,
        Payout
    }
}
