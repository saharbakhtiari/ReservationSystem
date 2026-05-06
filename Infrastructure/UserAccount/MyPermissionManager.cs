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
    public class MyPermissionManager
        : PermissionManager<Guid, ApplicationUser, ApplicationUserRoles, ApplicationPermission, ApplicationDbContext>
    {
        private readonly MyPermissionStore _permissionStore;

        public MyPermissionManager(
            MyRoleManager<ApplicationUserRoles> roleManager,
            MyPermissionStore permissionStore)
            : base(roleManager, permissionStore)
        {
            _permissionStore = permissionStore;
        }

        public Task CreateAsync(ApplicationPermission permission, CancellationToken cancellationToken = default)
        {
            return _permissionStore.CreateAsync(permission, cancellationToken);
        }

        public async Task CreateAsync([NotNull] string code, string title, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            var permission = new ApplicationPermission(code, title);

            await _permissionStore.CreateAsync(permission, cancellationToken);
        }

        public Task<List<ApplicationPermission>> GetNotExistPermission(
            [NotNull] List<string> codes,
            CancellationToken cancellationToken = default)
        {
            if (codes is null)
                throw new ArgumentNullException(nameof(codes));

            return _permissionStore.GetNotExistPermission(codes, cancellationToken);
        }

        public Task<List<ApplicationPermission>> GetAllPermission(CancellationToken cancellationToken = default)
        {
            return _permissionStore.GetAllPermissions(cancellationToken);
        }

        public Task DeleteNotExistPermission(
            [NotNull] List<string> codes,
            CancellationToken cancellationToken = default)
        {
            if (codes is null)
                throw new ArgumentNullException(nameof(codes));

            return _permissionStore.DeleteNotExistPermission(codes, cancellationToken);
        }

        public Task<List<string>> GetExtraPermission(
            [NotNull] List<string> codes,
            CancellationToken cancellationToken = default)
        {
            if (codes is null)
                throw new ArgumentNullException(nameof(codes));

            return _permissionStore.GetExtraPermission(codes, cancellationToken);
        }
    }
}