using Domain.UnitOfWork;
using Extensions;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore
{
    public class SedDbContextOptions
    {
        internal List<Action<SedDbContextConfigurationContext>> DefaultPreConfigureActions { get; set; }

        internal Action<SedDbContextConfigurationContext> DefaultConfigureAction { get; set; }

        internal Dictionary<Type, List<object>> PreConfigureActions { get; set; }

        internal Dictionary<Type, object> ConfigureActions { get; set; }

        public SedDbContextOptions()
        {
            DefaultPreConfigureActions = new List<Action<SedDbContextConfigurationContext>>();
            PreConfigureActions = new Dictionary<Type, List<object>>();
            ConfigureActions = new Dictionary<Type, object>();
        }

        public void PreConfigure(Action<SedDbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));

            DefaultPreConfigureActions.Add(action);
        }

        public void Configure(Action<SedDbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));

            DefaultConfigureAction = action;
        }

        public void PreConfigure<TDbContext>(Action<SedDbContextConfigurationContext<TDbContext>> action)
            where TDbContext : SedDbContext<TDbContext>
        {
            Check.NotNull(action, nameof(action));

            var actions = PreConfigureActions.GetOrDefault(typeof(TDbContext));
            if (actions == null)
            {
                PreConfigureActions[typeof(TDbContext)] = actions = new List<object>();
            }

            actions.Add(action);
        }

        public void Configure<TDbContext>(Action<SedDbContextConfigurationContext<TDbContext>> action)
            where TDbContext : SedDbContext<TDbContext>
        {
            Check.NotNull(action, nameof(action));

            ConfigureActions[typeof(TDbContext)] = action;
        }
    }
}