using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmsService.Contract;

namespace GhasedakSmsService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSeoSms(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCMIXConfiguration(options =>
            {
                configuration.GetSection("CMIXSettings").Bind(options);
            });
            services.AddSingleton(typeof(ISMSService), typeof(GhasedakSMSService));

            return services;
        }
    }
}
