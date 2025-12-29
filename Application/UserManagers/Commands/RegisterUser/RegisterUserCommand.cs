using MediatR;

namespace Application.UserManagers.Commands.RegisterUser
{
    //  [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_UserManager_Create)]
    public class RegisterUserCommand : IRequest
    {
        public string PhoneNumber { get; set; }
    }
}