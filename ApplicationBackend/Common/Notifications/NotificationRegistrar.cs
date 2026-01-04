using Application.Common.Notifications;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Application_Backend.Common.Notifications;

public static class NotificationRegistrar
{
    public static IServiceCollection AddNotificationConfig(this IServiceCollection services,
        Action<INotificationConfiguration> options)
    {
        var configuration = new NotificationConfiguration();

        options(configuration);

        services.AddSingleton<INotificationConfiguration, NotificationConfiguration>(services => configuration);
        return services;
    }
}

