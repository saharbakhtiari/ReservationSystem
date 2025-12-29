using Application.Common.Security;
using Domain.Common;
using Domain.Permissions;
using Domain.Security;
using Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagers.Queries.GetUsers
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_UserManager)]
    public class GetUsersQuery : IRequest<PagedList<UserDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
