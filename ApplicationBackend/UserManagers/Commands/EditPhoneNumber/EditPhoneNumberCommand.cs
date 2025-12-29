using Application.UserManagers.Commands.EditPhoneNumber;
using AutoMapper;
using Domain.Common;
using Domain.Common.Interfaces;
using Domain.MemberProfiles;
using Domain.Users;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.EditPhoneNumber
{
    public class EditPhoneNumberCommandHandler : IRequestHandler<EditPhoneNumberCommand>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStringLocalizer _localizer;
        public EditPhoneNumberCommandHandler(IUserManager userManager, IMapper mapper, ICurrentUserService currentUserService, IStringLocalizer localizer)
        {
            _userManager = userManager;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(EditPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            await _userManager.VerifyOtpAsync(request.PhoneNumber.Trim(), request.VerifyCode, cancellationToken);
            var user = _mapper.Map<UserInputDto>(request);
            user.Id = _currentUserService.UserId.HasValue ?
                             _currentUserService.UserId.Value :
                             throw new UserFriendlyException(_localizer["User is not login"]);
            await _userManager.UpdatePhoneNumberAsync(user);
            var profile = await MemberProfile.GetProfileAsync(user.Id, cancellationToken);
            _mapper.Map(request, profile);
            await profile.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}