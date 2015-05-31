using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WCFPlugin.DataContract
{
    [DataContract]
    public class UserParam
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public List<RoleTypes> Roles { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public double Rate { get; set; }
    }
}
