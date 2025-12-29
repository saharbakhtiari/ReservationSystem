using System;
using System.Collections.Generic;
using Domain.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore.DependencyInjection
{
    public class SedDbContextRegistrationOptions : ISedDbContextRegistrationOptionsBuilder
    {
        public Dictionary<Type, object> SedEntityOptions { get; }
        public IServiceCollection Services { get; }
        public SedDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
        {
            SedEntityOptions = new Dictionary<Type, object>();
            Services = services;
        }

        public void Entity<TEntity>(Action<SedEntityOptions<TEntity>> optionsAction) where TEntity : IEntity
        {
            Services.Configure<SedEntityOptions>(options =>
            {
                options.Entity(optionsAction);
            });
        }
    }
}