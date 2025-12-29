using CachingFramework.Redis;
using System.Threading.Tasks;

namespace CacheManagers.Contract
{
    public interface IRedisConnector
    {
        Task<RedisContext> GetContext();
    }
}
