namespace CacheManagers.Contract;

public interface IRedisConfiguration
{
    string Url { get; set; }
    int ConnectRetry { get; set; }
    bool AbortConnect { get; set; }
    bool AllowAdmin { get; set; }
    string User { get; set; }
    string Password { get; set; }
}
