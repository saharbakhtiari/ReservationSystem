using Application.RoleManagers.Queries.GetPermission;
using Application_Frontend.Common;
using Domain.Common;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.RoleManagers.Queries.GetPermission
{
    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, List<string>>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        public GetPermissionQueryHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }
        
        public Task<List<string>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            var url = string.Format(ApiAddress.GetRolePermissions, request.RoleId);
            return _clientRequestHandler.GetAsync<List<string>>(url);
        }
    }
}