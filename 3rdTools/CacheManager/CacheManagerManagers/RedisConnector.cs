using CacheManagers.Contract;
using CachingFramework.Redis;
using CachingFramework.Redis.Serializers;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace CacheManagers
{
    public class RedisConnector : IRedisConnector
    {
        private readonly IRedisConfiguration _redisConfiguration;

        public RedisConnector(IRedisConfiguration redisConfiguration)
        {
            _redisConfiguration = redisConfiguration;
        }

        public async Task<RedisContext> GetContext()
        {
            var option = ConfigurationOptions.Parse(_redisConfiguration.Url);
            option.Password = _redisConfiguration.Password;
            option.User = _redisConfiguration.User;
            option.ConnectRetry = _redisConfiguration.ConnectRetry;
            option.AbortOnConnectFail = _redisConfiguration.AbortConnect;
            ConnectionMultiplexer multiplexer = await ConnectionMultiplexer.ConnectAsync(option);
            return new RedisContext(multiplexer, new JsonSerializer());
            //return new RedisContext($"{_redisConfiguration.Url}, connectRetry={_redisConfiguration.ConnectRetry}, abortConnect={_redisConfiguration.AbortConnect}, allowAdmin={_redisConfiguration.AllowAdmin}",new JsonSerializer());
        }
    }
}
