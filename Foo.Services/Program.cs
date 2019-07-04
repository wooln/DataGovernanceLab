using System;
using System.Threading.Tasks;
using Grpc.Core;
using Foo.Bll;
using Foo.Contracts;
using Library.ServiceTool;
using Library.ServiceTool.Util;
using Rabbit.Zookeeper;

namespace Foo.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            int port;
            if (args.Length > 0)
            {
                port = int.Parse(args[0]);
            }
            else
            {
                port = GetRandomPort(50000, 60000);
            }
            StartService("127.0.0.1", port).Await();
        }

        private static int GetRandomPort(int start, int end)
        {
            Random ran = new Random();
            return ran.Next(start, end);
        }

        private static async Task StartService(string host, int port)
        {
            //服务信息
            var serviceInfo = new ServiceInformation()
            {
                Host = host,
                Port = port,
                Description = "我的小服务, 老火了",
                Key = "Foo.Services",
                Name = "我的小服务",
                ServiceType = ServiceType.Grpc,
            };

            //启动服务
            Server service = new Server
            {
                Services = { Greeter.BindService(new GreeterImpl()) },
                Ports = { new ServerPort(serviceInfo.Host, port, ServerCredentials.Insecure) }
            };
            service.Start();

            using (IZookeeperClient zon = await ServiceBus.Register(serviceInfo))
            {
                Console.WriteLine("Greeter server listening on port " + port);
                Console.WriteLine("Press any key to stop the server...");
                Console.ReadKey();
                service.ShutdownAsync().Wait();
            }
        }
    }
}
