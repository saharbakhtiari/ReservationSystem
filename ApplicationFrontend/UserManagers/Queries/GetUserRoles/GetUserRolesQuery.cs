using Application.Common.Security;
using Application.UserManagers.Queries.GetUserRoles;
using Application_Frontend.Common;
using Domain.Common;
using Domain.Permissions;
using Domain.Security;
using Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.UserManagers.Queries.GetUserRoles
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, List<string>>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        public GetUserRolesQueryHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        public async Task<List<string>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var url = string.Format(ApiAddress.GetUserRoles, request.UserId);
            var response = await _clientRequestHandler.GetAsync<List<string>>(url);
            return response;
        }
    }
}
