using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.UserManagers.Commands.AssignRole
{
   [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_RoleManager)]
    public class AssignRoleCommand : IRequest
    {
        public Guid UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}