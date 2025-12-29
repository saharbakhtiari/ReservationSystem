using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount.Permission
{
    public class PermissionStore<TKey, TRole, TUser, TContext,TPermission>
        where TUser : IdentityUser<TKey>
        where TPermission : IdentityPermission<TKey, TPermission, TRole>
        where TRole : IdentityRole<TKey, TPermission, TRole>
        where TContext : IdentityWithPermisionDbContext<TUser, TRole, TPermission, TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>Context for the store</summary>
        protected virtual TContext Context { get; private set; }
        protected virtual DbSet<TPermission> Permissions => Context.Set<TPermission>();

        /// <summary>
        ///     Constructor which takes a db context and wires up the stores with default instances using the context
        /// </summary>
        /// <param name="context"></param>
        public PermissionStore(TContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Context = context;

            //_roles = (IDbSet<TRole>)Context.Set<TRole>();
            //_permissions = (IDbSet<TPermission>)Context.Set<TPermission>();
            //_user = (IDbSet<TUser>)Context.Set<TUser>();
        }


        public virtual async Task CreateAsync(TPermission permission, CancellationToken cancellationToken = default)
        {
            if (await CheckIsExistAsync(permission.Name, cancellationToken))
            {
                return;
            }
            
            await Permissions.AddAsync(permission, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }
        public virtual Task<bool> CheckIsExistAsync(string permissionName, CancellationToken cancellationToken = default)
        {
            return Permissions.AnyAsync(x => x.Name.ToUpper() == permissionName.ToUpper(), cancellationToken);
        }
        public virtual Task<TPermission> GetPermissionAsync(string permissionName, CancellationToken cancellationToken = default)
        {
            return Permissions.Include(x=>x.Roles).FirstAsync(x => x.Name.ToUpper() == permissionName.ToUpper(), cancellationToken);
        }
        public virtual Task<TPermission> GetPermissionByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return Permissions.Include(x => x.Roles).FirstAsync(x => x.Id.ToString() == id, cancellationToken);
        }
        
        public virtual Task<List<TPermission>> GetNotExistPermission(List<string> permissionsName ,CancellationToken cancellationToken = default)
        {
            var uppercasePermission = permissionsName.Select(x => x.ToUpper()).ToList();
            return Permissions.AsNoTracking().Where(x => !uppercasePermission.Contains(x.Name.ToUpper())).ToListAsync(cancellationToken);
        }
        public virtual Task<List<TPermission>> GetAllPermissions( CancellationToken cancellationToken = default)
        {
           
            return Permissions.AsNoTracking().ToListAsync(cancellationToken);
        }
        public virtual Task DeleteNotExistPermission(List<string> permissionsName ,CancellationToken cancellationToken = default)
        {
            var uppercasePermission = permissionsName.Select(x => x.ToUpper()).ToList();
            return Permissions.AsNoTracking().Where(x => !uppercasePermission.Contains(x.Name.ToUpper())).BatchDeleteAsync(cancellationToken);
        }
        public virtual async Task<List<string>> GetExtraPermission(List<string> permissionsName, CancellationToken cancellationToken = default)
        {
            var allExistPermision = await Permissions.AsNoTracking().ToListAsync(cancellationToken);
            var uppercasePermission = permissionsName.Select(x => x.ToUpper()).ToList();
            return permissionsName.Where(x => !allExistPermision.Any(z => z.Name.ToUpper() == x.ToUpper())).ToList();
        }
        public virtual async Task AddToRoleAsync(TPermission permission, TRole role, CancellationToken cancellationToken = default)
        {
            permission.Roles.Add(role);
            await Context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task TakeFromRoleAsync(TPermission permission, TRole role, CancellationToken cancellationToken = default)
        {
            permission.Roles.Remove(role);
            await Context.SaveChangesAsync(cancellationToken);
        }

        ///// <summary>
        ///// Invoke initial configuration of permission extension. For instance create an Admin user and at least two main roles.
        ///// </summary>
        ///// <returns></returns>
        //public virtual async Task InitialConfiguration()
        //{
        //    if (!_roles.Any(r => r.Name == nameof(Roles.Administrator)))
        //        _roles.Add(new TRole { Name = nameof(Roles.Administrator), Title = "Super Administrator" });
        //    if (!_roles.Any(r => r.Name == nameof(Roles.User)))
        //        _roles.Add(new TRole { Name = nameof(Roles.User), Title = "General User" });

        //    Context.SaveChanges();

        //    var adminRole = _roles.First(x => x.Name == nameof(Roles.Administrator));
        //    if (!_user.Any(u => u.Roles.Any(r => r.RoleId.Equals(adminRole.Id))))
        //    {
        //        var userStore = Activator.CreateInstance(typeof(TUserStore), new object[] { Context });
        //        var userManager = Activator.CreateInstance(typeof(UserManager<TUser, TKey>), new object[] { userStore });
        //        var admin = new TUser
        //        {
        //            UserName = "Admin",
        //            Email = "Admin@here.com"
        //        };
        //        await ((UserManager<TUser, TKey>)userManager).CreateAsync(admin, "Admin@123");
        //        await ((UserManager<TUser, TKey>)userManager).AddToRoleAsync(admin.Id, nameof(Roles.Administrator));
        //    }
        //}

        ///// <summary>
        ///// Get the roles that is assigned to the user ClaimPrincipal object.
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public virtual async Task<IList<string>> GetRolesAsync(int userId, ClaimsPrincipal user = null)
        //{
        //    if (userId == 0 && user == null)
        //        throw new ArgumentNullException(nameof(userId));
        //    if (user == null || !user.Claims.Any())
        //        return await _roles.Where(x => x.Users.Any(u => userId.Equals(u.UserId)))
        //            .Select(r => r.Name).ToListAsync();
        //    //if else there is a user claims
        //    return
        //        await
        //            Task.Run(() => user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList());
        //}

        ///// <summary>
        ///// Get the roles that is assigned to the permission object.
        ///// </summary>
        ///// <param name="permission"></param>
        ///// <returns></returns>
        //public virtual async Task<IList<string>> GetRolesAsync(TPermission permission)
        //{
        //    if (permission == null)
        //        throw new ArgumentNullException(nameof(permission));
        //    if (default(TKey).Equals(permission.Id))
        //        throw new ArgumentNullException(nameof(permission.Id));
        //    return
        //        await
        //            _roles.Where(x => x.Permissions.Any(p => p.PermissionId.Equals(permission.Id)))
        //                .Select(x => x.Name)
        //                .ToListAsync();
        //}

        //public virtual async Task<TPermission> FindPermissionAsync(string name, long origin, bool isGlobal)
        //{
        //    if (name == null)
        //        throw new ArgumentNullException(nameof(name));
        //    if (isGlobal || origin == 0)
        //    {
        //        return await Task
        //            .FromResult((TPermission)_permissions.FirstOrDefault(x => x.Name == name && x.IsGlobal));
        //    }

        //    return await Task
        //        .FromResult((TPermission)_permissions.FirstOrDefault(x => x.Name == name && x.Origin == origin));
        //}

        ///// <summary>
        ///// Create a new permission.
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="origin"></param>
        ///// <param name="description"></param>
        ///// <param name="isGlobal"></param>
        ///// <param name="httpContext"></param>
        ///// <returns></returns>
        //public virtual async Task<TPermission> CreatePermissionAsync(string name, long origin, string description = null,
        //    bool isGlobal = false, HttpContextBase httpContext = null)
        //{
        //    if (name == null)
        //        throw new ArgumentNullException(nameof(name));

        //    string area = null, controller = null, action = null;
        //    TPermission permission = await FindPermissionAsync(name, origin, isGlobal);

        //    if (permission?.Name.ToLower() == name)
        //        throw new ArgumentException("The " + nameof(name) + " argument must be unique.");

        //    if (httpContext != null)
        //    {
        //        area = RouteTable.Routes?.GetRouteData(httpContext)?.Values[nameof(area)]?.ToString();
        //        controller = RouteTable.Routes?.GetRouteData(httpContext)?.Values[nameof(controller)]?.ToString();
        //        action = RouteTable.Routes?.GetRouteData(httpContext)?.Values[nameof(action)]?.ToString();
        //    }

        //    var newPerm = new TPermission();
        //    //create a new permission
        //    if (permission == null)
        //    {
        //        newPerm = new TPermission()
        //        {
        //            Name = name,
        //            Origin = origin,
        //            AreaName = area,
        //            ControllerName = controller,
        //            ActionName = action,
        //            Description = description,
        //            IsGlobal = origin == 0 || isGlobal
        //        };
        //        _permissions.Add(newPerm);
        //        Context.SaveChanges();

        //        //Add the administrator role to the permission
        //        await AddInitialRolesAsync(newPerm);

        //        Context.SaveChanges();
        //    }
        //    //the same permission existed
        //    else
        //    {
        //        int convert;
        //        var last = permission.Name.Split('_').Last();
        //        int.TryParse(last, out convert);
        //        if (convert != 0)
        //        {
        //            convert++;
        //            newPerm.Name = permission.Name
        //                .Split('_')
        //                .Except(new[] { last })
        //                .Concat(new[] { convert.ToString() })
        //                .Aggregate((x, y) => x + y);
        //        }
        //        else
        //        {
        //            convert = 1;
        //            newPerm.Name = permission.Name
        //                .Insert(permission.Name.Length - 1, convert.ToString());
        //        }
        //        newPerm.Origin = origin;
        //        newPerm.AreaName = area;
        //        newPerm.ControllerName = controller;
        //        newPerm.ActionName = action;
        //        newPerm.Description = description;
        //        newPerm.IsGlobal = origin == 0 || isGlobal;

        //        _permissions.Add(newPerm);
        //        Context.SaveChanges();

        //        //Add the administrator role to the permission
        //        await AddInitialRolesAsync(newPerm);

        //        Context.SaveChanges();
        //    }

        //    return await Task.FromResult(newPerm);
        //}

        //public virtual async Task DeletePermissionAsync(TKey id)
        //{
        //    var key = default(TKey);
        //    if (key != null && key.Equals(id))
        //        throw new ArgumentNullException(nameof(id));
        //    var permission = Context.Permissions.Find(id);
        //    if (permission == null)
        //        throw new Exception("Permission not found.");
        //    _permissions.Remove(permission);
        //    await Context.SaveChangesAsync();
        //}

        ///// <summary>
        ///// Add the administrator role to the specified permission.
        ///// </summary>
        ///// <param name="permission"></param>
        ///// <returns></returns>
        //private async Task AddInitialRolesAsync(TPermission permission)
        //{
        //    if (permission.Id.Equals(default))
        //        throw new ArgumentNullException(nameof(permission.Id));

        //    await Task.Run(() =>
        //    {

        //        var adminRole = new TRolePermission()
        //        {
        //            RoleId = _roles.First(x => x.Name == nameof(Roles.Administrator)).Id,
        //            PermissionId = permission.Id
        //        };

        //        if (permission.Roles != null)
        //            permission.Roles.Add(adminRole);
        //        else
        //        {
        //            permission.Roles = new List<TRolePermission>()
        //            {
        //                adminRole
        //            };
        //        }
        //    });
        //}
    }
}