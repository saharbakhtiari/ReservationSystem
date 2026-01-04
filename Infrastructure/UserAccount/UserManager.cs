using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomLoggers;
using Domain.Common;
using Domain.Roles;
using Domain.Security;
using Domain.SeoSms;
using Domain.UnitOfWork.Uow;
using Domain.Users;
using Exceptions;
using Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmsService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{
    public class UserManager : IUserManager
    {
        private readonly MyUserManager<ApplicationUser> _userManager;
        private readonly MyRoleManager<ApplicationUserRoles> _roleManager;
        private readonly MyPermissionManager _permissionManager;
        private readonly IJwtGeneratorService _jwtGenerator;
        private readonly ApplicationDbContext _context;
        private readonly IUserMediator _userMediator;
        private readonly IUnitOfWorkManager _uowManager;
        private readonly ICustomLogger<UserManager> _customLogger;
        private readonly ISMSService _smsService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserManager(
            MyUserManager<ApplicationUser> userManager,
            IJwtGeneratorService jwtGenerator,
            MyRoleManager<ApplicationUserRoles> roleManager,
            IDbContextProvider<ApplicationDbContext> context,
            IUserMediator userMediator,
            MyPermissionManager permissionManager,
            IUnitOfWorkManager uowManager,
            ICustomLogger<UserManager> customLogger,
            ISMSService smsService,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtGenerator = jwtGenerator;
            _context = context.GetDbContext();
            _userMediator = userMediator;
            _permissionManager = permissionManager;
            _uowManager = uowManager;
            _customLogger = customLogger;
            _smsService = smsService;
            _configuration = configuration;
            _mapper = mapper;
        }


        public async Task<bool> HasPasswordAsync(Guid userId)
        {
            var userData = await _userManager.FindByIdAsync(userId.ToString());
            return await _userManager.HasPasswordAsync(userData);
        }

        public async Task<string> GetUserNameAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserName;
        }

        public async Task<Guid> CreateUserAsync(string userName, string password, LoginProvider loginProvider)
        {
            var userStoreProvider = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IUserStoreProvider>(loginProvider.ToString());

            var newUser = await userStoreProvider.GetApplicationUserAsync(userName);

            if (newUser.Email.IsNullOrWhiteSpace())
            {
                newUser.Email = $"{newUser.UserName}@seo.ir";
            }
            var user = await _userManager.FindByNameAsync(newUser.UserName);
            if (user is null)
            {
                var result = await userStoreProvider.CreateUserAsync(newUser);
                if (!result.Succeeded)
                {
                    throw new UserFriendlyException($"Can not Create User {String.Join(",", result.Errors.Select(x => x.Code))}");
                }
            }
            else
            {
                if (user.EmailConfirmed.Not())
                {
                    user.EmailConfirmed = true;
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        throw new UserFriendlyException($"Can not Create User {String.Join(",", result.Errors.Select(x => x.Code))}");
                    }
                }
                else
                {
                    throw new UserFriendlyException("کاربر مورد نظر تکراری می باشد");
                }
            }
            return (await _userManager.FindByNameAsync(userName)).Id;

        }


        /// <summary>
        /// Seo Service
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="loginProvider"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task SendRegisterOtpCodeAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(phoneNumber);
            if (user is not null)
            {
                throw new UserFriendlyException($"کاربری با شماره موبایل وارد شده وجود دارد");
            }
            var code = GenerateCode(5);
            SeoSms sms = new SeoSms()
            {
                VerifyCode = code.Encrypt(_configuration.GetSection("EncyptionKey").Value),
                IssuedDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMinutes(2),
                PhoneNumber = phoneNumber,
            };
            await sms.DomainService.DeleteOlds(cancellationToken);
            await sms.SaveAsync(cancellationToken);
            var res = await _smsService.Send(phoneNumber, code, cancellationToken);
            if (!res)
            {
                throw new UserFriendlyException($"ارسال پیام با خطا مواجه شد");
            }
        }

        public async Task SendLoginOtpCodeAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            //var user = await _userManager.FindByNameAsync(phoneNumber);
            //if (user is null)
            //{
            //    //throw new UserFriendlyException($"کاربری با شماره موبایل وارد شده وجود ندارد");
            //    return;
            //}
            var code = GenerateCode(5);
            SeoSms sms = new SeoSms()
            {
                VerifyCode = code.Encrypt(_configuration.GetSection("EncyptionKey").Value),
                IssuedDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMinutes(2),
                PhoneNumber = phoneNumber,
            };
            await sms.DomainService.DeleteOlds(cancellationToken);
            await sms.SaveAsync(cancellationToken);
            var res = await _smsService.Send(phoneNumber, code, cancellationToken);
            if (!res)
            {
                throw new UserFriendlyException($"ارسال پیام با خطا مواجه شد");
            }
        }

        private string GenerateCode(int length)
        {
            string _numbers = "0123456789";
            StringBuilder builder = new StringBuilder(length);
            Random random = new Random();

            for (var i = 0; i < length; i++)
            {
                builder.Append(_numbers[random.Next(0, _numbers.Length)]);
            }

            return builder.ToString();
        }

        public async Task<Guid> RegisterUser(string phoneNumber, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByNameAsync(phoneNumber);
            if (user is not null)
            {
                throw new UserFriendlyException($"کاربری با شماره موبایل وارد شده وجود دارد");
            }
            var userStoreProvider = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IUserStoreProvider>(LoginProvider.BasicAuthentication.ToString());
            ApplicationUser newUser = new ApplicationUser()
            {
                UserName = phoneNumber,
                PhoneNumber = phoneNumber,
                Email = $"{Guid.NewGuid()}@seo.ir",
                LoginProvider = LoginProvider.BasicAuthentication,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
            };
            var result = await userStoreProvider.CreateUserAsync(newUser);
            if (!result.Succeeded)
            {
                throw new UserFriendlyException($"Can not Create User {String.Join(",", result.Errors.Select(x => x.Code))}");
            }
            return (await _userManager.FindByNameAsync(phoneNumber)).Id;
        }

        public async Task VerifyOtpAsync(string phoneNumber, string verifyCode, CancellationToken cancellationToken)
        {
            var sms = await SeoSms.GetSmsAsync(phoneNumber, cancellationToken) ?? throw new UserFriendlyException($"هویت شما احراز نشد"); ;
            if (DateTime.Now > sms.ExpireDate)
            {
                throw new UserFriendlyException($"زمان ثبت کد به پایان رسید");
            }
            if (sms.VerifyCode.Decrypt(_configuration.GetSection("EncyptionKey").Value) != verifyCode)
            {
                throw new UserFriendlyException($"کد وارد شده صحیح نیست");
            }
        }

        public async Task<Guid> CreateUserAsync(string userName, string password, string firstName, string lastName, string phoneNumber, string employeeNumber, LoginProvider loginProvider)
        {
            var userStoreProvider = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IUserStoreProvider>(loginProvider.ToString());

            var newUser = await userStoreProvider.GetApplicationUserAsync(userName);

            if (newUser.Email.IsNullOrWhiteSpace())
            {
                newUser.Email = $"{newUser.UserName}@seo.ir";
            }

            if (newUser.PhoneNumber.IsNullOrWhiteSpace() && !phoneNumber.IsNullOrEmpty())
            {
                newUser.PhoneNumber = phoneNumber;
            }
            if (newUser.EmployeeNumber.IsNullOrWhiteSpace() && !employeeNumber.IsNullOrEmpty())
            {
                newUser.EmployeeNumber = employeeNumber;
            }
            if (loginProvider == LoginProvider.BasicAuthentication)
            {
                if (!firstName.IsNullOrWhiteSpace())
                    newUser.FirstName = firstName;
                if (!lastName.IsNullOrWhiteSpace())
                    newUser.LastName = lastName;
            }
            var user = await _userManager.FindByNameAsync(newUser.UserName);
            if (user is null)
            {
                newUser.PhoneNumberConfirmed = true;
                var result = await userStoreProvider.CreateUserAsync(newUser, password);
                if (!result.Succeeded)
                {
                    throw new UserFriendlyException($"Can not Create User {String.Join(",", result.Errors.Select(x => x.Code))}");
                }
            }
            else
            {
                if (user.EmailConfirmed.Not())
                {
                    user.EmailConfirmed = true;
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        throw new UserFriendlyException($"Can not Create User {String.Join(",", result.Errors.Select(x => x.Code))}");
                    }
                }
                else
                {
                    throw new UserFriendlyException("کاربر مورد نظر تکراری می باشد");
                }
            }

            return (await _userManager.FindByNameAsync(userName)).Id;
        }

        public async Task UpdateUserAsync(UserInputDto user)
        {
            var userData = await _userManager.FindByIdAsync(user.Id.ToString());
            if (userData is null)
            {
                throw new UserFriendlyException("کاربری با شناسه اعلامی یافت نشد");
            }

            userData.FirstName = user.FirstName;
            userData.LastName = user.LastName;
            userData.Email = user.Email;
            userData.EmployeeNumber = user.EmployeeNumber;
            userData.PhoneNumber = user.PhoneNumber;
            var result = await _userManager.UpdateAsync(userData);
            if (!result.Succeeded)
            {
                throw new UserFriendlyException($"Can not Update User {String.Join(",", result.Errors.Select(x => x.Code))}");
            }
        }

        public async Task UpdatePhoneNumberAsync(UserInputDto user)
        {
            var userData = await _userManager.FindByIdAsync(user.Id.ToString());
            if (userData is null)
            {
                throw new UserFriendlyException("کاربری با شناسه اعلامی یافت نشد");
            }
            userData.PhoneNumber = user.PhoneNumber;
            var result = await _userManager.UpdateAsync(userData);
            if (!result.Succeeded)
            {
                throw new UserFriendlyException($"Can not Update User {String.Join(",", result.Errors.Select(x => x.Code))}");
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var userData = await _userManager.FindByIdAsync(id.ToString());
            if (userData is null)
            {
                throw new UserFriendlyException("کاربری با شناسه اعلامی یافت نشد");
            }

            userData.PhoneNumberConfirmed = false;

            var result = await _userManager.UpdateAsync(userData);
            if (!result.Succeeded)
            {
                throw new UserFriendlyException($"Can not Update User {String.Join(",", result.Errors.Select(x => x.Code))}");
            }
        }

        public Task<PagedList<TOutput>> GetAllUserAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();

            return _userManager.Users
                .AsNoTracking()
                .Where(x => x.PhoneNumberConfirmed)
                .WhereIf(filter.IsNullOrWhiteSpace().Not(), u => u.UserName.Contains(filter) || u.FirstName.Contains(filter) || u.LastName.Contains(filter) || u.EmployeeNumber.Contains(filter))
                .OrderByIf(sort.IsNullOrWhiteSpace().Not(), sort)
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .ToPagedList(PageNumber, PageSize, cancellationToken);

        }

        public async Task<bool> UserIsInRoleAsync(Guid userId, string role)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId && u.PhoneNumberConfirmed);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<Guid> CreateRoleAsync(string roleName, string roleTitle)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                // Role doesn't exist, create one.
                var identityResult = await _roleManager.CreateAsync(new ApplicationUserRoles(roleName));

                if (identityResult.Succeeded)
                {
                    role = await _roleManager.FindByNameAsync(roleName);
                    await UpdateRoleAsync(role.Id, roleName, roleTitle);
                    return role.Id;
                }
                throw new Exception(String.Join(",", identityResult.Errors.Select(x => $"{x.Code}-{x.Description}")));
            }
            throw new UserFriendlyException("نقشی با این نام وجود دارد");
        }

        public async Task UpdateRoleAsync(Guid id, string roleName, string roleTitle)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role is null)
            {
                throw new UserFriendlyException("نقشی با این شناسه وجود ندارد");

            }

            CheckNotBeDefaultRole(role.Name, "نقش های سیستمی قابلیت ویرایش شدن ندارند");

            role.Name = roleName;
            role.NormalizedName = roleTitle;
            await _roleManager.UpdateNormalizedRoleNameAsync(role);
            var identityResult = await _roleManager.UpdateAsync(role);

            if (identityResult.Succeeded)
            {

                return;
            }
            throw new Exception(String.Join(",", identityResult.Errors.Select(x => $"{x.Code}-{x.Description}")));
        }

        private void CheckNotBeDefaultRole(string roleName, string message)
        {
            Type type = typeof(DefaultRoleNames);
            var allFields = type.GetFields();

            foreach (var field in allFields)
            {
                var propertyValue = field.GetValue(null) as string;

                if (string.IsNullOrWhiteSpace(propertyValue) == false)
                {
                    if (roleName == propertyValue)
                    {
                        throw new UserFriendlyException(message);
                    }
                }

            }
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role is null)
            {
                throw new UserFriendlyException("نقشی با این شناسه وجود ندارد");
            }

            CheckNotBeDefaultRole(role.Name, "نقش های سیستمی قابلیت حذف شدن ندارند");

            var identityResult = await _roleManager.DeleteAsync(role);

            if (identityResult.Succeeded)
            {
                return;
            }
            throw new Exception(String.Join(",", identityResult.Errors.Select(x => $"{x.Code}-{x.Description}")));
        }

        public Task<PagedList<TOutput>> GetAllRolesAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();

            return _roleManager.Roles
                .AsNoTracking()
                .WhereIf(filter.IsNullOrWhiteSpace().Not(), r => r.Name.Contains(filter))
                .OrderByIf(sort.IsNullOrWhiteSpace().Not(), sort)
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .ToPagedList(PageNumber, PageSize, cancellationToken);

        }

        public async Task<List<string>> GetRolePermissionsAsync(Guid roleId, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role is null)
            {
                throw new UserFriendlyException("نقشی با این نام وجود دارد");
            }
            return await _roleManager.GetAllPermission(roleId, cancellationToken);
        }

        public async Task<List<string>> GetRolePermissionIdsAsync(Guid roleId, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role is null)
            {
                throw new UserFriendlyException("نقشی با این نام وجود دارد");
            }
            return await _roleManager.GetAllPermissionIds(roleId, cancellationToken);
        }

        public async Task<List<PermissionDto>> GetRolePermissionsFullAsync(Guid roleId, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role is null)
            {
                throw new UserFriendlyException("نقشی با این نام وجود دارد");
            }
            return await _roleManager.GetAllPermissionFull(roleId, cancellationToken);
        }

        public async Task AssignPermissionToRoleAsync(Guid roleId, List<string> Permissions, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role is null)
            {
                throw new UserFriendlyException("نقشی با این نام وجود دارد");
            }

            var lastPermissions = await _roleManager.GetAllPermissionFull(roleId, cancellationToken); ;

            var deletePermissions = lastPermissions.Where(x => Permissions.Contains(x.Id.ToString()).Not());
            var newPermissions = Permissions.Where(x => lastPermissions.Any(y => y.Id.ToString() == x).Not());
            foreach (var permission in newPermissions)
            {
                var permissionObject = await _permissionManager.GetPermissionByIdAsync(permission, cancellationToken);
                await _permissionManager.AddToRoleAsync(permissionObject, role, cancellationToken);
            }
            foreach (var permission in deletePermissions)
            {
                var permissionObject = await _permissionManager.GetPermissionByIdAsync(permission.Id.ToString(), cancellationToken);
                await _permissionManager.TakeFromRoleAsync(permissionObject, role, cancellationToken);
            }
        }

        public async Task AssignRoleAsync(Guid userId, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                throw new UserFriendlyException("نقش مورد نظر یافت نشد");
            }

            bool userAlreadyInRole = await UserIsInRoleAsync(userId, roleName);

            if (userAlreadyInRole == false)
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
                var identityResult = await _userManager.AddToRoleAsync(user, roleName);

                if (identityResult.Succeeded == false)
                {
                    throw new Exception(String.Join(",", identityResult.Errors.Select(x => $"{x.Code}-{x.Description}")));
                }
            }
        }

        public async Task AssignRoleAsyncByRoleId(Guid userId, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                throw new UserFriendlyException("نقش مورد نظر یافت نشد");
            }

            bool userAlreadyInRole = await UserIsInRoleAsync(userId, role.Name);

            if (userAlreadyInRole == false)
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
                var identityResult = await _userManager.AddToRoleAsync(user, role.Name);

                if (identityResult.Succeeded == false)
                {
                    throw new Exception(String.Join(",", identityResult.Errors.Select(x => $"{x.Code}-{x.Description}")));
                }
            }
        }

        public async Task UnAssignRoleAsync(Guid userId, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                throw new UserFriendlyException("نقش مورد نظر یافت نشد");
            }
            bool userAlreadyInRole = await UserIsInRoleAsync(userId, roleName);
            if (userAlreadyInRole)
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
                var identityResult = await _userManager.RemoveFromRoleAsync(user, roleName);

                if (identityResult.Succeeded == false)
                {
                    throw new Exception(String.Join(",", identityResult.Errors.Select(x => $"{x.Code}-{x.Description}")));
                }
            }
        }

        public async Task AuthenticateAsync(string userName, string password)
        {
            using (var uow = _uowManager.Begin(new SedUnitOfWorkOptions { IsTransactional = false, Timeout = TimeSpan.FromMinutes(1) }, requiresNew: true))
            {
                var signInManager = ServiceLocator.ServiceProvider.GetService<SignInManager<ApplicationUser>>();
                var signInResult = await signInManager.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: true);

                if (signInResult.Succeeded)
                {
                    return;
                }
                else
                {
                    List<string> errors = new List<string>();

                    errors.Add("نام کاربری و یا رمز عبور صحیح نمی باشد");

                    if (signInResult.IsNotAllowed)
                    {
                        errors.Add("Email not confirmed");
                    }

                    if (signInResult.IsLockedOut)
                    {
                        errors.Add("User Locked out.");
                        await _customLogger.LogWarning($"{userName} is locked");
                    }

                    throw new UserFriendlyException("نام کاربری و یا رمز عبور صحیح نمی باشد");
                }
            }
        }

        public async Task AuthenticateAsync(string userName)
        {
            using (var uow = _uowManager.Begin(new SedUnitOfWorkOptions { IsTransactional = false, Timeout = TimeSpan.FromMinutes(1) }, requiresNew: true))
            {
                var signInManager = ServiceLocator.ServiceProvider.GetService<SignInManager<ApplicationUser>>();
                var userData = await _userManager.FindByNameAsync(userName) ?? throw new UserFriendlyException($"هویت شما احراز نشد");
                await signInManager.SignInAsync(userData, isPersistent: false);
            }
        }

        public async Task<TokenDto> GetUserTokenIdAsync(string userName)
        {
            using (var uow = _uowManager.Begin(new SedUnitOfWorkOptions { IsTransactional = true, Timeout = TimeSpan.FromMinutes(1) }, requiresNew: true))
            {
                var user = await _userManager.Users.FirstAsync(u => u.UserName == userName);
                var accessToken = await _jwtGenerator.GenerateJwtAsync(user);
                var refereshToken = GenerateRefreshToken();
                user.RefreshToken = refereshToken;
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                var expireTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                user.RefreshTokenExpiryTime = expireTime;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new UserFriendlyException($"درخواست شما با خطا مواجه شد");
                }
                return new()
                {
                    AccessToken = accessToken,
                    RefreshToken = refereshToken,
                    ExpiresTime = expireTime
                };
            }
        }

        public async Task<List<TOutput>> GetUsersAsync<TOutput>(List<Guid> userIds)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();

            return await _userManager.Users
                .AsNoTracking()
                .Where(u => userIds.Contains(u.Id))
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public Task<Dictionary<string, int>> GetUserCountInRoleAsync()
        {
            return _context.Users.Where(x => x.PhoneNumberConfirmed)
                                    .SelectMany(
                                        user => _context.UserRoles
                                                    .Where(userRoleMapEntry => user.Id == userRoleMapEntry.UserId)
                                                    .DefaultIfEmpty(),
                                        (user, roleMapEntry) => new { User = user, RoleMapEntry = roleMapEntry })
                                    .SelectMany(
                                         x => _context.Roles
                                                    .Where(role => role.Id == x.RoleMapEntry.RoleId)
                                                    .DefaultIfEmpty(),
                                         (x, role) => new { x.User, Role = role })
                                    .GroupBy(x => x.Role.Name)
                                    .Select(x => new { RoleName = x.Key, Count = x.Count() })
                                    .ToDictionaryAsync(x => x.RoleName, x => x.Count);

        }

        public Task<PagedList<TOutput>> GetAllUserInRoleAsync<TOutput>(string role, int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();

            return _context.Users.Where(x => x.PhoneNumberConfirmed).SelectMany(
                                        user => _context.UserRoles.Where(userRoleMapEntry => user.Id == userRoleMapEntry.UserId).DefaultIfEmpty(),
                                        (user, roleMapEntry) => new { User = user, RoleMapEntry = roleMapEntry })
                                .SelectMany(
                                    x => _context.Roles.Where(role => role.Id == x.RoleMapEntry.RoleId).DefaultIfEmpty(),
                                    (x, role) => new { x.User, Role = role })
                                .Where(x => x.Role.Name == role).Select(x => x.User)
                                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                                .ToPagedList(PageNumber, PageSize, cancellationToken);

        }

        public Task<List<TOutput>> GetAllUserInRoleAsync<TOutput>(Guid roleId, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();

            return _context.Users.Where(x => x.PhoneNumberConfirmed).SelectMany(
                                        user => _context.UserRoles.Where(userRoleMapEntry => user.Id == userRoleMapEntry.UserId).DefaultIfEmpty(),
                                        (user, roleMapEntry) => new { User = user, RoleMapEntry = roleMapEntry })
                                .SelectMany(
                                    x => _context.Roles.Where(role => role.Id == x.RoleMapEntry.RoleId).DefaultIfEmpty(),
                                    (x, role) => new { x.User, Role = role })
                                .Where(x => x.Role.Id == roleId).Select(x => x.User)
                                .ProjectTo<TOutput>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        }

        //public Task<bool> IsEmailConfirmed(string userEmail)
        //{
        //    return _userManager.Users
        //                        .AsNoTracking()
        //                        .AnyAsync(u => u.Email == userEmail && u.EmailConfirmed);
        //}

        public Task<List<RoleDto>> GetAllRoleAsync(Guid userId, CancellationToken cancellationToken)
        {
            return _userManager.GetRolesAsync(userId, cancellationToken);
        }

        public Task<List<string>> GetAllPermissionAsync(Guid userId, CancellationToken cancellationToken)
        {
            return _userManager.GetPermissionsAsync(userId, cancellationToken);
        }

        public Task<List<string>> GetAllPermissionIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return _userManager.GetPermissionIdsAsync(userId, cancellationToken);
        }

        public async Task<List<PermissionDto>> GetAllPermissionFullAsync(Guid userId, CancellationToken cancellationToken = default)
        {

            var result = new List<PermissionDto>();
            var permData = _userManager.GetPermissionsFullAsync(userId, cancellationToken).Result;
            if (permData is null || permData.Count == 0)
            {
                return result;
            }
            foreach (var permissionData in permData)
            {
                result.Add(new()
                {
                    Id = permissionData.Id,
                    Name = permissionData.Name

                });

            }
            return result;
        }

        public async Task<List<PermissionDto>> GetAllPermissionAsync(CancellationToken cancellationToken = default)
        {
            var result = new List<PermissionDto>();
            var permData = _permissionManager.GetAllPermission(cancellationToken).Result;
            if (permData is null || permData.Count == 0)
            {
                return result;
            }
            foreach (var permissionData in permData)
            {
                result.Add(new()
                {
                    Id = permissionData.Id,
                    Name = permissionData.Name

                });

            }
            return result;
        }

        public Task<bool> CheckPermissionAsync(Guid userId, string permission, CancellationToken cancellationToken = default)
        {
            return _userManager.CheckPermissionAsync(userId, permission, cancellationToken);
        }

        public async Task<string> GetVerifiedMobileNumber(Guid userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId.ToString());
            return applicationUser?.PhoneNumber;
        }

        public async Task<UserDto> GetInformationOfUserByUserId(Guid userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId.ToString());
            return _mapper.Map<UserDto>(applicationUser);
        }

        public async Task<Guid?> FindUserIdByUserName(string username)
        {
            return (await _userManager.FindByNameAsync(username))?.Id;
        }

        public async Task<List<UserInputDto>> GetSuggestUser(string username)
        {
            var result = new List<UserInputDto>();

            var usersData = await _userMediator.SearchUsers(username);
            if (usersData is null || usersData.Count == 0)
            {
                return result;
            }
            foreach (var userData in usersData)
            {
                result.Add(new()
                {
                    UserName = userData.UserName,
                    Email = userData["mail"],
                    FirstName = userData["givenName"],
                    LastName = userData["sn"],
                    Sex = userData["otherPager"],
                    EmployeeNumber = userData["physicalDeliveryOfficeName"],
                    PhoneNumber = userData["phoneNumber"],
                    BirthDate = userData["userParameters"]
                });
            }
            return result;
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var userData = await _userManager.FindByIdAsync(userId.ToString());
            if (userData is null)
            {
                throw new UserFriendlyException("کاربری با شناسه اعلامی یافت نشد");
            }

            var userStoreProvider = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IUserStoreProvider>(userData.LoginProvider.ToString());

            var result = await userStoreProvider.ChangePasswordAsync(userData, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new UserFriendlyException($"امکان به روز رسانی رمز عبور وجو ندارد {String.Join(",", result.Errors.Select(x => x.Code))}");
            }
        }

        public async Task SetPasswordAsync(string userName, string password)
        {
            var userData = await _userManager.FindByNameAsync(userName);
            if (userData is null)
            {
                throw new UserFriendlyException("کاربری با شناسه اعلامی یافت نشد");
            }

            var userStoreProvider = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IUserStoreProvider>(userData.LoginProvider.ToString());

            var result = await userStoreProvider.SetPasswordAsync(userData, password);
            if (!result)
            {
                throw new UserFriendlyException("تخصیص پسورد با خطا مواجه گردید");
            }
        }

        public async Task SetPasswordAsync(Guid userId, string password)
        {
            var userData = await _userManager.FindByIdAsync(userId.ToString());
            if (userData is null)
            {
                throw new UserFriendlyException("کاربری با شناسه اعلامی یافت نشد");
            }

            var userStoreProvider = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IUserStoreProvider>(userData.LoginProvider.ToString());
            if (userData.PasswordHash.IsNullOrEmpty().Not())
            {
                throw new UserFriendlyException("امکان تخصیص پسورد وجود ندارد");
            }
            var result = await userStoreProvider.SetPasswordAsync(userData, password);
            if (!result)
            {
                throw new UserFriendlyException("تخصیص پسورد با خطا مواجه گردید");
            }
        }

        public async Task SignOut(Guid userId)
        {
            using (var uow = _uowManager.Begin(new SedUnitOfWorkOptions { IsTransactional = false, Timeout = TimeSpan.FromMinutes(1) }, requiresNew: true))
            {

                var user = await _userManager.FindByIdAsync(userId.ToString());
                user.UserKey = Guid.Empty;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new UserFriendlyException($"درخواست شما با خطا مواجه شد");
                }
                var signInManager = ServiceLocator.ServiceProvider.GetService<SignInManager<ApplicationUser>>();

                await signInManager.SignOutAsync();
            }
        }

        public async Task<bool> IsValidToken(Guid userId,Guid userKey)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId.ToString());
            if(applicationUser.UserKey == userKey)
            {
                return true;
            }
            return false;
        }

        public async Task<TokenDto> RefreshToken(string accessToken, string refreshToken)
        {
            var principal = _jwtGenerator.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                throw new UnauthorizedAccessException("Invalid access token or refresh token");
            }
            string username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new UnauthorizedAccessException("Invalid access token or refresh token");
            }
            var newAccessToken = await _jwtGenerator.GenerateJwtAsync(user);
            var newRefereshToken = GenerateRefreshToken();
            user.RefreshToken = newRefereshToken;
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            var expireTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
            user.RefreshTokenExpiryTime = expireTime;
            await _userManager.UpdateAsync(user);
            return new()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefereshToken,
                ExpiresTime = expireTime,
            };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
