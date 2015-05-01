using Cx.Client.Utils.Server;
using WCFPlugin.DataContract;

namespace WCFPlugin
{
    internal class SessionInfo
    {
        public LoginInfo LoginInfo { get; set; }

        public bool CanDoPayIn { get; set; }

        public bool CanDoPayout { get; set; }

        public ICxChildConnection Connection { get; set; }

        public bool CanCreateNewClients { get; set; }
    }
}