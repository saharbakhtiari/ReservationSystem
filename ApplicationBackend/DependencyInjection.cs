using Application.Common.QueueManagers;
using Application_Backend.Common.Behaviours;
using Application_Backend.Rules.Commands.AddRuleVersion;
using Domain.Books;
using Domain.Common;
using Domain.MemberProfiles;
using Domain.SeoSms;
using Domain.Settings;
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
            services.AddTransient(typeof(IBookDomainService), typeof(BookDomainService));

            services.AddTransient(typeof(IMemberProfileDomainService), typeof(MemberProfileDomainService));
            services.AddTransient(typeof(ISeoSmsDomainService), typeof(SeoSmsDomainService));

            //services.AddSingleton<IJobService, Notify>();
            services.AddSingleton(typeof(IQueueManager<>), typeof(BufferQueueManager<>));
            return services;
        }
    }
}
