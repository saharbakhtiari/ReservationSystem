using Domain.Common;
using Domain.Users;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{
    public class MyRoleStore<TRole> : RoleStore<TRole, ApplicationDbContext, Guid>, IRoleStore<TRole>
        where TRole : ApplicationUserRoles
    {
        public MyRoleStore(IDbContextProvider<ApplicationDbContext> context, IdentityErrorDescriber describer = null) : base(context.GetDbContext(), describer)
        {
        }
        public Task<List<string>> GetPermissionsAsync(Guid roleId, CancellationToken cancellationToken = default)
        {
            return Roles.AsNoTracking().Include(r => r.Permissions).Where(x => x.Id == roleId)
                        .SelectMany(x => x.Permissions).Select(p => p.Name)
                        .ToListAsync(cancellationToken);
        }
        public Task<List<ApplicationPermission>> GetPermissionsFullAsync(Guid roleId, CancellationToken cancellationToken = default)
        {
            return Roles.AsNoTracking().Include(r => r.Permissions).Where(x => x.Id == roleId)
                        .SelectMany(x => x.Permissions)
                        .ToListAsync(cancellationToken);
        }
    }
}
