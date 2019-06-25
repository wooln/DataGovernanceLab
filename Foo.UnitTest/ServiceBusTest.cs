using System;
using Library.ServiceTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZooKeeperNet;

namespace Foo.UnitTest
{
    [TestClass]
    public class ServiceBusTest
    {
        [TestMethod]
        public void RegisterTest()
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
            using (ZooKeeper zon = ServiceBus.Register(serviceInfo))
            {
                var serviceLoaded = ServiceBus.Get(serviceInfo.Key).FeelingLucky();
                Assert.AreEqual(serviceInfo.Key, serviceLoaded.Key);
            }
        }
    }
}
