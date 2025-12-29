using Application.UserManagers.Queries.GetUserRoles;
using Domain.Common;
using Domain.Roles;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Queries.GetUserRoles
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, List<RoleDto>>
    {
        private readonly IUserManager _userManager;
        public GetUserRolesQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<List<RoleDto>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            return _userManager.GetAllRoleAsync(request.UserId, cancellationToken);
        }
    }
}
