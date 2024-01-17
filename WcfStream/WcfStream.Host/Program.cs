using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfStream.Contracts;

namespace WcfStream.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(StreamSampleServer));

            var tcp = new NetTcpBinding();
            tcp.TransferMode = TransferMode.Streamed;
            tcp.MaxReceivedMessageSize = int.MaxValue;

            host.AddServiceEndpoint(typeof(IStreamingSample), tcp, "net.tcp://localhost:1");

            host.Description.Behaviors.Add(new ServiceHealthBehavior() { HttpsGetEnabled = true, HttpGetUrl = new Uri("http://localhost:3") });


            host.Open();
            Console.WriteLine("Läuft");
            Console.ReadLine();
            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, AutomaticSessionShutdown = false, ConcurrencyMode = ConcurrencyMode.Single)]
    public class StreamSampleServer : IStreamingSample
    {
        public StreamSampleServer()
        {
            Console.WriteLine("Neuer Server");
            var imgPath = @"C:\Users\Fred\Pictures\SamplesImages\pexels-pixabay-326055.jpg";


            cache = new MemoryStream();
            var fs = File.OpenRead(imgPath);
            fs.CopyTo(cache);
            fs.Close();
        }

        MemoryStream cache;
        public Stream EchoStream(Stream stream)
        {
            throw new NotImplementedException();
        }

        public Stream GetReversedStream()
        {
            throw new NotImplementedException();
        }
        static int count = 0;
        public Stream GetStream(string data)
        {
            Console.WriteLine($"Sending img {count++}");
            //cache.Position = 0;
            //var ms = new MemoryStream();
            //cache.CopyTo(ms);
            //return ms;
            var imgPath = @"C:\Users\Fred\Pictures\SamplesImages\pexels-pixabay-326055.jpg";
            return File.OpenRead(imgPath);
        }

        public bool UploadStream(Stream stream)
        {
            throw new NotImplementedException();
        }

        public string GetString()
        {
            return $"Es ist {DateTime.Now:f}";
        }

        public IEnumerable<Pizza> GetPizzas()
        {
            var list = new List<Pizza>();
            list.Add(new Pizza() { Id = 1, Name = "P1" });
            list.Add(new Pizza() { Id = 2, Name = "P2" });
            list.Add(new Pizza() { Id = 3, Name = "P3" });
            list.Add(new Pizza() { Id = 4, Name = "P4" });
            return list;
        }
    }
}
