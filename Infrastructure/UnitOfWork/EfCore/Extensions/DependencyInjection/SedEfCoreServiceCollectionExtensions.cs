using System;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.UnitOfWork.EfCore.Extensions.DependencyInjection
{
    public static class SedEfCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddSedDbContext<TDbContext>(
            this IServiceCollection services,
            Action<ISedDbContextRegistrationOptionsBuilder> optionsBuilder = null)
            where TDbContext : SedDbContext<TDbContext>
        {
            services.AddMemoryCache();

            var options = new SedDbContextRegistrationOptions(typeof(TDbContext), services);
            optionsBuilder?.Invoke(options);

            services.TryAddTransient(DbContextOptionsFactory.Create<TDbContext>);

            return services;
        }
    }
}
