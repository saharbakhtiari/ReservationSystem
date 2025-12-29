using Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace WebAppBlazor.Server.Services
{
    public class HttpContextServiceProviderProxy : IServiceProviderProxy
    {
        private readonly IHttpContextAccessor contextAccessor;

        public IServiceProvider HostedServiceServiceProvider { get; set; }

        public HttpContextServiceProviderProxy(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        public IServiceProvider GetServiceProvider()
        {
            if (contextAccessor.HttpContext == null && HostedServiceServiceProvider != null)
            {
                return HostedServiceServiceProvider;
            }
            return contextAccessor.HttpContext.RequestServices;
        }

        public T GetService<T>()
        {
            if (contextAccessor.HttpContext == null && HostedServiceServiceProvider != null)
            {
                return HostedServiceServiceProvider.GetService<T>();
            }
            return contextAccessor.HttpContext.RequestServices.GetService<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            if (contextAccessor.HttpContext == null)
            {
                return HostedServiceServiceProvider.GetServices<T>();
            }
            return contextAccessor.HttpContext.RequestServices.GetServices<T>();
        }

        public object GetService(Type type)
        {
            if (contextAccessor.HttpContext == null && HostedServiceServiceProvider != null)
            {
                return HostedServiceServiceProvider.GetService(type);
            }
            return contextAccessor.HttpContext.RequestServices.GetService(type);
        }

        public IEnumerable<object> GetServices(Type type)
        {
            if (contextAccessor.HttpContext == null && HostedServiceServiceProvider != null)
            {
                return HostedServiceServiceProvider.GetServices(type);
            }
            return contextAccessor.HttpContext.RequestServices.GetServices(type);
        }
    }
}
