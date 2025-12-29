using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.UserAccount.Permission
{
    /// <summary>
    /// Permission-based authorization attribute.
    /// </summary>
    public class AuthorizePermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///  Gets or sets a comma delimited list of Permissions that are allowed to access the resource.
        /// </summary>
        public string Permissions { get; set; }
    }
}