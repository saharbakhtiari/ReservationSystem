using Application.UserManagers.Queries.IsLogin;
using Domain.Common;
using Domain.Common.Interfaces;
using Domain.Users;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Queries.IsLogin
{
    public class GetCurrentUserInfoQueryHandler : IRequestHandler<GetCurrentUserInfoQuery,UserDto>
    {
        private readonly IUserManager _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStringLocalizer _localizer;

        public GetCurrentUserInfoQueryHandler(IUserManager userManager, ICurrentUserService currentUserService, IStringLocalizer localizer)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _localizer = localizer;
        }

        public async Task<UserDto> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId.HasValue ?
                             _currentUserService.UserId.Value :
                             throw new UserFriendlyException(_localizer["User is not login"]);
            var user = await _userManager.GetInformationOfUserByUserId(userId);
            return user;
        }
    }
}
