using Application.Common.Security;
using Application.UserManagers.Commands.DeleteUser;
using Domain.Common;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserManager _userManager;
        public DeleteUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userManager.DeleteUserAsync(request.Id);
            return Unit.Value;
        }
    }
}