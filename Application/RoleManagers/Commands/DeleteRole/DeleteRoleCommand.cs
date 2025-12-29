using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;

namespace Application.RoleManagers.Commands.DeleteRole
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_RoleManager)]
    public class DeleteRoleCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
