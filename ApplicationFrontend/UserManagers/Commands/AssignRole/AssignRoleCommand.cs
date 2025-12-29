using Application.UserManagers.Commands.AssignRole;
using Application_Frontend.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.UserManagers.Commands.AssignRole
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand>
    {

        private readonly IClientSideRequestHandler _clientRequestHandler;

        public AssignRoleCommandHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        public async Task<Unit> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            await _clientRequestHandler.PostAsync<bool, AssignRoleCommand>(request, ApiAddress.AssignRoleToUser);
            return Unit.Value;
        }
    }
}