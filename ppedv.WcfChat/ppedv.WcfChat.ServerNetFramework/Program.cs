using ppedv.WcfChat.Contracts;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ppedv.WcfChat.ServerNetFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"*** WCF Chat Server ***");

            var tcp = new NetTcpBinding();
            tcp.Security.Mode = SecurityMode.None;
            tcp.MaxReceivedMessageSize = int.MaxValue;
            tcp.TransferMode = TransferMode.Buffered;
            //tcp.ReliableSession.Enabled = true;


            var host = new ServiceHost(typeof(WcfChatServer));

            host.Description.Behaviors.Add(new ServiceHealthBehavior() { HttpsGetEnabled = true, HttpGetUrl = new Uri("http://localhost:3") });

            host.AddServiceEndpoint(typeof(IChatServer), tcp, "net.tcp://localhost:1");

            host.Open();

            Console.WriteLine("Server gestartet");
            Console.ReadKey();

            Console.WriteLine("Ende");
            Console.ReadKey();
        }
    }
}
