using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rabbit.Zookeeper;
using Rabbit.Zookeeper.Implementation;

namespace Library.ServiceTool
{
    public class ServiceBus
    {
        private const string Root = "/service_register";
        private static readonly Dictionary<string, ServiceInformationCollection> ServiceInformationDictionary = new Dictionary<string, ServiceInformationCollection>();
        private static IZookeeperClient _zooKeeper = null;

        public static async Task<IZookeeperClient> Register(ServiceInformation service)
        { 
            IZookeeperClient zk = GetZooKeeper();
            await CheckOrCreateRoot(zk);

            string servicesPath = $"{Root}/{service.Key}";

            //todo 先获取后追加
            if (!await zk.ExistsAsync(servicesPath))
            {
                await zk.CreatePersistentAsync(servicesPath, GetData(string.Empty));
            }

            string serviceNodePaht = $"{servicesPath}/{service.Host}:{service.Port}";
            await zk.CreateEphemeralAsync(serviceNodePaht, GetData(service));
            return zk;
        }

        public static async Task<ServiceInformationCollection> Get(string key)
        {
            return await LoadServiceInformation($"{Root}/{key}");
            //return ServiceInformationDictionary[key];
        }


        #region private

        private static async Task<ServiceInformationCollection> LoadServiceInformation(string servicesPath)
        {
            var zk = GetZooKeeper();
            IEnumerable<string> nodes  =  await zk.GetChildrenAsync(servicesPath);

            var result = new ServiceInformationCollection();
            foreach(string nodeName in nodes)
            {
                string nodePath = $"{servicesPath}/{nodeName}";
                byte[] data = (await zk.GetDataAsync(nodePath)).ToArray();

                string json = Encoding.UTF8.GetString(data);
                var serviceInformation = JsonConvert.DeserializeObject<ServiceInformation>(json);
                result.Add(serviceInformation);
            }
            return result;
        }

        private static byte[] GetData(object graph)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(graph));
        }

        private static IZookeeperClient GetZooKeeper()
        {
            if (_zooKeeper == null)
            {
                _zooKeeper = new ZookeeperClient(new ZookeeperClientOptions("127.0.0.1:2181"));
            }
            return _zooKeeper;
        }

        private static async Task CheckOrCreateRoot(IZookeeperClient zk)
        {
            bool exists = await zk.ExistsAsync(Root);
            if (!exists)
            {
                await zk.CreatePersistentAsync(Root, GetData("this is service register root"));
            }
        }
        #endregion
    }

    public class ServiceInformationCollection : List<ServiceInformation>
    {
        public ServiceInformationCollection() { }
        public ServiceInformationCollection(IEnumerable<ServiceInformation> items)
        {
            this.AddRange(items);
        }

        public ServiceInformation FeelingLucky()
        {
            //todo
            return this.FirstOrDefault();
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
