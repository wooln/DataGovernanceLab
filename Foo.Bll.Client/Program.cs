using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foo.Contracts;

namespace Foo.Bll.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                GreeterImpl client = new GreeterImpl();

                Console.Write("Input your name: ");
                String user = Console.ReadLine();

                var reply = client.SayHello(new HelloRequest { Name = user }, null).Result;
                Console.WriteLine("Greeting: " + reply.Message);
            }
        }
    }
}
