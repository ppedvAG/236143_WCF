// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


ServiceReference1.ServiceClient serviceClient = new ServiceReference1.ServiceClient();
serviceClient.Open();
Console.WriteLine(serviceClient.GetDataAsync(5).Result);

Console.ReadLine();