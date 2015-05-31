using System;
using System.Collections.Generic;
using System.IO;
using Cx.Client.Taxi.Clients.Data;
using Cx.Client.Utils.Server;
using WCFPlugin.Contract;
using WCFPlugin.CxPlugin;
using WCFPlugin.DataContract;

namespace WCFPlugin.Stub
{
    public class DataProviderStub : ICxDataProvider
    {
        private readonly Dictionary<string, IClient> _clientsByPhone = new Dictionary<string, IClient>();
        private readonly Dictionary<long, IClient> _clientsById = new Dictionary<long, IClient>();
        private readonly Random _rng = new Random(DateTime.Now.Millisecond);

        private readonly StreamWriter file = File.CreateText("CodesAreSent.txt");

        public DataProviderStub()
        {
            const string phone = "123456789";
            IClient defaultClient = new ClientStub(_rng.Next(), "Алексей Авершин", phone);
            _clientsByPhone.Add(phone, defaultClient);
            _clientsById.Add(defaultClient.ID, defaultClient);
        }

        public IClient GetClient(string phone)
        {
            IClient client;
            return _clientsByPhone.TryGetValue(phone, out client) ? client : null;
        }

        public IClient CreateNewClient(string phone, string name)
        {
            var client = new ClientStub(_rng.Next(), name, phone);
            _clientsByPhone.Add(phone, client);
            _clientsById.Add(client.ID, client);
            return client;
        }

        public bool TryPayInToAccount(long clientId, double amount, out ClientInfo clientInfo)
        {
            clientInfo = null;
            IClient clientStub;
            if (!_clientsById.TryGetValue(clientId, out clientStub))
                return false;

            if (clientStub.Billing.Balance.HasValue)
                clientStub.Billing.Balance = clientStub.Billing.Balance.Value + amount;
            else
                clientStub.Billing.Balance = amount;

            clientInfo = new ClientInfo(clientStub, ((ClientStub)clientStub).Phone);
            return true;
        }

        public bool TryPayoutFromAccount(long clientId, double amount, out ClientInfo clientInfo)
        {
            clientInfo = null;
            IClient clientStub;
            if (!_clientsById.TryGetValue(clientId, out clientStub))
                return false;

            if (clientStub.Billing.Balance == null || (clientStub.Billing.Balance ?? 0) < amount)
                return false;

            clientStub.Billing.Balance = clientStub.Billing.Balance.Value - amount;

            clientInfo = new ClientInfo(clientStub, ((ClientStub)clientStub).Phone);
            return true;
        }

        public void SendCode(string phone, string text, SendMethod sendMethod)
        {
            file.WriteLine("***SendCode*** Phone = {0} Text = {1} method = {2}", phone, text, sendMethod);
            file.Flush();
        }

        public PluginParams PluginParams { get; set; }
    }
}