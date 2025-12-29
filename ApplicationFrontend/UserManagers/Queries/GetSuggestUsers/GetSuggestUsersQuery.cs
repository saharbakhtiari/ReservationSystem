using Application.Common.Security;
using Application.UserManagers.Queries.GetSuggestUsers;
using Application_Frontend.Common;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.UserManagers.Queries.GetSuggestUsers
{
    public class GetSuggestUsersQueryHandler : IRequestHandler<GetSuggestUsersQuery, List<UserDto>> 
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        public GetSuggestUsersQueryHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        public Task<List<UserDto>> Handle(GetSuggestUsersQuery request, CancellationToken cancellationToken)
        {
            var url = string.Format(ApiAddress.GetSuggestUsers, request.Filter);
            return  _clientRequestHandler.GetAsync<List<UserDto>>(url);
        }
    }
}
