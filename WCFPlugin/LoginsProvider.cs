using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace WCFPlugin
{
    using DataContract;

    public class UsersProvider
    {
        public string AdminPass { get; set; }

        public List<UserParam> Logins { get; set; }

        public static string FileName = "WCFLogins.xml";

        public UsersProvider() : this (false)
        {             
        }

        private UsersProvider(bool AddDefaultData)
        {
            AdminPass = LogonManager.GetMd5Hash("admin");
            Logins = new List<UserParam>();

            if (AddDefaultData)
            {
                var testLogin = new UserParam
                {
                    Login = "test1",
                    Password = LogonManager.GetMd5Hash("test1"),
                    Rate = 0.50,
                    Roles = new List<RoleTypes>{ RoleTypes.Payin },
                    UserName = "PayIn Test User"
                };

                Logins.Add(testLogin);

                testLogin = new UserParam
                {
                    Login = "test2",
                    Password = LogonManager.GetMd5Hash("test2"),
                    Rate = 0.50,
                    Roles = new List<RoleTypes>{ RoleTypes.Payout },
                    UserName = "PayOut Test User"
                };

                Logins.Add(testLogin);
            }
        }

        public static void CreateFile(string path)
        {
            using (Stream writer = new FileStream(path + "\\" + FileName, FileMode.Create))
            {
                var param = new UsersProvider(true);
                XmlSerializer serializer = new XmlSerializer(typeof(UsersProvider));
                serializer.Serialize(writer, param);
            }
        }

        public static UsersProvider LoadLogins(string path)
        {
            try
            {
                string fileName = path + "\\" + FileName;
                using (Stream stream = new FileStream(fileName, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(UsersProvider));
                    return (UsersProvider)serializer.Deserialize(stream);
                }
            }
            catch
            {
                CreateFile(path);

                return new UsersProvider(true);
            }
        }


        public void Save(string path)
        {
            using (Stream writer = new FileStream(path + "\\" + FileName, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UsersProvider));
                serializer.Serialize(writer, this);
            }
        }
    }
}
