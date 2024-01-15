using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWCF.Client_Framework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF Client ***");

            var ser1 = new ServiceReference1.Service1Client();

            var result = ser1.GetData(12);

            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
