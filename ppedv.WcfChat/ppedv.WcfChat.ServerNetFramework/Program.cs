using ppedv.WcfChat.Contracts;
using System;
using System.ServiceModel;

namespace ppedv.WcfChat.ServerNetFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"*** WCF Chat Server ***");

            var tcp = new NetTcpBinding();
            tcp.ReliableSession.Enabled = true;

            var host = new ServiceHost(typeof(WcfChatServer));

            host.AddServiceEndpoint(typeof(IChatServer), tcp, "net.tcp://localhost:1");

            host.Open();

            Console.WriteLine("Server gestartet");
            Console.ReadKey();

            Console.WriteLine("Ende");
            Console.ReadKey();
        }
    }
}
