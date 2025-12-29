using Application.UserManagers.Queries.GetPermission;
using Domain.Common;
using Domain.Common.Interfaces;
using Domain.Users;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Queries.GetPermission
{
    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, List<PermissionDto>>
    {
        private readonly IUserManager _userManager;
        private readonly ICurrentUserService _currentUserService;

        public GetPermissionQueryHandler(IUserManager userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public Task<List<PermissionDto>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            return _userManager.GetAllPermissionFullAsync(_currentUserService.UserId.Value, cancellationToken);
        }
    }
}