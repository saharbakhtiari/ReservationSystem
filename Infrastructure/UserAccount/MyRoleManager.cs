using Domain.Common;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{
    public class MyRoleManager<TRole> : RoleManager<TRole> where TRole : ApplicationUserRoles
    {
        public MyRoleManager(IRoleStore<TRole> store, IEnumerable<IRoleValidator<TRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<TRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
        public Task<bool> HasPermission(string role, string permission)
        {
            return Roles.AnyAsync(x => x.NormalizedName == role.ToUpper() && x.Permissions.Any(p => p.Name.ToUpper() == permission.ToUpper()));
        }
        public Task<List<string>> GetAllPermission(Guid roleId, CancellationToken cancellationToken = default)
        {
            return (Store as MyRoleStore<TRole>).GetPermissionsAsync(roleId, cancellationToken);
        }
        public async Task<List<string>> GetAllPermissionIds(Guid roleId, CancellationToken cancellationToken = default)
        {
            var result = new List<string>();
            var permData = (Store as MyRoleStore<TRole>).GetPermissionsFullAsync(roleId, cancellationToken).Result;
            if (permData is null || permData.Count == 0)
            {
                return result;
            }
            foreach (var permissionData in permData)
              result.Add(permissionData.Id.ToString());        

            return result;
        }
        
        public async Task<List<PermissionDto>> GetAllPermissionFull(Guid roleId, CancellationToken cancellationToken = default)
        {
            var result = new List<PermissionDto>();
            var permData = (Store as MyRoleStore<TRole>).GetPermissionsFullAsync(roleId, cancellationToken).Result;
            if (permData is null || permData.Count == 0)
            {
                return result;
            }
            foreach (var permissionData in permData)
            {
                result.Add(new()
                {
                    Id = permissionData.Id,
                    Name = permissionData.Name

                });

            }
            return result;
 
        }
    }

}
