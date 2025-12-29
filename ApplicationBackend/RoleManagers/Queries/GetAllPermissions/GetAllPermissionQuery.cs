using Application.Common.Security;
using Domain.Common;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Application.RoleManagers.Queries.GetPermission;
using Domain.Users;

namespace Application_Backend.RoleManagers.Queries.GetAllPermissions
{
    

    public class GetAllPermissionQueryHandler : IRequestHandler<GetAllPermissionQuery, List<PermissionDto>>
    {
        private readonly IUserManager _userManager;
        public GetAllPermissionQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<List<PermissionDto>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
        {
            return _userManager.GetAllPermissionAsync(cancellationToken);
        }
    }
}