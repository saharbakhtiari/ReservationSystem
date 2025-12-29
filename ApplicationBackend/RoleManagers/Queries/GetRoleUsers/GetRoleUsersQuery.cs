using Application.RoleManagers.Queries.GetRoleUsers;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.RoleManagers.Queries.GetRoleUsers
{
    public class GetRoleUsersQueryHandler : IRequestHandler<GetRoleUsersQuery, List<UserInputDto>>
    {
        private readonly IUserManager _userManager;
        public GetRoleUsersQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public  Task<List<UserInputDto>> Handle(GetRoleUsersQuery request, CancellationToken cancellationToken)
        {
            return _userManager.GetAllUserInRoleAsync<UserInputDto>(request.RoleId, cancellationToken);
        }
    }
}
