using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CustomLoggers.AuditLog;

public static class DefaultLoggerAuditlogRegistrar
{
    public static IServiceCollection AddDefaultLoggerAuditlog(this IServiceCollection services,
        Action<IDefaultLoggerAuditlogConfiguration> options)
    {
        var dlaConfiguration = new DefaultLoggerAuditlogConfiguration();

        options(dlaConfiguration);

        if (dlaConfiguration.UseAuditingLog)
        {
            var descriptor = new ServiceDescriptor(
                    typeof(IAuditlogStorage),
                    typeof(DefaultLoggerAuditlog),
                    ServiceLifetime.Singleton);
            services.Replace(descriptor);
        }

        return services;
    }
}

