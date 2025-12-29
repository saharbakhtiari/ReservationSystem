using Domain.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{
    public class BasicUserStoreProvider : IUserStoreProvider
    {
        private readonly MyUserManager<ApplicationUser> _userManager;

        public BasicUserStoreProvider(MyUserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return _userManager.CheckPasswordByBasicAsync(user, password);
        }

        public Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<IdentityResult> CreateUserAsync(ApplicationUser user)
        {
            var result = _userManager.CreateAsync(user);
            return result;
        }

        public Task<ApplicationUser> GetApplicationUserAsync(string userName)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = $"{userName}@seo.ir",
                FirstName = userName,
                LastName = userName,
                Sex = "",
                DistinguishedName = "",
                EmployeeNumber = "",
                BirthDate = "",
                EmailConfirmed = true,
                LoginProvider = LoginProvider.BasicAuthentication
            };
            return Task.FromResult(user);

        }

        public async Task<bool> SetPasswordAsync(ApplicationUser user, string password)
        {
            var deleteResult = await _userManager.RemovePasswordAsync(user);
            if (!deleteResult.Succeeded)
            {
                return false;
            }
            var setResult = await _userManager.AddPasswordAsync(user, password);
            if (!setResult.Succeeded)
            {
                return false;
            }
            return true;
        }
    }

}
