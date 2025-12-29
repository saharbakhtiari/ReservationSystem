using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Infrastructure.UserAccount.Permission
{
    public class IdentityRole<TKey, TPermission, TRole> : IdentityRole<TKey>
        where TPermission : IdentityPermission<TKey, TPermission, TRole>
        where TRole : IdentityRole<TKey, TPermission, TRole>
        where TKey : IEquatable<TKey>
    {
        public IdentityRole() : base()
        {
            Permissions = new List<TPermission>();
        }
        public IdentityRole(string roleName) : base(roleName)
        {
            Permissions = new List<TPermission>();
        }
        public virtual ICollection<TPermission> Permissions { get; set; }
    }
    public class IdentityPermission<TKey, TPermission, TRole>
        where TPermission : IdentityPermission<TKey, TPermission, TRole>
        where TRole : IdentityRole<TKey, TPermission, TRole>
        where TKey : IEquatable<TKey>
    {
        public IdentityPermission()
        {
            Roles = new List<TRole>();
        }
        public IdentityPermission(string permissionName) : base()
        {
            Name = permissionName;
        }
        public virtual TKey Id { get; set; }
        public virtual string Name { get; set; }
        //public virtual string NormalizedName => Name.ToUpper();

        public virtual ICollection<TRole> Roles { get; set; }
    }
}