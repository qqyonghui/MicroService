using System;
using System.Collections.Generic;
using System.Text;

namespace School.MicroService.Core.Registry
{
    public interface IConsulServiceRegistry
    {
        //注册服务
        void Registry(ServiceRegistryConfig registryConfig);

        //注销服务
        void Deregister(ServiceRegistryConfig registryConfig);
    }
}
