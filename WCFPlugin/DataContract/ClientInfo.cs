using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Cx.Client.Taxi.Clients.Data;
using Cx.Client.Utils.Extensions;

namespace WCFPlugin.DataContract
{
    [DataContract]
    public class ClientInfo
    {
        public ClientInfo()
        {
        }

        public ClientInfo(IClient client, string phone)
        {
            Phone = phone;
            AddrDescription = client.AddrDescription;
            Apartment = client.Apartment;
            BillingAccountNumber = client.Billing == null ? "Нет" : client.Billing.Account;
            Balance = client.Billing == null ? 0 : client.Billing.Balance ?? 0;
            Birthday = client.Birthday;
            Comment = client.Comment;
            CorporationName = client.Corporation == null ? null : client.Corporation.Name;
            DefaultDiscountCardNumber = client.DefaultDiscount == null ? "Нет" : client.DefaultDiscount.Name;
            DefaultLanguage = client.Language == null ? "Не указан" : client.Language.Name;
            DefaultServiceName = client.DefaultService == null ? "Не указана" : client.DefaultService.Name;
            Description = client.Description;
            if (client.Discounts != null && client.Discounts.Any())
            {
                DiscountCards = new List<string>(client.Discounts.Count);
                foreach (var item in client.Discounts)
                {
                    DiscountCards.Add(item.Name);
                }
            }

            Entrance = client.Entrance;
            Guid = client.GUID;
            Id = client.ID;
            Login = client.Login;

            if (client.Markups != null && client.Markups.Any())
            {
                Markups = new List<string>(client.Markups.Count);
                foreach (var item in client.Markups)
                {
                    Markups.Add(item.Name);
                }
            }

            Name = client.Name;
            OrdersCanceled = client.OrdersCanceled;
            OrdersCounter = client.OrdersCounter;
            Rating = client.Rating;
        }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string AddrDescription { get; set; }

        [DataMember]
        public string Apartment { get; set; }

        [DataMember]
        public string BillingAccountNumber { get; set; }

        [DataMember]
        public double Balance { get; set; }

        [DataMember]
        public DateTime? Birthday { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public string CorporationName { get; set; }

        [DataMember]
        public string DefaultDiscountCardNumber { get; set; }

        [DataMember]
        public string DefaultServiceName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public List<string> DiscountCards { get; set; }

        [DataMember]
        public string Entrance { get; set; }

        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string DefaultLanguage { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public List<string> Markups { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int? OrdersCanceled { get; set; }

        [DataMember]
        public int? OrdersCounter { get; set; }

        [DataMember]
        public double? Rating { get; set; }
    }
}
