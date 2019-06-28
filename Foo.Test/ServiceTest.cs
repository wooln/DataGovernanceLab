using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foo.Bll;
using Foo.Contracts;
using Google.Protobuf.Collections;
using Grpc.Core;
using Library.ServiceTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Foo.Test
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public async Task GreeterServiceTest()
        {
            //服务信息
            var serviceInfo = new ServiceInformation()
            {
                Host = "127.0.0.1",
                Port = 1234,
                Description = "我的小服务, 老火了",
                Key = "Foo.Services",
                Name = "我的小服务",
                ServiceType = ServiceType.Grpc,
            };

            //启动服务
            Server server = new Server
            {
                Services = { Greeter.BindService(new GreeterImpl()) },
                Ports = { new ServerPort(serviceInfo.Host, serviceInfo.Port, ServerCredentials.Insecure) }
            };
            server.Start();

            //注册服务
            using (var zon = await ServiceBus.Register(serviceInfo))
            {
                //发现服务
                string serviceKey = serviceInfo.Key;
                ServiceInformation service = ServiceBus.Get(serviceKey).Result.FeelingLucky();

                //使用服务

                string target = $"{service.Host}:{service.Port}";
                Channel channel = new Channel(target, ChannelCredentials.Insecure);
                Greeter.GreeterClient client = new Greeter.GreeterClient(channel);

                HelloRequest request = new HelloRequest()
                {
                    Name = "徐云金",
                    SecretSignal = "天王盖地虎",
                };
                request.Gifts.AddRange(new List<Gift>()
                {
                    new Gift{ Name = "兔子"},
                    new Gift{ Name = "橘猫"},
                });

                Console.WriteLine($"request: \n{JsonConvert.SerializeObject(request)}");

                HelloReply reply = client.SayHello(request);

                Console.WriteLine($"reply: \n{JsonConvert.SerializeObject(reply)}");

                Assert.AreEqual(request.Name, reply.Name);
                channel.ShutdownAsync().Wait();
            }
        }
    }
}
