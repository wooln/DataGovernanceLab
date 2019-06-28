using System.Threading.Tasks;
using Library.ServiceTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foo.Test
{
    [TestClass]
    public class ServiceBusTest
    {
        [TestMethod]
        public async Task RegisterTest()
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
            
            using (var zon = await ServiceBus.Register(serviceInfo))
            {
                var serviceLoaded = (await ServiceBus.Get(serviceInfo.Key)).FeelingLucky();
                Assert.AreEqual(serviceInfo.Key, serviceLoaded.Key);
            }
        }
    }
}
