using Application.Common.Security;
using Application.UserManagers.Commands.EditUser;
using Application_Frontend.Common;
using Domain.Common;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.UserManagers.Commands.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        public EditUserCommandHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        public async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            await _clientRequestHandler.PostAsync<bool, EditUserCommand>(request, ApiAddress.EditUser);
            return Unit.Value;
        }
        
    }
}