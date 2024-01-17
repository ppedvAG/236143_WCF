using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Hosting;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.DependencyInjection;


class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddServiceModelServices()
                .AddServiceModelMetadata()
                .AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
        services.AddSingleton<ITestService, TestService>();

        var serviceProvider = services.BuildServiceProvider();
        var serviceBuilder = serviceProvider.GetRequiredService<IServiceBuilder>();
        serviceBuilder.AddService<TestService>()
                      .AddServiceEndpoint<TestService, ITestService>(new CoreWCF.BasicHttpBinding(), "/TestService");

        var app = new WebHostBuilder()
            .UseKestrel()
            .UseUrls("http://localhost:8000")
            .ConfigureServices(services => services.Add(serviceBuilder))
            .Configure(app => app.UseServiceModel())
            .Build();

        app.Run();
    }
}

[ServiceContract]
public interface ITestService
{
    [OperationContract]
    string SendMessage(string message);
}

public class TestService : ITestService
{
    public string SendMessage(string message)
    {
        // Send message to RabbitMQ
        RabbitMQProducer.SendMessageToRabbitMQ(message);
        return "Message received: " + message;
    }
}

public static class RabbitMQProducer
{
    public static void SendMessageToRabbitMQ(string message)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "testQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "testQueue",
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}