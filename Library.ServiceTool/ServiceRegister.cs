using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Org.Apache.Zookeeper.Data;
using ZooKeeperNet;

namespace Library.ServiceTool
{
    public class ServiceBus
    {
        class InnerWatcher : IWatcher
        {
            public void Process(WatchedEvent @event)
            {
                switch (@event.Type)
                {
                    case EventType.None:
                        break;
                    case EventType.NodeCreated:
                        break;
                    case EventType.NodeDeleted:
                        break;
                    case EventType.NodeDataChanged:
                        break;
                    case EventType.NodeChildrenChanged:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        private const string Root = "/service_register";
        private static readonly InnerWatcher Watcher = new InnerWatcher();
        private static readonly Dictionary<string, ServiceInformationCollection> ServiceInformationDictionary = new Dictionary<string, ServiceInformationCollection>();

        private static ZooKeeper _zooKeeper = null;

        private static ZooKeeper GetZooKeeper()
        {
            if (_zooKeeper == null)
            {
                _zooKeeper = new ZooKeeper("127.0.0.1:2181", new TimeSpan(0, 0, 0, 50000), new InnerWatcher());
            }
            return _zooKeeper;
        }

        public static ZooKeeper Register(ServiceInformation service)
        {
            //创建一个Zookeeper实例，第一个参数为目标服务器地址和端口，第二个参数为Session超时时间，第三个为节点变化时的回调方法 
            var zk = GetZooKeeper();
            Stat stat = zk.Exists(Root, true);

            ////创建一个节点root，数据是mydata,不进行ACL权限控制，节点为永久性的(即客户端shutdown了也不会消失)
            //zk.Create(Root, "mydata".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);

            string path = $"{Root}/{service.Key}";
            //todo 先获取后追加
            try
            {
                zk.Delete(path, -1);
            }
            catch (Exception e)
            {

            }
            //在root下面创建一个child znode,数据为服务信息,不进行ACL权限控制，节点为暂时的 
            zk.Create(path, JsonConvert.SerializeObject(new ServiceInformationCollection() { service }).GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Ephemeral);

            return zk;
        }

        public static ServiceInformationCollection Get(string key)
        {
            return LoadServiceInformation($"{Root}/{key}");
            //return ServiceInformationDictionary[key];
        }

        private static ServiceInformationCollection LoadServiceInformation(string path)
        {
            var zk = GetZooKeeper();
            byte[] data = zk.GetData(path, false, null);

            string json = Encoding.UTF8.GetString(data);
            var serviceInformation = JsonConvert.DeserializeObject<ServiceInformationCollection>(json);
            return serviceInformation;
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
