using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;

namespace Application.UserManagers.Commands.DeleteUser
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_UserManager)]
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}