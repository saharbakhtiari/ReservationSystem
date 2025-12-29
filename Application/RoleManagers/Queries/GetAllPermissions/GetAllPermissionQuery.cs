using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.RoleManagers.Queries.GetPermission
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_RoleManager)]
    public class GetAllPermissionQuery : IRequest<List<PermissionDto>>
    {
        
    }
}