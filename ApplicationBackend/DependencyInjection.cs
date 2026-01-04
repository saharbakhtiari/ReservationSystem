using Application.Common.QueueManagers;
using Application_Backend.Common.Behaviours;
using Application_Backend.Common.Notifications;
using Application_Backend.Rules.Commands.AddRuleVersion;
using Domain.Cartables;
using Domain.Common;
using Domain.Externals.CMRServer;
using Domain.Externals.MavaServer;
using Domain.Externals.NotifyServer;
using Domain.MemberProfiles;
using Domain.SeoSms;
using Domain.Settings;
using Domain.SliderFiles;
using Domain.Sliders;
using Domain.UnitOfWork.Uow;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application_Backend
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationBackEnd(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));



            services.AddSingleton(typeof(IUnitOfWorkManager), typeof(UnitOfWorkManager));
            services.AddSingleton(typeof(IAmbientUnitOfWork), typeof(AmbientUnitOfWork));
            services.AddSingleton(typeof(IConnectionStringResolver), typeof(DefaultConnectionStringResolver));
            services.AddSingleton(typeof(IElapsedTimeManager), typeof(ElapsedTimeManager));
            services.AddTransient(typeof(IElapsedTime<>), typeof(ElapsedTime<>));
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddTransient(typeof(ISettingDomainService), typeof(SettingDomainService));

            services.AddTransient(typeof(IMemberProfileDomainService), typeof(MemberProfileDomainService));
            services.AddTransient(typeof(ISeoSmsDomainService), typeof(SeoSmsDomainService));
            services.AddTransient(typeof(ISliderDomainService), typeof(SliderDomainService));
            services.AddTransient(typeof(ISliderFileDomainService), typeof(SliderFileDomainService));
            services.AddTransient(typeof(INotificationDomainService), typeof(NotificationDomainService));
            services.AddTransient(typeof(ICMRRequestDomainService), typeof(CMRRequestDomainService));
            services.AddTransient(typeof(IMavaRequestDomainService), typeof(MavaRequestDomainService));
            services.AddTransient(typeof(ICartableDomainService), typeof(CartableDomainService));
            services.AddNotificationConfig(options =>
            {
                configuration.GetSection("Notification").Bind(options);
            });

            //services.AddSingleton<IJobService, Notify>();
            services.AddSingleton(typeof(IQueueManager<>), typeof(BufferQueueManager<>));
            return services;
        }
    }
}
