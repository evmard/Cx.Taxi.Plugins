using Cx.Client.Utils.Server;

namespace WCFPlugin.Stub
{
    public class ConnectionStub : ICxChildConnection
    {
        public void Drop()
        {
        }

        public string Login
        {
            get { return "Admin"; }
        }

        public long IDUser
        {
            get { return 1; }
        }

        public long IDRole
        {
            get { return 1; }
        }
    }
}