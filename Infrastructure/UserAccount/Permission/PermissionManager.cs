using Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount.Permission
{
    public class PermissionManager<TKey, TUser, TRole, TPermission, TContext>
        where TUser : IdentityUser<TKey>
        where TPermission : IdentityPermission<TKey, TPermission, TRole>
        where TRole : IdentityRole<TKey, TPermission, TRole>
        where TContext : IdentityWithPermisionDbContext<TUser, TRole, TPermission, TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly RoleManager<TRole> _roleManager;
        private readonly PermissionStore<TKey, TRole, TUser, TContext, TPermission> _permissionStore;

        public PermissionManager(RoleManager<TRole> roleManager, PermissionStore<TKey, TRole, TUser, TContext, TPermission> permissionStore)
        {
            _roleManager = roleManager;
            _permissionStore = permissionStore;
        }
        public virtual async Task AddToRoleAsync(TPermission permission, TRole role, CancellationToken cancellationToken = default)
        {
            await _permissionStore.AddToRoleAsync(permission, role, cancellationToken);
        }
        public virtual async Task AddToRoleAsync(TPermission permission, string roleName, CancellationToken cancellationToken = default)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null)
            {
                throw new NotFoundException(typeof(TRole).Name, roleName);
            }
            await _permissionStore.AddToRoleAsync(permission, role, cancellationToken);
        }
        public virtual async Task AddToRoleAsync(string permissionName, string roleName, CancellationToken cancellationToken = default)
        {
            var permission = await _permissionStore.GetPermissionAsync(permissionName, cancellationToken);
            await AddToRoleAsync(permission, roleName, cancellationToken);
        }
        public virtual async Task TakeFromRoleAsync(TPermission permission, TRole role, CancellationToken cancellationToken = default)
        {
            await _permissionStore.TakeFromRoleAsync(permission, role, cancellationToken);
        }
        public Task<TPermission> GetPermissionAsync(string permissionName, CancellationToken cancellationToken = default)
        {
            return _permissionStore.GetPermissionAsync(permissionName, cancellationToken);
        }
        public Task<TPermission> GetPermissionByIdAsync(string permissionId, CancellationToken cancellationToken = default)
        {
            return _permissionStore.GetPermissionByIdAsync(permissionId, cancellationToken);
        }
        //public virtual async Task InitialConfiguration()
        //{
        //    await Store.InitialConfiguration();
        //}

        //public virtual async Task<IList<string>> GetRolesAsync(int userId, ClaimsPrincipal user = null)
        //{
        //    return await Store.GetRolesAsync(userId, user);
        //}

        //public virtual async Task<TPermission> FindPermissionAsync(string name, long origin, bool isGlobal)
        //{
        //    return await Store.FindPermissionAsync(name, origin, isGlobal);
        //}

        //public virtual async Task<TPermission> CreatePermissionAsync(string name, long origin, string description = null,
        //    bool isGlobal = false, HttpContextBase httpContext = null)
        //{
        //    return await Store.CreatePermissionAsync(name, origin, description, isGlobal, httpContext);
        //}

        //public virtual async Task DeletePermissionAsync(TKey id)
        //{
        //    await Store.DeletePermissionAsync(id);
        //}

        ///// <summary>
        ///// Check weather the permission assigned to the list of roles or not.
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="roles"></param>
        ///// <param name="description"></param>
        ///// <param name="isGlobal"></param>
        ///// <param name="httpContext"></param>
        ///// <returns></returns>
        //public virtual async Task<bool> CheckPermissionAsync(string name, IList<string> roles, string description = null,
        //    bool isGlobal = false, HttpContextBase httpContext = null)
        //{
        //    var origin = GetOrigin(httpContext);
        //    var permission = await FindPermissionAsync(name, origin, isGlobal) ??
        //                     await CreatePermissionAsync(name, origin, description, isGlobal, httpContext);
        //    foreach (var role in await Store.GetRolesAsync(permission))
        //    {
        //        if (roles.Contains(role))
        //            return await Task.FromResult(true);
        //    }
        //    return await Task.FromResult(false);
        //}

        ///// <summary>
        ///// Using the RouteData to find the permission origin.
        ///// </summary>
        ///// <returns>Return a long hash number. if is zero, the permission defined out of the MVC structure scope.</returns>
        //public virtual long GetOrigin(HttpContextBase httpContext = null)
        //{
        //    var mvcOrigin = "";
        //    if (httpContext != null)
        //    {
        //        mvcOrigin += RouteTable.Routes?.GetRouteData(httpContext)?.Values["area"]?.ToString();
        //        mvcOrigin += RouteTable.Routes?.GetRouteData(httpContext)?.Values["controller"]?.ToString();
        //        mvcOrigin += RouteTable.Routes?.GetRouteData(httpContext)?.Values["action"]?.ToString();
        //    }

        //    if (string.IsNullOrEmpty(mvcOrigin))
        //        return 0;

        //    return new ByteEncoder(mvcOrigin).ToLong();
        //}

        //public virtual async Task<bool> AuthorizePermissionAsync(string name, string description = null,
        //    bool isGlobal = false)
        //{
        //    if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
        //        return await Task.FromResult(false);

        //    var stringUserId = Thread.CurrentPrincipal?.Identity?.GetUserId();
        //    if (string.IsNullOrEmpty(stringUserId))
        //        throw new ArgumentNullException(nameof(stringUserId));

        //    var roles =
        //        await
        //            GetRolesAsync(int.Parse(stringUserId), (ClaimsPrincipal)Thread.CurrentPrincipal);

        //    return await this.CheckPermissionAsync(name, roles, description, isGlobal);
        //}

        //public virtual async Task<bool> AuthorizePermissionAsync(HttpContextBase httpContext, string name,
        //    string description = null, bool isGlobal = false)
        //{
        //    if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
        //        return await Task.FromResult(false);

        //    var stringUserId = Thread.CurrentPrincipal?.Identity?.GetUserId();
        //    if (string.IsNullOrEmpty(stringUserId))
        //        throw new ArgumentNullException(nameof(stringUserId));

        //    var roles =
        //        await
        //            GetRolesAsync(int.Parse(stringUserId), (ClaimsPrincipal)Thread.CurrentPrincipal);

        //    return await this.CheckPermissionAsync(name, roles, description, isGlobal, httpContext);
        //}

        //public void Dispose()
        //{
        //    Store = null;
        //}
    }
}