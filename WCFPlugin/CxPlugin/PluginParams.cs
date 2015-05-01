using System;
using System.IO;
using System.Xml.Serialization;

namespace WCFPlugin.CxPlugin
{
    public class PluginParams
    {
        public static string FileName = "WCFPluginParams.xml";
        public string HostName { get; set; }
        public int Port { get; set; }
        public long RoleId { get; set; }
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
            RoleId = 1000003000;
            PayinLogDescription = "Зачисление суммы через WCFPlugin";
            PayinMessageTemplate = "Ваш счет пополнен на сумму {0} руб.";
            NeedMakeCall = true;
            NeedSendSms = true;
            PayOutMessageTemplate = "Списание суммы через WCFPlugin";
            PayOutLogDescription = "С Вашего счета списано {0} руб.";
        }

        public PluginParams(PluginParams other)
        {
            HostName = other.HostName;
            Port = other.Port;
            RoleId = other.RoleId;
            PayinLogDescription = other.PayinLogDescription;
            PayinMessageTemplate = other.PayinMessageTemplate;
            NeedMakeCall = other.NeedMakeCall;
            NeedSendSms = other.NeedSendSms;
            PayOutMessageTemplate = other.PayOutMessageTemplate;
            PayOutLogDescription = other.PayOutLogDescription;
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
