using System;
using Domain.UnitOfWork;
using JetBrains.Annotations;

namespace Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore.DependencyInjection
{
    public interface ISedDbContextRegistrationOptionsBuilder
    {
        void Entity<TEntity>(Action<SedEntityOptions<TEntity>> optionsAction)
            where TEntity : IEntity;
    }
}