using Microsoft.Extensions.DependencyInjection;

namespace GhasedakSmsService
{
    public static class AppSettingsCMIXRegister 
    {
        public static IServiceCollection AddCMIXConfiguration(this IServiceCollection services,
           Action<IAppSettingCMIX> options)
        {
            var esConfiguration = new AppSettingsCMIX();

            options(esConfiguration);

            services.AddSingleton<IAppSettingCMIX, AppSettingsCMIX>(service => esConfiguration);

            return services;
        }
    }
}
