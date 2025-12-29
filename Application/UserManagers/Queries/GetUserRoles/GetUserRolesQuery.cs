using Application.Common.Security;
using Domain.Permissions;
using Domain.Roles;
using Domain.Security;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.UserManagers.Queries.GetUserRoles
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_RoleManager)]
    public class GetUserRolesQuery : IRequest<List<RoleDto>>
    {
        public Guid UserId { get; set; }
    }
}
