using Domain.Common.Mappings;
using Domain.Roles;
using Infrastructure.UserAccount.Permission;
using System;

namespace Infrastructure.UserAccount
{
    public class ApplicationUserRoles : IdentityRole<Guid, ApplicationPermission, ApplicationUserRoles>, IMapTo<RoleDto>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationUserRoles() : base()
        {
        }

        /// <summary>
        /// Overloaded constructor, creates a new role with entered name. 
        /// </summary>
        /// <param name="roleName"></param>
        public ApplicationUserRoles(string roleName) : base(roleName)
        {
        }

    }
}
