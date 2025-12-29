using Infrastructure.UserAccount.Permission;
using System;

namespace Infrastructure.UserAccount
{
    public class ApplicationPermission : IdentityPermission<Guid, ApplicationPermission, ApplicationUserRoles>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationPermission() : base()
        {
        }

        /// <summary>
        /// Overloaded constructor, creates a new permission with entered name. 
        /// </summary>
        /// <param name="roleName"></param>
        public ApplicationPermission(string permissionName) : base(permissionName)
        {
        }
    }
}
