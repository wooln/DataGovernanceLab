using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using org.apache.zookeeper;
using org.apache.zookeeper.data;
using Rabbit.Zookeeper;
using Rabbit.Zookeeper.Implementation;

namespace Library.ServiceTool
{
    public class ServiceBus
    {
        private const string Root = "/service_register";
        private static readonly Dictionary<string, ServiceInformationCollection> ServiceInformationDictionary = new Dictionary<string, ServiceInformationCollection>();

        private static IZookeeperClient _zooKeeper = null;

        private static IZookeeperClient GetZooKeeper()
        {
            if (_zooKeeper == null)
            {
                _zooKeeper = new ZookeeperClient(new ZookeeperClientOptions("127.0.0.1:2181"));
            }
            return _zooKeeper;
        }

        public static async Task<IZookeeperClient> Register(ServiceInformation service)
        {
            //创建一个Zookeeper实例，第一个参数为目标服务器地址和端口，第二个参数为Session超时时间，第三个为节点变化时的回调方法 
            IZookeeperClient zk = GetZooKeeper();
            bool exists = await zk.ExistsAsync(Root);
            if (!exists)
            {
                await zk.CreatePersistentAsync(Root, GetData("this is service register root"));
            }

            string path = $"{Root}/{service.Key}";
            //todo 先获取后追加
            if (await zk.ExistsAsync(path))
            {
                await zk.DeleteAsync(path);
            }

            //创建一个child znode,数据为服务信息,不进行ACL权限控制，节点为暂时的 
            await zk.CreateEphemeralAsync(path, GetData(new ServiceInformationCollection() { service }));
            return zk;
        }

        public static async Task<ServiceInformationCollection> Get(string key)
        {
            return await LoadServiceInformation($"{Root}/{key}");
            //return ServiceInformationDictionary[key];
        }

        private static async Task<ServiceInformationCollection> LoadServiceInformation(string path)
        {
            var zk = GetZooKeeper();
            byte[] data = (await zk.GetDataAsync(path)).ToArray();

            string json = Encoding.UTF8.GetString(data);
            var serviceInformation = JsonConvert.DeserializeObject<ServiceInformationCollection>(json);
            return serviceInformation;
        }

        private static byte[] GetData(object graph)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(graph));
        }
    }

    public class ServiceInformationCollection : List<ServiceInformation>
    {
        public ServiceInformation FeelingLucky()
        {
            //todo
            return this.First();
        }
    }

    public class ServiceInformation
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ServiceType ServiceType { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public enum ServiceType
    {
        Grpc = 1,
    }
}
