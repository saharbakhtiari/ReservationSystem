using Application.Common.Security;
using Domain.Common;
using Domain.Permissions;
using Domain.Roles;
using Domain.Security;
using MediatR;


namespace Application.RoleManagers.Queries.GetRoles
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_RoleManager)]
    public class GetRolesQuery : IRequest<PagedList<RoleDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
