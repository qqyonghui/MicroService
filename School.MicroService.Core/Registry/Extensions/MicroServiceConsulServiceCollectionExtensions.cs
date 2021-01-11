using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.MicroService.Core.Registry.Extensions
{
    /// <summary>
    /// Consul注册扩展类
    /// </summary>
    public static class MicroServiceConsulServiceCollectionExtensions
    {
        /// <summary>
        /// 服务注册的扩展方法
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsulRegistry(this IServiceCollection services, IConfiguration config)
        {
            //加载微服务项目的Consul配置
            services.Configure<ServiceRegistryConfig>(config.GetSection("ConsulRegistry"));
            //
            services.AddSingleton<IConsulServiceRegistry, ConsulServiceRegistry>();
            return services;
        }
        /// <summary>
        /// 服务发现的扩展方法
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsulDiscovery(this IServiceCollection services)
        {
            //
            services.AddSingleton<IServiceDiscovery, ConsulServiceDiscovery>();
            return services;
        }
    }
}
