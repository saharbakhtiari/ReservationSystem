namespace NotificationManagers.Contract;

public interface IRabbitMqConfiguration
{
    string Exchange { get; set; }
    string RoutingKey { get; set; }
}
