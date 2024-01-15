
Console.WriteLine("Hello, World!");

var client = new ServiceReference1.Service1Client();

var result = client.GetDataAsync(12).Result;
Console.WriteLine(result);