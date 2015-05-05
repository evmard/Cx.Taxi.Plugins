using System.IO;
using System.Xml.Serialization;
using WCFPlugin.DataContract;

namespace WCFPlugin.CxPlugin
{
    public class PluginParams
    {
        public static string FileName = "WCFPluginParams.xml";
        public string HostName { get; set; }
        public int Port { get; set; }
        public long PayInRoleId { get; set; }
        public long PayOutRoleId { get; set; }
        public string PayinLogDescription { get; set; }
        public string PayinMessageTemplate { get; set; }
        public bool NeedMakeCall { get; set; }
        public bool NeedSendSms { get; set; }
        public string PayOutMessageTemplate { get; set; }
        public string PayOutLogDescription { get; set; }

        public PluginParams()
        {
            HostName = "localhost";
            Port = 23444;
            PayInRoleId = 1000003000;
            PayOutRoleId = 1000003000;
            PayinLogDescription = "Зачисление суммы через WCFPlugin";
            PayinMessageTemplate = "Ваш счет пополнен на сумму {0} руб.";
            NeedMakeCall = true;
            NeedSendSms = true;
            PayOutLogDescription = "Списание суммы через WCFPlugin";
            PayOutMessageTemplate = "С Вашего счета списано {0} руб.";
        }

        public long GetRoleId(RoleTypes roleType)
        {
            switch (roleType)
            {
                case RoleTypes.Payin:
                    return PayInRoleId;
                case RoleTypes.Payout:
                    return PayOutRoleId;
                default:
                    return PayInRoleId;
            }
        }

        public static void CreateFile(string path)
        {
            using (Stream writer = new FileStream(path + "\\" + FileName, FileMode.Create))
            {
                var param = new PluginParams();
                XmlSerializer serializer = new XmlSerializer(typeof(PluginParams));
                serializer.Serialize(writer, param);
            }
        }

        public static PluginParams LoadParams(string path)
        {
            try
            {
                string fileName = path + "\\" + FileName;
                using (Stream stream = new FileStream(fileName, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PluginParams));
                    return (PluginParams)serializer.Deserialize(stream);
                }
            }
            catch 
            {
                CreateFile(path);

                return new PluginParams();
            }
        }
    }
}
