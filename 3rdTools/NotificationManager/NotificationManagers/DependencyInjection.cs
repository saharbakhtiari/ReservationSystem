using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationManagers.Contract;

namespace NotificationManagers;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMqManagers(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddQueueManager(options =>
        {
            configuration.GetSection("NotifRabbitMQConfiguration").Bind(options);
        });

        var rabbitConfig = configuration.GetSection("NotifRabbitMQConfiguration");
        services.Configure<RabbitMqConfiguration>(c => rabbitConfig.Bind(c));

        // services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

        services.AddSingleton(typeof(INotificationManager<>), typeof(NotificationManager<>));
        services
           .AddRabbitMQCoreClient(configuration.GetSection("RabbitMQ"))
           .AddConsumer()
           .AddSystemTextJson(x =>
           {
               x.PropertyNamingPolicy = null;
           });
        return services;
    }
}
