using SelfHosting.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;

namespace SelfHosting.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF HOST ***");

            var host = new ServiceHost(typeof(PizzaService));


            var netTcp = new NetTcpBinding();
            //netTcp.ReliableSession.Enabled = true;
            //netTcp.ReliableSession.Ordered = true;


            host.AddServiceEndpoint(typeof(IPizzaService), netTcp, "net.tcp://localhost:1");

            var basic = new BasicHttpBinding();
            host.AddServiceEndpoint(typeof(IPizzaService), new BasicHttpBinding(), "http://localhost:2");

            //var ws = new WSHttpBinding();
            //ws.ReliableSession.Enabled = true;
            //ws.ReliableSession.Ordered = true;
            //ws.Security.Mode = SecurityMode.None;
            //host.AddServiceEndpoint(typeof(IPizzaService), ws, "http://localhost:3");
            host.AddServiceEndpoint(typeof(IPizzaService), new NetNamedPipeBinding(), "net.pipe://pizza/");

            var smb = new ServiceMetadataBehavior() { HttpGetEnabled = true, HttpGetUrl = new Uri("http://localhost:2/mex") };
            host.Description.Behaviors.Add(smb);

            host.Open();

            Console.WriteLine("Server gestartet");
            Console.ReadKey();
            //host.Close();

            Console.WriteLine("Ende");
            Console.ReadKey();
        }
    }


    [ServiceBehavior(IncludeExceptionDetailInFaults = true, AutomaticSessionShutdown = false)]
    public class PizzaService : IPizzaService
    {

        static int count = 0;

        public PizzaService()
        {
            Console.WriteLine($"New PizzaService Instance: {count++}");
        }

        public IEnumerable<Pizza> GetAllPizzas()
        {
            //throw new FaultException("Schade");

            throw new FaultException<OutOfPizzaError>(new OutOfPizzaError() { Msg = ":-(", WerIstSchuld = "Fred" }, new FaultReason("AAAA"));

            //yield return new Pizza { Id = 1, Name = "Käse", Price = 7.9m };
            //yield return new Pizza { Id = 2, Name = "Salami", Price = 8.5m };
            //yield return new Pizza { Id = 3, Name = "Schinken", Price = 9.2m };
        }

        public void OrderPizza(int amount, Pizza pizza)
        {
            Console.WriteLine($"OrderPizza {amount} Thread#{Thread.CurrentThread.ManagedThreadId}");
            //return pizza.Price * amount;
        }
    }
}
