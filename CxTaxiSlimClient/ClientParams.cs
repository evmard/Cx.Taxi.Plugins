using System.IO;
using System.Xml.Serialization;
using CxTaxiSlimClient.CxTaxiService;

namespace CxTaxiSlimClient
{
    public class ClientParams
    {
        private const string FileName = "ClientParams.xml";
        public string HostName { get; set; }
        public int Port { get; set; }

        public string UserName { get; set; }

        public RoleTypes? RoleType { get; set; }

        private ClientParams()
        {
            HostName = "localhost";
            Port = 23444;
        }

        public void Save()
        {
            using (Stream writer = new FileStream(FileName, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ClientParams));
                serializer.Serialize(writer, this);
            }
        }

        public static ClientParams Load()
        {
            try
            {
                using (Stream stream = new FileStream(FileName, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ClientParams));
                    return (ClientParams)serializer.Deserialize(stream);
                }
            }
            catch
            {
                return new ClientParams();
            }
        }
    }
}