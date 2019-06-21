using System;
using System.Threading.Tasks;
using Grpc.Core;
using Foo.Bll;
using Foo.Contracts;


namespace Foo.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            StartService(50051);
        }

        private static void StartService(int port)
        {
            Server server = new Server
            {
                Services = { Greeter.BindService(new GreeterImpl()) },
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Greeter server listening on port " + port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
