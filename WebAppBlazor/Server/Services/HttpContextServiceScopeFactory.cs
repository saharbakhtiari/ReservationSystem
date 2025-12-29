using Domain.Common;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebAppBlazor.Server.Services
{
    public class HttpContextServiceScopeFactory : IHybridServiceScopeFactory//, ITransientDependency
    {
        protected IHttpContextAccessor HttpContextAccessor { get; }

        protected IServiceScopeFactory ServiceScopeFactory { get; }
        
        public HttpContextServiceScopeFactory(
            IHttpContextAccessor httpContextAccessor,
            IServiceScopeFactory serviceScopeFactory)
        {
            HttpContextAccessor = httpContextAccessor;
            ServiceScopeFactory = serviceScopeFactory;
        }

        public virtual IServiceScope CreateScope()
        {
            var httpContext = HttpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return ServiceScopeFactory.CreateScope();
            }

            return new NonDisposedHttpContextServiceScope(httpContext.RequestServices);
        }

        protected class NonDisposedHttpContextServiceScope : IServiceScope
        {
            public IServiceProvider ServiceProvider { get; }

            public NonDisposedHttpContextServiceScope(IServiceProvider serviceProvider)
            {
                ServiceProvider = serviceProvider;
            }

            public void Dispose()
            {
            }
        }
    }
}