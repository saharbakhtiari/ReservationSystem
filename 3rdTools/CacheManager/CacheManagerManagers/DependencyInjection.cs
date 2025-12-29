using CacheManagers.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CacheManagers;

public static class DependencyInjection
{
    public static IServiceCollection AddRedisCacheManagers(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCacheManager(options =>
        {
            configuration.GetSection("RedisConfiguration").Bind(options);
        });
        services.AddTransient(typeof(IRedisManager<>), typeof(RedisManager<>));
        return services;
    }
}
