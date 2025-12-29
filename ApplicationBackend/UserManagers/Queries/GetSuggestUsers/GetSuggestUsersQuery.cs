using Application.Common.Security;
using Application.UserManagers.Queries.GetSuggestUsers;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Queries.GetSuggestUsers
{
    public class GetSuggestUsersQueryHandler : IRequestHandler<GetSuggestUsersQuery, List<UserInputDto>> 
    {
        private readonly IUserManager _userManager;
        public GetSuggestUsersQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<List<UserInputDto>> Handle(GetSuggestUsersQuery request, CancellationToken cancellationToken)
        {
            return _userManager.GetSuggestUser(request.Filter);
        }
    }
}
