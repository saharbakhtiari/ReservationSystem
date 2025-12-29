using CacheManagers.Contract;

namespace CacheManagers
{
    public class RedisConfiguration : IRedisConfiguration
    {
        public string Url { get; set; }
        public int ConnectRetry { get; set; }
        public bool AbortConnect { get; set; }
        public bool AllowAdmin { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}