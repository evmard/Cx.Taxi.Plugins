using Cx.Client.Taxi.Clients.Data;
using Cx.Client.Utils.Server;
using WCFPlugin.Contract;
using WCFPlugin.DataContract;

namespace WCFPlugin.CxPlugin
{
    public interface ICxDataProvider
    {
        PluginParams PluginParams { get; set; }

        LogonResult TryGetConnection(string login, string pass, long roleId, out ICxChildConnection connection, out string errorMsg);

        IClient GetClient(string phone);

        IClient CreateNewClient(string phone, string name);

        bool TryPayInToAccount(long clientId, double amount, out ClientInfo clientInfo);

        bool TryPayoutFromAccount(long clientId, double amount, out ClientInfo clientInfo);

        string GetUserName(long p);

        void SendCode(string phone, string text, SendMethod sendMethod);
    }
}