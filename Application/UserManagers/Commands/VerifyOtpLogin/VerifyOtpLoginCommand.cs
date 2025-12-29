using Domain.Users;
using MediatR;

namespace Application.UserManagers.Commands.VerifyOtpLogin
{
    //  [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_UserManager_Create)]
    public class VerifyOtpLoginCommand : IRequest<TokenDto>
    {
        public string PhoneNumber { get; set; }
        public string VerifyCode { get; set; }
    }
}