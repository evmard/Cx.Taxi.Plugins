using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cx.Client.Taxi.ClientsBounty
{
    public class PluginParams
    {
        public long IDService {get; set;}
        public double Procent { get; set; }
        public string BountyDescription { get; set; }
        public string MessageTemplate { get; set; }
        public bool NeedMakeCall { get; set; }
        public bool NeedSendSMS { get; set; }

        public PluginParams()
        {
            IDService = 5252428308;
            Procent = 0.025;
            BountyDescription = "Вознаграждение за заказ.";
            MessageTemplate = "На Ваш счет начислено {0} рублей.";
            NeedMakeCall = true;
            NeedSendSMS = true;
        }

        public PluginParams(PluginParams other)
        {
            IDService = other.IDService;
            Procent = other.Procent;
            BountyDescription = other.BountyDescription;
            MessageTemplate = other.MessageTemplate;
            NeedMakeCall = other.NeedMakeCall;
            NeedSendSMS = other.NeedSendSMS;
        }
    }
}
