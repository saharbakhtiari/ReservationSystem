using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.RoleManagers.Commands.AssignPermission
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_RoleManager)]
    public class AssignPermissionCommand : IRequest
    {
        public Guid RoleId { get; set; }
        public List<string> Permissions { get; set; }
    }
}