using Application.UserManagers.Commands.EditRegisteredUser;
using AutoMapper;
using Domain.MemberProfiles;
using Domain.Common;
using Domain.Common.Interfaces;
using Domain.Users;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.EditRegisteredUser
{
    public class EditProfileUserCommandHandler : IRequestHandler<EditProfileUserCommand>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStringLocalizer _localizer;
        public EditProfileUserCommandHandler(IUserManager userManager, IMapper mapper, ICurrentUserService currentUserService, IStringLocalizer localizer)
        {
            _userManager = userManager;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(EditProfileUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UserInputDto>(request);
            user.Id = _currentUserService.UserId.HasValue ?
                             _currentUserService.UserId.Value :
                             throw new UserFriendlyException(_localizer["User is not login"]);
            await _userManager.UpdateUserAsync(user);
            var profile = await MemberProfile.GetProfileAsync(user.Id, cancellationToken);
            _mapper.Map(request, profile);
            await profile.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}