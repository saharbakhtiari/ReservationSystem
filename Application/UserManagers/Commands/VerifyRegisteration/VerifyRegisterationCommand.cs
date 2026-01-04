using Domain.Users;
using MediatR;

namespace Application.UserManagers.Commands.VerifyRegisteration
{
    //  [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_UserManager_Create)]
    public class VerifyRegisterationCommand : IRequest<TokenDto>
    {
        public string PhoneNumber { get; set; }
        public string VerifyCode { get; set; }
    }
}