using System;
using System.Threading.Tasks;

namespace CacheManagers.Contract;

public interface IRedisManager<T> where T : class
{
    Task AddAsync(string key, T message, TimeSpan? timespan);
    Task<T> GetAsync(string key);
    Task RemoveAsync(string key);
}
