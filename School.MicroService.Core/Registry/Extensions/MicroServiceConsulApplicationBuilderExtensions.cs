using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.MicroService.Core.Registry.Extensions
{
    /// <summary>
    /// 微服务注册发现扩展类
    /// </summary>
    public static class MicroServiceConsulApplicationBuilderExtensions
    {
        public static IApplicationBuilder UserConsulConfig(this IApplicationBuilder app)
        {
            //从IOC容器中获取consul的配置
            var serviceNode = app.ApplicationServices.GetRequiredService<IOptions<ServiceRegistryConfig>>().Value;

            //获取应用程序的生命周期
            var lifeTime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            //获取注册服务的实例
            var serviceRegistry = app.ApplicationServices.GetRequiredService<IConsulServiceRegistry>();
            //获取服务的地址
            var features = app.Properties["server.Features"] as FeatureCollection;
            var address = features.Get<IServerAddressesFeature>().Addresses.First();
            //var address = "https://localhost:5004";
            var uri = new Uri(address);
            //注册服务
            serviceNode.Id = Guid.NewGuid().ToString();
            serviceNode.Address = $"{uri.Scheme}://{uri.Host}";
            serviceNode.Port = uri.Port;
            serviceNode.HealthCheckAddress = $"{uri.Scheme}://{uri.Host}:{uri.Port}{serviceNode.HealthCheckAddress}";
            serviceRegistry.Registry(serviceNode);
            //服务器关闭时注销服务
            lifeTime.ApplicationStopping.Register(() =>
            {
                serviceRegistry.Deregister(serviceNode);
            });

            return app;
        }
    }
}
