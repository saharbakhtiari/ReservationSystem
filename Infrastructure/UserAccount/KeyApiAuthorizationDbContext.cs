using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace Infrastructure.UserAccount
{
    // Taken from: https://stackoverflow.com/questions/58208894/aspnet-core-identity-custom-apiauthorizationdbcontext

    /// <summary>
    /// Database abstraction for a combined <see cref="DbContext"/> using ASP.NET Identity and Identity Server.
    /// </summary>
    /// <typeparam name="TUser">Derrived class type of IdentityUser from ASP.NET Identity</typeparam>
    /// <typeparam name="TRole">Derrived class type of IdentityRole from ASP.NET Identity</typeparam>
    /// <typeparam name="TKey">Please use GUID. Although stricly speaking it can be string, int also (in which case recommend to rename class)</typeparam>
    public class KeyApiAuthorizationDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {

        /// <summary>
        /// Initializes a new instance of <see cref="ApiAuthorizationDbContext{TUser, TRole, TKey}"/>.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions"/>.</param>
        /// <param name="operationalStoreOptions">The <see cref="IOptions{OperationalStoreOptions}"/>.</param>
        public KeyApiAuthorizationDbContext(
            DbContextOptions options)
            : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TUser>().ToTable("Users");
            builder.Entity<TRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<TKey>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<TKey>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<TKey>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<TKey>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<TKey>>().ToTable("UserTokens");
        }
    }

}
