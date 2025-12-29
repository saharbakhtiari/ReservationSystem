using Domain.Common;
using Domain.Roles;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{
    public class MyUserManager<TUser> : UserManager<TUser> where TUser : ApplicationUser
    {
        public MyUserManager(MyUserStore<TUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators, IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
        public override Task<bool> CheckPasswordAsync(TUser user, string password)
        {
            var passwordChecker = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IUserStoreProvider>(user.LoginProvider.ToString());
            if (passwordChecker is not null)
            {
                return passwordChecker.CheckPasswordAsync(user, password);
            }
            return Task.FromResult(false);
        }

        public Task<bool> CheckPasswordByBasicAsync(TUser user, string password)
        {
            return base.CheckPasswordAsync(user, password);
        }
        public Task<List<RoleDto>> GetRolesAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return (Store as MyUserStore<TUser>).GetRolesAsync(userId, cancellationToken);
        }
        public Task<List<string>> GetPermissionsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return (Store as MyUserStore<TUser>).GetPermissionsAsync(userId, cancellationToken);
        }
        public Task<List<string>> GetPermissionIdsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return (Store as MyUserStore<TUser>).GetPermissionIdsAsync(userId, cancellationToken);
        }
        
        public Task<List<ApplicationPermission>> GetPermissionsFullAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return (Store as MyUserStore<TUser>).GetPermissionsFullAsync(userId, cancellationToken);
        }
        public Task<bool> CheckPermissionAsync(Guid userId,string permission, CancellationToken cancellationToken = default)
        {
            return (Store as MyUserStore<TUser>).CheckPermissionAsync(userId, permission, cancellationToken);
        }

    }

}
