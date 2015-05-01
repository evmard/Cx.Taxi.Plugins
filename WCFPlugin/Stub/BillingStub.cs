using System.ComponentModel;
using Cx.Client.Taxi.Billings.Data;
using Cx.Client.Taxi.Enums;

namespace WCFPlugin.Stub
{
    public class BillingStub : IBilling
    {
        public BillingStub(long id, int summ)
        {
            ID = id;
            Account = ID.ToString();
            Balance = summ;
        }

        public long ID { get; set; }

        public string Account { get; set; }

        public double? Balance { get; set; }

        public double? Limit { get; set; }

        public BillingLogType? OwnerType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool DataObjectDeleted { get { return false; } }
    }
}