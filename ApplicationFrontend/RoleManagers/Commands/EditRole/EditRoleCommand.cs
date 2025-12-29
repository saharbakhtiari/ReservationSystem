using Application.RoleManagers.Commands.EditRole;
using Application_Frontend.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.RoleManagers.Commands.EditRole
{

    public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        public EditRoleCommandHandler(IClientSideRequestHandler clientRequestHandler = null)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        public async Task<Unit> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            await _clientRequestHandler.PostAsync<bool, EditRoleCommand>(request, ApiAddress.EditRole);
            return Unit.Value;
        }
    }
}
