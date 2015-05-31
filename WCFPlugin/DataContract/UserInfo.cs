using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFPlugin.DataContract
{

    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
