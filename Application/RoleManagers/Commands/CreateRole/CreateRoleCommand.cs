using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;

namespace Application.RoleManagers.Commands.CreateRole
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_RoleManager)]
    public class CreateRoleCommand : IRequest<Guid>
    {
        public string RoleKey { get; set; }
        public string RoleTitle { get; set; }
    }
}
