using CacheManagers.Contract;
using CachingFramework.Redis;
using CustomLoggers;
using System;
using System.Threading.Tasks;

namespace CacheManagers;

public class RedisManager<T> : IRedisManager<T> where T : class
{
    private readonly ICustomLogger<RedisManager<T>> _logger;
    private readonly IRedisConnector _connector;
    public RedisManager(ICustomLogger<RedisManager<T>> logger, IRedisConnector connector)
    {
        _logger = logger;
        _connector = connector;
    }
    public async Task AddAsync(string key, T message, TimeSpan? timespan)
    {
        var cache = (await _connector.GetContext()).Cache;
        await cache.SetObjectAsync(key, message, timespan);
    }

    public async Task RemoveAsync(string key)
    {
        var cache = (await _connector.GetContext()).Cache;
        await cache.RemoveAsync(key);
    }

    public async Task<T> GetAsync(string key)
    {
        var cache = (await _connector.GetContext()).Cache;
        return await cache.GetObjectAsync<T>(key);
    }
}