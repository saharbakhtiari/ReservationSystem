using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Externals.NotifyServer
{
    public static class NotifyServerConfigurationRegister
    {
        public static IServiceCollection AddNotifyServerConfiguration(this IServiceCollection services,
            Action<INotifyServerConfiguration> options)
        {
            var esConfiguration = new NotifyServerConfiguration();

            options(esConfiguration);

            services.AddSingleton<INotifyServerConfiguration, NotifyServerConfiguration>(service => esConfiguration);

            return services;
        }


    }
}
