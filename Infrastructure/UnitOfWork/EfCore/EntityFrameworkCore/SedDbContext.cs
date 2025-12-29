using Domain.UnitOfWork.Uow;
using Extensions;
using Infrastructure.UserAccount;
using Infrastructure.UserAccount.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore
{
    public abstract class SedDbContext<TDbContext> : IdentityWithPermisionDbContext<ApplicationUser, ApplicationUserRoles, ApplicationPermission, Guid>, ISedEfCoreDbContext //KeyApiAuthorizationDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, ISedEfCoreDbContext
        where TDbContext : DbContext
    {
        public IUnitOfWorkManager UnitOfWorkManager { get; set; }

        protected SedDbContext(DbContextOptions<TDbContext> options)
           : base(options)
        {
        }
        public virtual void Initialize(SedEfCoreDbContextInitializationContext initializationContext)
        {
            if (initializationContext.UnitOfWork.Options.Timeout.HasValue &&
                Database.IsRelational() &&
                !Database.GetCommandTimeout().HasValue)
            {
                Database.SetCommandTimeout(initializationContext.UnitOfWork.Options.Timeout.Value.TotalSeconds.To<int>());
            }

            ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;

        }

    }
}