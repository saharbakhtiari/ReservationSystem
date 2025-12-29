using CacheManagers.Contract;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CacheManagers;

public static class RedisConfigurationRegister
{
    public static IServiceCollection AddCacheManager(this IServiceCollection services,
     Action<IRedisConfiguration> options)
    {
        var esConfiguration = new RedisConfiguration();
        options(esConfiguration);
        services.AddSingleton<IRedisConfiguration, RedisConfiguration>(service => esConfiguration);
        services.AddScoped<IRedisConnector, RedisConnector>();
        return services;
    }
}

