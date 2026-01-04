using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Externals.CMRServer
{
    public static class CMRServerConfigurationRegister
    {
        public static IServiceCollection AddCMRServerConfiguration(this IServiceCollection services,
            Action<ICMRServerConfiguration> options)
        {
            var esConfiguration = new CMRServerConfiguration();

            options(esConfiguration);

            services.AddSingleton<ICMRServerConfiguration, CMRServerConfiguration>(service => esConfiguration);

            return services;
        }


    }
}
