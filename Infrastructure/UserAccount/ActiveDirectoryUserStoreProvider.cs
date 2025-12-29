using Domain.Users;
using Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{
    public class ActiveDirectoryUserStoreProvider : IUserStoreProvider
    {
        private readonly IUserMediator _userMediator;
        private readonly MyUserManager<ApplicationUser> _userManager;

        public ActiveDirectoryUserStoreProvider(IUserMediator userMediator, MyUserManager<ApplicationUser> userManager)
        {
            _userMediator = userMediator;
            _userManager = userManager;
        }

        public Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string password)
        {
            throw new UserFriendlyException("تغییر کلمه عبور این کاربر باید از طریق active directory صورت پذیرد.");
        }
        public Task<bool> SetPasswordAsync(ApplicationUser user, string password)
        {
            throw new UserFriendlyException("تخصیص کلمه عبور این کاربر باید از طریق active directory صورت پذیرد.");
        }

        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return _userMediator.ValidateUser(user.UserName, password);
        }
        public Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return _userManager.CreateAsync(user);
        }

        public Task<IdentityResult> CreateUserAsync(ApplicationUser user)
        {
            return _userManager.CreateAsync(user);
        }

        public async Task<ApplicationUser> GetApplicationUserAsync(string userName)
        {
            var userData = await _userMediator.GetUser(userName);
            if (userData is null)
            {
                throw new UserFriendlyException("کاربر یافت نشد");
            }
            return new()
            {
                UserName = userData.UserName,
                Email = userData["mail"],
                FirstName = userData["givenName"],
                LastName = userData["sn"],
                Sex = userData["otherPager"],
                DistinguishedName = userData["distinguishedName"],
                EmployeeNumber = userData["physicalDeliveryOfficeName"],
                BirthDate = userData["userParameters"],
                EmailConfirmed = true,
                LoginProvider = LoginProvider.ActiveDirectory
            };
        }
    }

}
