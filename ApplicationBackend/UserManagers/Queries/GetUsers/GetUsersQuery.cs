using Application.Common.Security;
using Application.UserManagers.Queries.GetUsers;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedList<UserDto>>
    {
        private readonly IUserManager _userManager;
        public GetUsersQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<PagedList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return _userManager.GetAllUserAsync<UserDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
