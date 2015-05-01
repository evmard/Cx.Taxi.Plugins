using System;
using System.ComponentModel;
using Cx.Client.Data.DataObject;
using Cx.Client.Taxi.Billings.Data;
using Cx.Client.Taxi.Clients.Data;
using Cx.Client.Taxi.Corporations.Data;
using Cx.Client.Taxi.DiscountCards.Data;
using Cx.Client.Taxi.Enums;
using Cx.Client.Taxi.Markups.Data;
using Cx.Client.Taxi.Services.Data;
using Cx.Client.Taxi.TranslationLanguages.Data;

namespace WCFPlugin.Stub
{
    public class ClientStub : IClient
    {
        public ClientStub(long id, string name, string phone)
        {
            ID = id;
            Name = name;
            Phone = phone;
            Billing = new BillingStub(id + 1, 1000);
        }

        public string Phone { get; set; }

        public long ID { get; set; }

        public string Name { get; set; }

        public int? OrdersCounter { get; set; }

        public int? OrdersCanceled { get; set; }

        public string Description { get; set; }

        public long? IDDeliveriAdds { get; set; }

        public long? IDDestionationAdds { get; set; }

        public string Entrance { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public EmploeersType CantRun { get; set; }

        public string Apartment { get; set; }

        public bool? Cashless { get; set; }

        public string Comment { get; set; }

        public DateTime? Birthday { get; set; }

        public double? Rating { get; set; }
        public string GUID { get; set; }

        public string AddrDescription { get; set; }

        public long? IDBilling { get; set; }
        public IBilling Billing { get; set; }

        public ICxDataObjectCollection<IMarkup> Markups{ get { return null; } }

        public long? IDDefaultService { get; set; }

        public IService DefaultService { get; set; }

        public long? IDCorporation { get; set; }

        public ICorporation Corporation { get; set; }

        public ICxDataObjectCollection<IDiscountCard> Discounts { get { return null; }
        }

        public long? IDDefaultDiscount { get; set; }

        public IDiscountCard DefaultDiscount { get; set; }

        public long? IDLanguage { get; set; }

        public ITranslationLanguage Language { get; set; }

        public void MakeCall() { }

        public void SendSMS() { }

        public void EditComment() { }

        public void ChangeRating() { }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool DataObjectDeleted {get { return false; } }
    }
}