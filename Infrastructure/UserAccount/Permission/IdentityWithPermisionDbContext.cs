using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.UserAccount.Permission
{
    public class IdentityWithPermisionDbContext<TUser, TRole, TPermission, TKey> :
        KeyApiAuthorizationDbContext<TUser, TRole, TKey>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey, TPermission, TRole>
    where TPermission : IdentityPermission<TKey, TPermission, TRole>
    where TKey : IEquatable<TKey>
    {
        public IdentityWithPermisionDbContext(
            DbContextOptions options)
            : base(options)
        {

        }

        public virtual DbSet<TPermission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TPermission>().Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Entity<TPermission>().HasIndex(p => p.Name).IsUnique();
            builder.Entity<TPermission>().ToTable("Permissions");
        }
    }
}