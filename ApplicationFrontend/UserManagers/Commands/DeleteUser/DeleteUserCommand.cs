using Application.UserManagers.Commands.DeleteUser;
using Application_Frontend.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.UserManagers.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        public DeleteUserCommandHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _clientRequestHandler.PostAsync<bool, DeleteUserCommand>(request, ApiAddress.DeleteUser);
            return Unit.Value;
        }
    }
}