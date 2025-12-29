using CustomLoggers.AuditLog;
using CustomLoggers.EmailSenders;
using log4stash.ElasticClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Extensions;

namespace CustomLoggers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomLoggers(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton(typeof(ICustomLogger<>), typeof(CustomLogger<>));
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddSingleton<IAuditlogStorage, DefaultAuditlog>();

            services.AddSingleton<IAuditLogBulkSet, AuditLogBulkSet>();
            services.AddScoped<IAuditInfo, AuditInfo>();
            services.AddSingleton<IAmbientAuditLogActionInfo, AmbientAuditLogActionInfo>();

            services.AddSingleton<IResponseValidator, ResponseValidator>();
            services.AddSingletonWithName<ISaveInElastic, SaveInElasticWithRestSharp>("restsharp");
            services.AddSingletonWithName<ISaveInElastic, SaveInElasticWithNest>("nest");

            // ElasticSearch (Enable to use ElasticSearch)
            services.AddElasticSearch(options =>
            {
                configuration.GetSection("ElasticSearchConfiguration").Bind(options);
            });
            // DefaultLoggerAuditing 
            services.AddDefaultLoggerAuditlog(options =>
            {
                configuration.GetSection("DefaultLoggerAuditlogConfiguration").Bind(options);
            });

            services.AddMailSenderConfiguration(options =>
            {
                configuration.GetSection("MailSenderConfiguration").Bind(options);
            });

            return services;
        }
    }
}
