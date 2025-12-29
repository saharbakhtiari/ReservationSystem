using Domain.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Domain.ServiceLocators
{
    public class ServiceProviderProxy : IServiceProviderProxy
    {
        private readonly IServiceScope ScopeFactory;
        public IServiceProvider HostedServiceServiceProvider { get; set; }
        public ServiceProviderProxy(IServiceScope ScopeFactory)
        {
            this.ScopeFactory = ScopeFactory;
        }

        public T GetService<T>()
        {
            return ScopeFactory.ServiceProvider.GetService<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return ScopeFactory.ServiceProvider.GetServices<T>();
        }

        public object GetService(Type type)
        {
            return ScopeFactory.ServiceProvider.GetService(type);
        }

        public IEnumerable<object> GetServices(Type type)
        {
            return ScopeFactory.ServiceProvider.GetServices(type);
        }

        public IServiceProvider GetServiceProvider()
        {
            return ScopeFactory.ServiceProvider;
        }
    }
}
