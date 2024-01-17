// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");

ConnectionFactory factory = new ConnectionFactory() { HostName = "20.113.73.199", UserName = "guest", Password = "guest" };
IConnection conn = factory.CreateConnection();

using var channel = conn.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);



for (int i = 0; i < 500000; i++)
{
    string message = $"({i}) Hello World, it's André - {DateTime.Now:g} + {RandomString()}";
    var body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: string.Empty,
                         routingKey: "hello",
                         basicProperties: null,
                         body: body);

    if (i % 10000 == 0)
        Console.WriteLine($" [x] Sent {message}");
}

 static string RandomString()
{
    Random random = new Random();
    int length = random.Next(100, 1001);
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();