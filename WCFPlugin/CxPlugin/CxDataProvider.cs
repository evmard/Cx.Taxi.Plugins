using Cx.Client.CallManagement;
using Cx.Client.Data;
using Cx.Client.Security.Users.Data;
using Cx.Client.Taxi.Billings.Data;
using Cx.Client.Taxi.Clients.Data;
using Cx.Client.Taxi.Corporations.Data;
using Cx.Client.Taxi.Enums;
using Cx.Client.Taxi.PaymentRoutine;
using Cx.Client.Utils;
using Cx.Client.Utils.Server;
using WCFPlugin.Contract;
using WCFPlugin.DataContract;

namespace WCFPlugin.CxPlugin
{
    public class CxDataProvider : ICxDataProvider
    {
        
        private readonly ClientManager.ClientManager _clientManager;
        private readonly IBillings _billings;
        private readonly IClients _clients;
        private readonly IUsers _users;

        public PluginParams PluginParams { get; set; }

        public CxDataProvider(PluginParams param, IBillings billings, IUsers users)
        {
            _billings = billings;
            PluginParams = param;
            _clientManager = ClientManager.ClientManager.GetManager();
            _clients = _clientManager.GetClients();
            _users = users;
        }

        public LogonResult TryGetConnection(
            string login,
            string pass,
            long roleId, 
            out ICxChildConnection connection, 
            out string errorMsg)
        {
            var logonResult = CxServerUtils.ConnectChild(login, pass, roleId, PluginParams.HostName, out connection, out errorMsg);
            GlobalLogManager.WriteString("Info: WCFPlugin TryGetConnection: {0}, {1}, {2}, {3};\n\t{4}, {5}",
                login, pass, roleId, PluginParams.HostName, logonResult, errorMsg);
            return logonResult;
        }

        public IClient GetClient(string phone)
        {
            ICorporation corp;
            return _clientManager.GetClientByPhone(phone, out corp);
        }

        public IClient CreateNewClient(string phone, string name)
        {
            var client = _clientManager.AddClient(phone, name, 0);
            CheckAndCreateBilling(client);
            return client;
        }

        public bool TryPayInToAccount(long clientId, double amount, out ClientInfo clientInfo)
        {
            clientInfo = null;
            var client = _clients.GetByID(clientId);
            if (client == null)
                return false;

            var messageTemplate = PluginParams.PayinMessageTemplate;
            var logDescription = PluginParams.PayinLogDescription;

            DoPayments(amount, client, logDescription, messageTemplate);
            clientInfo = new ClientInfo(client, _clientManager.GetDefaultPhone(clientId));
            return true;
        }

        public bool TryPayoutFromAccount(long clientId, double amount, out ClientInfo clientInfo)
        {
            clientInfo = null;
            var client = _clients.GetByID(clientId);
            if (client == null)
                return false;

            var messageTemplate = PluginParams.PayOutMessageTemplate;
            var logDescription = PluginParams.PayOutLogDescription;

            DoPayments(-amount, client, logDescription, messageTemplate);
            clientInfo = new ClientInfo(client, _clientManager.GetDefaultPhone(clientId));
            return true;
        }

        public string GetUserName(long userId)
        {
            var user = _users.GetByID(userId);
            return user == null ? "Не найден" : user.Name_F + " " + user.Name_I + " " + user.Name_O;
        }

        public void SendCode(string phone, string text, SendMethod sendMethod)
        {
            GlobalLogManager.WriteString("Info: WCFPlugin SendCode: phone {0}, text {1}, method {2}",
                phone, text, sendMethod);

            if (string.IsNullOrWhiteSpace(phone))
                return;

            switch (sendMethod)
            {
                case SendMethod.SMS:
                    CallManagementInterface.SendSMS(phone, text, 0);
                    break;
                case SendMethod.Call:
                    CallManagementInterface.StartAutoinformator(phone, text, 0);
                    break;
                case SendMethod.Both:
                    CallManagementInterface.SendSMS(phone, text, 0);
                    CallManagementInterface.StartAutoinformator(phone, text, 0);
                    break;
            }    
        }


        private void DoPayments(double amount, IClient client, string logDescription, string messageTemplate)
        {
            CheckAndCreateBilling(client);
            client.Billing.Balance += amount; 
            PaymentRoutine.AddBillingLogsInThreadPool( 
                amount,
                BillingTypes.Manually,
                client.Billing.ID,
                null /*orderId*/,
                string.Empty,
                logDescription,
                client.Billing.Balance ?? 0,
                BillingLogType.Client,
                null /*orerNumber*/);

            string number = _clientManager.GetDefaultPhone(client.ID);
            string message = string.Format(messageTemplate, amount);

            if (!string.IsNullOrWhiteSpace(number))
            {
                if (PluginParams.NeedMakeCall)
                    CallManagementInterface.StartAutoinformator(number, message, 0);

                if (PluginParams.NeedSendSms)
                    CallManagementInterface.SendSMS(number, message, 0);
            }
            else
            {
                GlobalLogManager.WriteString("Warning: WCFPlugin. У клиента не указан номер телефона");
            }
        }

        private void CheckAndCreateBilling(IClient client)
        {
            if (client.IDBilling == null)
            {
                client.Billing = _billings.AddNew(item =>
                {
                    item.ID = DbUtils.GenID();
                    item.Account = item.ID.ToString();
                    item.Balance = 0;
                    item.Limit = 0;
                    item.OwnerType = BillingLogType.Client;
                });
            }
        }
    }
}