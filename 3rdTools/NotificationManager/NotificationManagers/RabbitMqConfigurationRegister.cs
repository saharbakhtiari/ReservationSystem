using Microsoft.Extensions.DependencyInjection;
using NotificationManagers.Contract;
using System;

namespace NotificationManagers;

public static class RabbitMqConfigurationRegister
{
    public static IServiceCollection AddQueueManager(this IServiceCollection services,
     Action<IRabbitMqConfiguration> options)
    {
        var esConfiguration = new RabbitMqConfiguration();
        options(esConfiguration);
        services.AddSingleton<IRabbitMqConfiguration, RabbitMqConfiguration>(service => esConfiguration);
        return services;
    }
}

