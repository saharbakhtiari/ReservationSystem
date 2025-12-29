using Application.RoleManagers.Queries.GetPermission;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.RoleManagers.Queries.GetPermission
{
    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, List<PermissionDto>>
    {
        private readonly IUserManager _userManager;
        public GetPermissionQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<List<PermissionDto>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            return _userManager.GetRolePermissionsFullAsync(request.RoleId, cancellationToken);
        }
    }
}