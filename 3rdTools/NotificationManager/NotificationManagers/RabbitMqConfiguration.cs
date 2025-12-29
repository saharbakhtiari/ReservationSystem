using NotificationManagers.Contract;

namespace NotificationManagers
{
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }

    }
}