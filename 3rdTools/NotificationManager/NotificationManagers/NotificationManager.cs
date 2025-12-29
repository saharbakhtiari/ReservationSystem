using CustomLoggers;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using NotificationManagers.Contract;
using RabbitMQCoreClient;
using System;
using System.Threading.Tasks;

namespace NotificationManagers;

public class NotificationManager<T> : INotificationManager<T> where T : class
{
    private readonly IRabbitMqConfiguration _rabbitConfig;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ICustomLogger<NotificationManager<T>> _logger;

    public NotificationManager(IRabbitMqConfiguration rabbitConfig, IServiceScopeFactory serviceScopeFactory, ICustomLogger<NotificationManager<T>> logger)
    {
        _rabbitConfig = rabbitConfig;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async ValueTask Publish(T message)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var _rabbitQueueManager = services.GetService<IQueueService>();
                await _rabbitQueueManager.SendAsync(message, _rabbitConfig.RoutingKey, _rabbitConfig.Exchange);
            }
            catch(Exception ex) 
            {
                _ = _logger.LogError($"Publish in rabbit mq raised error :{ex.ToJson()}");
            }
        }
             
    }
}