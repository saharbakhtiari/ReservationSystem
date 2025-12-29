using Application.UserManagers.Commands.LogoutUser;
using Domain.Common;
using Domain.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.LogoutUser
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
    {
        private readonly IUserManager _userManager;
        private readonly ICurrentUserService _currentUserService;

        public LogoutUserCommandHandler(IUserManager userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is not null)
            {
                await _userManager.SignOut(_currentUserService.UserId.Value);
            }
            return Unit.Value;
        }
    }
}