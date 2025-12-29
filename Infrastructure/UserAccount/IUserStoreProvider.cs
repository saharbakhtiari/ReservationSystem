using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{
    public interface IUserStoreProvider
    {
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<ApplicationUser> GetApplicationUserAsync(string userName);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
        Task<bool> SetPasswordAsync(ApplicationUser user, string newPassword);
    }

}
