using Domain.Roles;
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
    public class MyUserStore<TUser> : UserStore<TUser, ApplicationUserRoles, ApplicationDbContext, Guid>, IUserStore<TUser>
     where TUser : IdentityUser<Guid>
    {
        private DbSet<IdentityUserRole<Guid>> _userRoles => Context.Set<IdentityUserRole<Guid>>();
        private DbSet<ApplicationUserRoles> _roleStore => Context.Set<ApplicationUserRoles>();
        public MyUserStore(IDbContextProvider<ApplicationDbContext> context, IdentityErrorDescriber describer = null) : base(context.GetDbContext(), describer)
        {
        }
        public async Task<List<RoleDto>> GetRolesAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var result = new List<RoleDto>();
            var roleData = _userRoles.AsNoTracking().Where(x => x.UserId.Equals(userId))
                        .Join(_roleStore, x => x.RoleId, x => x.Id, (userRole, roleStore) => roleStore).ToListAsync(cancellationToken).Result;
            if (roleData is null || roleData.Count == 0)
            {
                return result;
            }
            foreach (var role in roleData)
            {
                result.Add(new()
                {
                    Id = role.Id,
                    Name = role.Name

                });

            }
            return result;
        }
        public Task<List<string>> GetPermissionsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return _userRoles.AsNoTracking().Where(x => x.UserId.Equals(userId))
                        .Join(_roleStore, x => x.RoleId, x => x.Id, (userRole, roleStore) => roleStore)
                        .SelectMany(x=>x.Permissions).Select(x=>x.Name).Distinct()
                        .ToListAsync(cancellationToken);
        }
        public Task<List<string>> GetPermissionIdsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return _userRoles.AsNoTracking().Where(x => x.UserId.Equals(userId))
                        .Join(_roleStore, x => x.RoleId, x => x.Id, (userRole, roleStore) => roleStore)
                        .SelectMany(x => x.Permissions).Select(x => x.Id.ToString()).Distinct()
                        .ToListAsync(cancellationToken);
        }
        
        public Task<List<ApplicationPermission>> GetPermissionsFullAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return _userRoles.AsNoTracking().Where(x => x.UserId.Equals(userId))
                        .Join(_roleStore, x => x.RoleId, x => x.Id, (userRole, roleStore) => roleStore)
                        .SelectMany(x => x.Permissions).Distinct()
                        .ToListAsync(cancellationToken);
        }
        public Task<bool> CheckPermissionAsync(Guid userId,string permission ,CancellationToken cancellationToken = default)
        {
            return _userRoles.AsNoTracking().Where(x => x.UserId.Equals(userId))
                        .Join(_roleStore, x => x.RoleId, x => x.Id, (userRole, roleStore) => roleStore)
                        .SelectMany(x => x.Permissions).AnyAsync(x => x.Name.ToUpper() == permission.ToUpper(), cancellationToken);
        }
    }

}
