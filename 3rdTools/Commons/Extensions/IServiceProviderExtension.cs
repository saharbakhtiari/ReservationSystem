using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Extensions
{
    public class ServiceDefinition
    {
        public Type Service { get; set; }
        public Type Implementation { get; set; }
        public string Name { get; set; }

    }
    public static class IServiceProviderExtension
    {
        private static List<ServiceDefinition> serviceDefinition = new();
        public static IServiceCollection AddTransientWithName<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceCollection services, string name)
            where TService : class
            where TImplementation : class, TService
        {
            if (!serviceDefinition.Any(x => x.Service == typeof(TService) && x.Name == name))
            {
                serviceDefinition.Add(new()
                {
                    Implementation = typeof(TImplementation),
                    Service = typeof(TService),
                    Name = name
                });
            }
            else
            {
                throw new Exception("Name is repeated");
            }
            return services.AddTransient<TService, TImplementation>().AddTransient<TImplementation>();
        }
        public static IServiceCollection AddSingletonWithName<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceCollection services, string name)
            where TService : class
            where TImplementation : class, TService
        {
            if (!serviceDefinition.Any(x => x.Service == typeof(TService) && x.Name == name))
            {
                serviceDefinition.Add(new()
                {
                    Implementation = typeof(TImplementation),
                    Service = typeof(TService),
                    Name = name
                });
            }
            else
            {
                throw new Exception("Name is repeated");
            }
            return services.AddSingleton<TService, TImplementation>().AddSingleton<TImplementation>();
        }
        public static TService GetServiceWithName<TService>(this IServiceProvider services, string name)
            where TService : class
        {
            if (serviceDefinition.Any(x => x.Service == typeof(TService) && x.Name == name))
            {
                return services.GetService(serviceDefinition.First(x => x.Service == typeof(TService) && x.Name == name).Implementation) as TService;
            }
            return null;
        }
    }
}
