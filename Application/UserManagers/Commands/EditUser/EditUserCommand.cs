using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;

namespace Application.UserManagers.Commands.EditUser
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_UserManager)]
    public class EditUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}