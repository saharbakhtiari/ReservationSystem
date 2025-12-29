using Application.RoleManagers.Commands.DeleteRole;
using Application_Frontend.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.RoleManagers.Commands.DeleteRole
{


    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;

        public DeleteRoleCommandHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }


        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            await _clientRequestHandler.PostAsync<bool, DeleteRoleCommand>(request, ApiAddress.DeleteRole);
            return Unit.Value;
        }
    }
}
