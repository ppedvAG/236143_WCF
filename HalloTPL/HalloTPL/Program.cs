using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello TPL");

        //var t = new Thread(new ThreadStart(() => { Console.WriteLine("Oller Thread"); }));
        //t.Start();

        //Parallel.For(0, 100000, i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}"));
        //Parallel.Invoke(Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle);

        var t1 = new Task(() =>
        {
            Console.WriteLine("T1 gestartet");
            Thread.Sleep(3000);

            //throw new OutOfMemoryException();
            Console.WriteLine("T1 fertig");
        });

        t1.ContinueWith(t => Console.WriteLine("T1 continue (immer)"));
        t1.ContinueWith(t => Console.WriteLine($"T1 EX: {t.Exception.InnerException.Message}"), TaskContinuationOptions.OnlyOnFaulted);
        t1.ContinueWith(t => Console.WriteLine($"T1 OK"), TaskContinuationOptions.OnlyOnRanToCompletion);

        var t2 = new Task<long>(() =>
        {
            Console.WriteLine("T2 gestartet");
            Thread.Sleep(2000);
            Console.WriteLine("T2 fertig");
            return 923872893764;
        });

        t2.ContinueWith(t => { Console.WriteLine("T2 Continue"); });

        t1.Start();
        t2.Start();

        //Task.WaitAll(t1,t2);
        var result = t2.Result;
        Console.WriteLine($"T2 Result");


        Console.WriteLine("Ende");
        Console.ReadKey();
    }

    static void Zähle()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
        }
    }

}