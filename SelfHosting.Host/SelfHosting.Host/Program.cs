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
            host.AddServiceEndpoint(typeof(IPizzaService), new NetTcpBinding(), "net.tcp://localhost:1");
            host.AddServiceEndpoint(typeof(IPizzaService), new BasicHttpBinding(), "http://localhost:2");
            host.AddServiceEndpoint(typeof(IPizzaService), new WSHttpBinding(), "http://localhost:3");
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


    public class PizzaService : IPizzaService
    {
        public IEnumerable<Pizza> GetAllPizzas()
        {
            yield return new Pizza { Id = 1, Name = "Käse", Price = 7.9m };
            yield return new Pizza { Id = 2, Name = "Salami", Price = 8.5m };
            yield return new Pizza { Id = 3, Name = "Schinken", Price = 9.2m };
        }

        public decimal OrderPizza(int amount, Pizza pizza)
        {
            Console.WriteLine($"OrderPizza Thread#{Thread.CurrentThread.ManagedThreadId}");
            return pizza.Price * amount;
        }
    }
}
