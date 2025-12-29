using Application.RoleManagers.Commands.AssignPermission;
using Application_Frontend.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.RoleManagers.Commands.AssignPermission
{
    public class AssignPermissionCommandHandler : IRequestHandler<AssignPermissionCommand>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        public AssignPermissionCommandHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        public async Task<Unit> Handle(AssignPermissionCommand request, CancellationToken cancellationToken)
        {
            await _clientRequestHandler.PostAsync<bool, AssignPermissionCommand>(request, ApiAddress.AssignPermissionToRole);
            return Unit.Value;
        }
    }
}