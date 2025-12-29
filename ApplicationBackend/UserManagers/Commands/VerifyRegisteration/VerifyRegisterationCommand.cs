using Application.UserManagers.Commands.VerifyRegisteration;
using AutoMapper;
using Domain.Common;
using Domain.MemberProfiles;
using Domain.Users;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.VerifyRegisteration
{
    public class VerifyRegisterationCommandHandler : IRequestHandler<VerifyRegisterationCommand, TokenDto>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public VerifyRegisterationCommandHandler(IUserManager userManager, IMapper mapper, IStringLocalizer localizer)
        {
            _userManager = userManager;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<TokenDto> Handle(VerifyRegisterationCommand request, CancellationToken cancellationToken)
        {
            var old = await MemberProfile.GetProfileAsync(request.NationalId, cancellationToken);
            if (old != null)
            {
                throw new UserFriendlyException(_localizer["NationalId is exist"]);
            }
            await _userManager.VerifyOtpAsync(request.PhoneNumber.Trim(),request.VerifyCode, cancellationToken);
            var userid = await _userManager.RegisterUser(request.PhoneNumber,request.NationalId, cancellationToken);
            var profile = _mapper.Map<MemberProfile>(request);
            profile.UserId = userid;
            await profile.SaveAsync(cancellationToken);
            await _userManager.AuthenticateAsync(request.PhoneNumber.Trim());
            string accessToken = await _userManager.GetUserTokenIdAsync(request.PhoneNumber.Trim());
            TokenDto tokenDto = new TokenDto();
            tokenDto.expires_in = 2222;
            tokenDto.access_token = accessToken;
            tokenDto.refreshToken = accessToken;
            return tokenDto;
        }
    }
}
