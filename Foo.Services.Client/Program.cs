using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foo.Contracts;
using Grpc.Core;
using Library.ServiceTool;
using Newtonsoft.Json;

namespace Foo.Services.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //服务发现
            string serviceKey = "Foo.Services";
            ServiceInformation service = ServiceBus.Get(serviceKey).Result.FeelingLucky();
            //service = new ServiceInformation()
            //{
            //    Host = "localhost",
            //    Port = 50051,
            //};
            while (true)
            {
                string target = $"{service.Host}:{service.Port}";
                Channel channel = new Channel(target, ChannelCredentials.Insecure);
                var client = new Greeter.GreeterClient(channel);

                Console.Write("Input your name: ");
                String user = Console.ReadLine();

                var request = new HelloRequest { Name = user };
                Console.WriteLine($"request: \n{JsonConvert.SerializeObject(request)}");

                var reply = client.SayHello(request);
                Console.WriteLine($"reply: \n{JsonConvert.SerializeObject(reply)}");

                channel.ShutdownAsync().Wait();
            }
        }
    }
}
