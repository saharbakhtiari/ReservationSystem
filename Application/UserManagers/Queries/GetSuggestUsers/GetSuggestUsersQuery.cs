using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using Domain.Users;
using MediatR;
using System.Collections.Generic;

namespace Application.UserManagers.Queries.GetSuggestUsers
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_UserManager)]

    public class GetSuggestUsersQuery : IRequest<List<UserInputDto>>
    {
        public string Filter { get; set; }
    }
}
