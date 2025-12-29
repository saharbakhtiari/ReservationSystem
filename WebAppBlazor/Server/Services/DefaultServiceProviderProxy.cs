using Domain.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace WebAppBlazor.Server.Services
{
    public class DefaultServiceProviderProxy : IServiceProviderProxy
    {
        private readonly IServiceProvider contextAccessor;
        public IServiceProvider HostedServiceServiceProvider { get; set; }
        public DefaultServiceProviderProxy(IServiceProvider contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        public IServiceProvider GetServiceProvider()
        {
            return contextAccessor;
        }

        public T GetService<T>()
        {
            return contextAccessor.GetService<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return contextAccessor.GetServices<T>();
        }

        public object GetService(Type type)
        {
            return contextAccessor.GetService(type);
        }

        public IEnumerable<object> GetServices(Type type)
        {
            return contextAccessor.GetServices(type);
        }
    }
}
