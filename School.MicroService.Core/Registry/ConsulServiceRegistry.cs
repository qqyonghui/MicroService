using Consul;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.MicroService.Core.Registry
{
    public class ConsulServiceRegistry : IConsulServiceRegistry
    {
        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="registryConfig"></param>
        public void Deregister(ServiceRegistryConfig registryConfig)
        {
            //创建Consul连接
            var consulClient = new ConsulClient(config =>
            {
                //建立客户端和服务端的连接
                config.Address = new Uri(registryConfig.RegistryAddress);
            });
            //注销该服务,根据服务的Id进行注销
            consulClient.Agent.ServiceDeregister(registryConfig.Id);
            //注销后关闭当前连接
            consulClient.Dispose();
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="registryConfig"></param>
        public void Registry(ServiceRegistryConfig registryConfig)
        {
            //创建客户端
            var consulClient = new ConsulClient(config =>
            {
                config.Address = new Uri(registryConfig.RegistryAddress);
            });
            //创建consul注册对象
            var registration = new AgentServiceRegistration()
            {
                ID = registryConfig.Id,
                Address = registryConfig.Address,
                Name = registryConfig.Name,
                Port = registryConfig.Port,
                Tags = registryConfig.Tags,
                //健康检查设置
                Check = new AgentServiceCheck()
                {
                    // 3.1、consul健康检查超时间
                    Timeout = TimeSpan.FromSeconds(10),
                    // 3.2、服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 3.3、consul健康检查地址
                    HTTP = registryConfig.HealthCheckAddress,
                    // 3.4 consul健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(10),
                }

            };
            //注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();
            //注册完毕后关闭连接
            consulClient.Dispose();
        }
    }
}
