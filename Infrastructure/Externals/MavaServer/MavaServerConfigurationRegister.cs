using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Externals.MavaServer
{
    public static class MavaServerConfigurationRegister
    {
        public static IServiceCollection AddMavaServerConfiguration(this IServiceCollection services,
            Action<IMavaServerConfiguration> options)
        {
            var esConfiguration = new MavaServerConfiguration();

            options(esConfiguration);

            services.AddSingleton<IMavaServerConfiguration, MavaServerConfiguration>(service => esConfiguration);

            return services;
        }


    }
}
