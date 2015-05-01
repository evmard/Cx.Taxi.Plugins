using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WCFPlugin;
using WCFPlugin.Contract;
using WCFPlugin.CxPlugin;

namespace WCFSelfHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PluginParams.CreateFile(AppDomain.CurrentDomain.BaseDirectory);

            {
                using (ServiceHost serviceHost = new ServiceHost(typeof (ClientBonusService)))
                {
                    try
                    {
                        NetTcpBinding portsharingBinding = new NetTcpBinding();
                        BasicHttpBinding httpBinding = new BasicHttpBinding();
                        serviceHost.AddServiceEndpoint(typeof(IClientBonusService), portsharingBinding, "net.tcp://localhost:23222/ClientBonusService");
                        serviceHost.AddServiceEndpoint(typeof(IClientBonusService), httpBinding, "http://localhost:23444/ClientBonusService");
                        serviceHost.Open();

                        // The service can now be accessed.
                        Console.WriteLine("The service is ready.");
                        Console.WriteLine("Press <ENTER> to terminate service.");
                        Console.ReadLine();

                        // Close the ServiceHost.
                        serviceHost.Close();
                    }
                    catch (TimeoutException timeProblem)
                    {
                        Console.WriteLine(timeProblem.Message);
                        Console.ReadLine();
                    }
                    catch (CommunicationException commProblem)
                    {
                        Console.WriteLine(commProblem.Message);
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}