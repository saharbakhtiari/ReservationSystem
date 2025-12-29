using System.Collections.Generic;

namespace NotificationManagers.Contract;

public interface IRabbitMqConfigurationColection
{
    IEnumerable<IRabbitMqConfiguration> QueueManagerConfigurations { get; set; }
}
