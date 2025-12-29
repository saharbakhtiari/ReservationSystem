using Domain.Roles;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IUserManager
    {
        Task AssignRoleAsync(Guid userId, string roleName);
        Task AssignRoleAsyncByRoleId(Guid userId, string roleName);


        Task AuthenticateAsync(string userName, string password);
        Task<Guid> CreateUserAsync(string userName, string password, LoginProvider loginProvider);
        Task<Guid> CreateUserAsync(string userName, string password, string firstName, string lastName, string phoneNumber, string employeeNumber, LoginProvider loginProvider);
        Task SendRegisterOtpCodeAsync(string userName, CancellationToken cancellationToken);
        Task<Guid> CreateRoleAsync(string roleName, string roleTilte);
        Task<List<RoleDto>> GetAllRoleAsync(Guid userId, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetAllUserInRoleAsync<TOutput>(string role, int PageNumber, int PageSize, CancellationToken cancellationToken);

        public Task<List<TOutput>> GetAllUserInRoleAsync<TOutput>(Guid role, CancellationToken cancellationToken);

        Task<PagedList<TOutput>> GetAllUserAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
        Task<Dictionary<string, int>> GetUserCountInRoleAsync();
        Task<string> GetUserNameAsync(Guid userId);
        Task<List<TOutput>> GetUsersAsync<TOutput>(List<Guid> userIds);
        Task<string> GetUserTokenIdAsync(string userName);
        Task<string> GetVerifiedMobileNumber(Guid userId);
        //Task<bool> IsEmailConfirmed(string userEmail);
        Task<bool> UserIsInRoleAsync(Guid userId, string role);
        Task<UserDto> GetInformationOfUserByUserId(Guid userId);
        Task<List<UserInputDto>> GetSuggestUser(string username);
        Task<List<string>> GetAllPermissionAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<string>> GetAllPermissionIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<PermissionDto>> GetAllPermissionFullAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<List<PermissionDto>> GetAllPermissionAsync(CancellationToken cancellationToken = default);
        Task<Guid?> FindUserIdByUserName(string username);
        Task<bool> CheckPermissionAsync(Guid userId, string permission, CancellationToken cancellationToken = default);
        Task UpdateRoleAsync(Guid id, string roleName, string roleTitle);
        Task DeleteRoleAsync(Guid id);
        Task<PagedList<TOutput>> GetAllRolesAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
        Task UpdateUserAsync(UserInputDto user);
        Task DeleteUserAsync(Guid id);
        Task UnAssignRoleAsync(Guid userId, string roleName);
        Task<List<string>> GetRolePermissionsAsync(Guid roleId, CancellationToken cancellationToken);
        Task<List<string>> GetRolePermissionIdsAsync(Guid roleId, CancellationToken cancellationToken);
        Task<List<PermissionDto>> GetRolePermissionsFullAsync(Guid roleId, CancellationToken cancellationToken);

        Task AssignPermissionToRoleAsync(Guid roleId, List<string> Permissions, CancellationToken cancellationToken);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        Task SetPasswordAsync(string userName, string password);
        Task VerifyOtpAsync(string phoneNumber, string verifyCode, CancellationToken cancellationToken);
        Task SetPasswordAsync(Guid userId, string password);
        Task AuthenticateAsync(string userName);
        Task<Guid> RegisterUser(string phoneNumber, string nationalId, CancellationToken cancellationToken);
        Task SendLoginOtpCodeAsync(string phoneNumber, CancellationToken cancellationToken);
        Task<bool> HasPasswordAsync(Guid userId);
        Task UpdatePhoneNumberAsync(UserInputDto user);
        Task<Guid> RegisterUser(UserInputDto userDto, CancellationToken cancellationToken);
        Task SignOut(Guid userId);
        Task<bool> IsSignedIn(Guid userId);
    }
}