using Application.UserManagers.Commands.CreateUser;
using Application_Frontend.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.UserManagers.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;

        public CreateUserCommandHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _clientRequestHandler.PostAsync<bool, CreateUserCommand>(request, ApiAddress.CreateUser);
            return Unit.Value;
        }
    }
}
