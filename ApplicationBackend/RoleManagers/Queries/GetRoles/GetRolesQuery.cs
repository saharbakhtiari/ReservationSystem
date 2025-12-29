using Application.Common.Security;
using Application.RoleManagers.Queries.GetRoles;
using Domain.Common;
using Domain.Permissions;
using Domain.Roles;
using Domain.Security;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.RoleManagers.Queries.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PagedList<RoleDto>>
    {
        private readonly IUserManager _userManager;
        public GetRolesQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<PagedList<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return _userManager.GetAllRolesAsync<RoleDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
