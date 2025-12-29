using Application.Common.Security;
using Domain.Users;
using MediatR;
using System.Collections.Generic;

namespace Application.UserManagers.Queries.GetPermission
{
    [Authorize]
    public class GetPermissionQuery : IRequest<List<PermissionDto>>
    {
    }
}