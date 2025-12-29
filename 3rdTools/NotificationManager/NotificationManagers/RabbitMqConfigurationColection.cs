using NotificationManagers.Contract;
using System.Collections.Generic;

namespace NotificationManagers;

public class RabbitMqConfigurationColection : IRabbitMqConfigurationColection
{
    public IEnumerable<IRabbitMqConfiguration> QueueManagerConfigurations { get; set; }
}
