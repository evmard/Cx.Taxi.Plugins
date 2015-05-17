using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Cx.Client.Taxi.ClientsBounty
{
    public class PluginParams
    {
        public List<long> IdsServices {get; set;}
        public double Procent { get; set; }
        public string BountyDescription { get; set; }
        public string MessageTemplate { get; set; }
        public bool NeedMakeCall { get; set; }
        public bool NeedSendSMS { get; set; }

        public PluginParams()
        {
            IdsServices = new List<long> { 5252428308, 123456789 };
            Procent = 0.025;
            BountyDescription = "Вознаграждение за заказ.";
            MessageTemplate = "На Ваш счет начислено {0} рублей.";
            NeedMakeCall = true;
            NeedSendSMS = true;
        }

        public PluginParams(PluginParams other)
        {
            IdsServices = other.IdsServices;
            Procent = other.Procent;
            BountyDescription = other.BountyDescription;
            MessageTemplate = other.MessageTemplate;
            NeedMakeCall = other.NeedMakeCall;
            NeedSendSMS = other.NeedSendSMS;
        }

        public static void CreateFile(string path)
        {
            using (Stream writer = new FileStream(path + "\\" + "ClientsBountyParams.xml", FileMode.Create))
            {
                var param = new PluginParams();
                XmlSerializer serializer = new XmlSerializer(typeof(PluginParams));
                serializer.Serialize(writer, param);
            }
        }
    }
}
