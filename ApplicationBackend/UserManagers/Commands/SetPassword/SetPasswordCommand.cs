using Application.UserManagers.Commands.SetPassword;
using Domain.Common;
using Domain.Common.Interfaces;
using Exceptions;
using Extensions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.SetPassword
{
    public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand>
    {
        private readonly IUserManager _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStringLocalizer _localizer;
        public SetPasswordCommandHandler(IUserManager userManager, ICurrentUserService currentUserService, IStringLocalizer localizer)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId.HasValue.Not())
            {
                throw new UserFriendlyException(_localizer["User is not login"]);
            }
            await _userManager.SetPasswordAsync(_currentUserService.UserId.Value, request.Password);
            return Unit.Value;
        }
    }
}