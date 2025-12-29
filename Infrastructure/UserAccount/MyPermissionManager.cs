using Domain.Common;
using Infrastructure.Persistence;
using Infrastructure.UserAccount.Permission;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{
    public class MyPermissionManager : PermissionManager<Guid, ApplicationUser, ApplicationUserRoles, ApplicationPermission, ApplicationDbContext>
    {
        private readonly MyPermissionStore _permissionStore;

        public MyPermissionManager(MyRoleManager<ApplicationUserRoles> roleManager , MyPermissionStore permissionStore) : base(roleManager, permissionStore)
        {
            _permissionStore = permissionStore;
        }
        public Task CreateAsync(ApplicationPermission permission, CancellationToken cancellationToken = default)
        {
            return _permissionStore.CreateAsync(permission, cancellationToken);
        }
        public async Task CreateAsync( [NotNull] string permissionName, CancellationToken cancellationToken = default)
        {
            if (permissionName == null)
                throw new ArgumentNullException(nameof(permissionName));
            
            var permission = new ApplicationPermission(permissionName);

            await _permissionStore.CreateAsync(permission, cancellationToken);
        }
        public Task<List<ApplicationPermission>> GetNotExistPermission([NotNull] List<string> permissionsName, CancellationToken cancellationToken = default)
        {
            if (permissionsName is null)
                throw new ArgumentNullException(nameof(permissionsName));

            return _permissionStore.GetNotExistPermission(permissionsName, cancellationToken);
        }
        public Task<List<ApplicationPermission>> GetAllPermission( CancellationToken cancellationToken = default)
        {
           
            return _permissionStore.GetAllPermissions( cancellationToken);
        }
        public Task DeleteNotExistPermission([NotNull] List<string> permissionsName, CancellationToken cancellationToken = default)
        {
            if (permissionsName is null)
                throw new ArgumentNullException(nameof(permissionsName));

            return _permissionStore.DeleteNotExistPermission(permissionsName, cancellationToken);
        }
        public Task<List<string>> GetExtraPermission([NotNull] List<string> permissionsName, CancellationToken cancellationToken = default)
        {
            if (permissionsName is null)
                throw new ArgumentNullException(nameof(permissionsName));

            return _permissionStore.GetExtraPermission(permissionsName, cancellationToken);
        }
    }

}
