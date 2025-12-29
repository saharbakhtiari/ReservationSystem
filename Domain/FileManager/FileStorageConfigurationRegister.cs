using Microsoft.Extensions.DependencyInjection;
using System;

namespace Domain.FileManager
{
    public static class FileStorageConfigurationRegister 
    {
        public static IServiceCollection AddFileStorageConfiguration(this IServiceCollection services,
          Action<IFileStorageConfiguration> options)
        {
            var esConfiguration = new FileStorageConfiguration();
            options(esConfiguration);
            services.AddSingleton<IFileStorageConfiguration, FileStorageConfiguration>(service => esConfiguration);
            return services;
        }
    }
}
