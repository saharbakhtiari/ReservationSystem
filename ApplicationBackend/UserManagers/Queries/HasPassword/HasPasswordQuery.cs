using Application.UserManagers.Queries.HasPassword;
using Domain.Common;
using Domain.Common.Interfaces;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Queries.HasPassword
{
    public class HasPasswordQueryHandler : IRequestHandler<HasPasswordQuery, bool>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserManager _userManager;
        private readonly IStringLocalizer _localizer;
        public HasPasswordQueryHandler(ICurrentUserService currentUserService, IUserManager userManager, IStringLocalizer localizer)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _localizer = localizer;
        }

        public Task<bool> Handle(HasPasswordQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId.HasValue ?
                             _currentUserService.UserId.Value :
                             throw new UserFriendlyException(_localizer["User is not login"]);
            return _userManager.HasPasswordAsync(userId);
        }
    }
}
