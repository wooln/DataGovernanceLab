using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foo.Contracts;
using Grpc.Core;

namespace Foo.Services.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Channel channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
                var client = new Greeter.GreeterClient(channel);

                Console.Write("Input your name: ");
                String user = Console.ReadLine();

                var reply = client.SayHello(new HelloRequest { Name = user });
                Console.WriteLine("Greeting: " + reply.Message);
                channel.ShutdownAsync().Wait();
            }
        }
    }
}
