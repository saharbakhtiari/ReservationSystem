using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;

namespace Application.RoleManagers.Commands.EditRole
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_RoleManager)]
    public class EditRoleCommand : IRequest
    {
        public Guid Id { get; set; }
        public string RoleKey { get; set; }

        public string RoleTitle { get; set; }
    }
}
