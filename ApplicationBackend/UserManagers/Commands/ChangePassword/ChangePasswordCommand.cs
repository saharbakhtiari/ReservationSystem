using Application.UserManagers.Commands.ChangePassword;
using Domain.Common;
using Domain.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUserManager _userManager;
        private readonly ICurrentUserService _currentUserService;
        public ChangePasswordCommandHandler(IUserManager userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await _userManager.ChangePasswordAsync(_currentUserService.UserId.Value, request.CurrentPassword, request.NewPassword);
            return Unit.Value;
        }
    }
}