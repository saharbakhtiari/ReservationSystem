using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public interface IServiceProviderProxy
    {
        IServiceProvider HostedServiceServiceProvider { get; set; }
        IServiceProvider GetServiceProvider();
        T GetService<T>();
        IEnumerable<T> GetServices<T>();
        object GetService(Type type);
        IEnumerable<object> GetServices(Type type);
    }
}
