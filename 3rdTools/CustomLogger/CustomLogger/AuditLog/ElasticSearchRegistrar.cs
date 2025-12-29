using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CustomLoggers.AuditLog;

public static class ElasticSearchRegistrar
{
    public static IServiceCollection AddElasticSearch(this IServiceCollection services,
        Action<IElasticSearchConfiguration> options)
    {
        var esConfiguration = new ElasticSearchConfiguration();

        options(esConfiguration);

        if (esConfiguration.UseAuditingLog && string.IsNullOrWhiteSpace(esConfiguration.AuditingLogIndexName))
            throw new ArgumentNullException(nameof(esConfiguration.AuditingLogIndexName));

        services.AddSingleton<IElasticSearchConfiguration, ElasticSearchConfiguration>(service => esConfiguration);

        if (esConfiguration.UseAuditingLog)
        {
            var descriptor = new ServiceDescriptor(
                    typeof(IAuditlogStorage),
                    typeof(ElasticAuditlog),
                    ServiceLifetime.Singleton);
            services.Replace(descriptor);
        }

        return services;
    }
}

