using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foo.Contracts;
using Grpc.Core;
using Library.ServiceTool;

namespace Foo.Services.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //服务发现
            string serviceKey = "Foo.Services";
            ServiceInformation service = ServiceBus.Get(serviceKey).FeelingLucky();

            while (true)
            {
                string target = $"{service.Host}:{service.Port}";
                Channel channel = new Channel(target, ChannelCredentials.Insecure);
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
