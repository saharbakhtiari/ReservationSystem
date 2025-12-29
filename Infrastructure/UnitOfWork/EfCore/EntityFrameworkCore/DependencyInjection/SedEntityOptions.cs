using Domain.UnitOfWork;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore.DependencyInjection
{
    public class SedEntityOptions<TEntity>
        where TEntity : IEntity
    {
        public static SedEntityOptions<TEntity> Empty { get; } = new SedEntityOptions<TEntity>();

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> DefaultWithDetailsFunc { get; set; }
    }

    public class SedEntityOptions
    {
        private readonly IDictionary<Type, object> _options;

        public SedEntityOptions()
        {
            _options = new Dictionary<Type, object>();
        }

        public SedEntityOptions<TEntity> GetOrNull<TEntity>()
            where TEntity : IEntity
        {
            return _options.GetOrDefault(typeof(TEntity)) as SedEntityOptions<TEntity>;
        }

        public void Entity<TEntity>(Action<SedEntityOptions<TEntity>> optionsAction)
            where TEntity : IEntity
        {
            Check.NotNull(optionsAction, nameof(optionsAction));

            optionsAction(
                _options.GetOrAdd(
                    typeof(TEntity),
                    () => new SedEntityOptions<TEntity>()
                ) as SedEntityOptions<TEntity>
            );
        }
    }
}