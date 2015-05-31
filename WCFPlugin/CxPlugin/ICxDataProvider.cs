using Cx.Client.Taxi.Clients.Data;
using WCFPlugin.Contract;
using WCFPlugin.DataContract;

namespace WCFPlugin.CxPlugin
{
    public interface ICxDataProvider
    {
        PluginParams PluginParams { get; set; }

        IClient GetClient(string phone);

        IClient CreateNewClient(string phone, string name);

        bool TryPayInToAccount(long clientId, double amount, out ClientInfo clientInfo);

        bool TryPayoutFromAccount(long clientId, double amount, out ClientInfo clientInfo);

        void SendCode(string phone, string text, SendMethod sendMethod);
    }
}