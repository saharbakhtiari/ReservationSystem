using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Infrastructure.UserAccount.Permission;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{
    public class MyPermissionStore : PermissionStore<Guid, ApplicationUserRoles, ApplicationUser, ApplicationDbContext, ApplicationPermission>
    {
        public MyPermissionStore(IDbContextProvider<ApplicationDbContext> context) : base(context.GetDbContext())
        {
        }
        public override Task CreateAsync(ApplicationPermission permission, CancellationToken cancellationToken = default)
        {
            if (permission != null && (permission.Id) == Guid.Empty)
            {
                permission.Id = Guid.NewGuid();
            }
            return base.CreateAsync(permission, cancellationToken);
        }
    
    }
}
