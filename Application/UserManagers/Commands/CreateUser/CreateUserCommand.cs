using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using Domain.Users;
using MediatR;

namespace Application.UserManagers.Commands.CreateUser
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_UserManager)]
    public class CreateUserCommand : IRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }
        [RedactFromLogs]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string EmployeeNumber { get; set; }

        public LoginProvider LoginProvider { get; set; }
    }
}