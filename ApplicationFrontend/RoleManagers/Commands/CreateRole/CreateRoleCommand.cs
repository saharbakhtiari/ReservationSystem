using Application.RoleManagers.Commands.CreateRole;
using Application_Frontend.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.RoleManagers.Commands.CreateRole
{

    public class CreateRoleCommandHandler : IRequestHandler<CreateRuleCommand>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        public CreateRoleCommandHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        public async Task<Unit> Handle(CreateRuleCommand request, CancellationToken cancellationToken)
        {
            await _clientRequestHandler.PostAsync<bool, CreateRuleCommand>(request, ApiAddress.CreateRole);
            return Unit.Value;
        }
    }
}
