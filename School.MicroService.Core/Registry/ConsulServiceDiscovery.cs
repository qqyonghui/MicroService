using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace School.MicroService.Core.Registry
{
    public class ConsulServiceDiscovery : IServiceDiscovery
    {
        private readonly IConfiguration configuration;

        public ConsulServiceDiscovery(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<IList<ServiceUrl>> Discovery(string serviceName)
        {
            //根据配置文件获取Consul的配置
            ServiceDiscoveryConfig config = configuration.GetSection("ConsulDiscovery").Get<ServiceDiscoveryConfig>();
            //创建与Consul得连接
            ConsulClient consulClient = new ConsulClient(configuration =>
            {
                configuration.Address = new Uri(config.RegistryAddress);
            });
            //查询Consul上的服务
            var queryResult = await consulClient.Catalog.Service(serviceName);
            //拼接服务的地址
            List<ServiceUrl> list = new List<ServiceUrl>();
            //循环从Consul得到得服务
            foreach (var item in queryResult.Response)
            {
                list.Add(new ServiceUrl { Url = item.Address + ":" + item.ServicePort });
            }
            return list;

        }
    }
}
