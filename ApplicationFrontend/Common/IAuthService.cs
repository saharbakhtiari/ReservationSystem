using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application_Frontend.Common
{
    public interface IAuthService
    {
        Task Logout();
        Task UpdateToken(string token);
        Task<Guid> GetCurrentUserIdAsync();
        Task<ClaimsPrincipal> GetCurrentUserAsync();
        Task UpdatePermissions(List<string> permissions);
        Task<bool> HasPermissionOrRole(string permission,string role);
        Task<List<string>> GetUserPermissions();
    }
}
