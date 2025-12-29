using Application.UserManagers.Queries.GetPermission;
using Application_Frontend.Common;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.UserManagers.Queries.GetPermission
{
    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, List<string>>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        private readonly IAuthService _authService;
        public GetPermissionQueryHandler(IClientSideRequestHandler clientRequestHandler, IAuthService authService)
        {
            _clientRequestHandler = clientRequestHandler;
            _authService = authService;
        }

        public async Task<List<string>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            List<string> permissions = await _clientRequestHandler.GetAsync<List<string>>(ApiAddress.GetPermissions);
            await _authService.UpdatePermissions(permissions);
            return permissions;
        }
    }
}