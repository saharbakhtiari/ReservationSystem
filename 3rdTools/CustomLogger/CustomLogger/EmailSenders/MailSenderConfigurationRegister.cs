using Microsoft.Extensions.DependencyInjection;
using System;

namespace CustomLoggers.EmailSenders
{
    public static class MailSenderConfigurationRegister
    {
        public static IServiceCollection AddMailSenderConfiguration(this IServiceCollection services,
            Action<IMailSenderConfiguration> options)
        {
            var esConfiguration = new MailSenderConfiguration();

            options(esConfiguration);

            services.AddSingleton<IMailSenderConfiguration, MailSenderConfiguration>(service => esConfiguration);

            return services;
        }
    }
}