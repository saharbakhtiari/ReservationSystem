using System;
using Domain.UnitOfWork;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using JetBrains.Annotations;

namespace Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore.DependencyInjection
{
    public class SedDbContextConfigurerAction : ISedDbContextConfigurer
    {

        public Action<SedDbContextConfigurationContext> Action { get; }

        public SedDbContextConfigurerAction(Action<SedDbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));

            Action = action;
        }

        public void Configure(SedDbContextConfigurationContext context)
        {
            Action.Invoke(context);
        }
    }

    public class SedDbContextConfigurerAction<TDbContext> : SedDbContextConfigurerAction
        where TDbContext : SedDbContext<TDbContext>
    {
        public SedDbContextConfigurerAction(Action<SedDbContextConfigurationContext> action)
            : base(action)
        {
        }
    }
}