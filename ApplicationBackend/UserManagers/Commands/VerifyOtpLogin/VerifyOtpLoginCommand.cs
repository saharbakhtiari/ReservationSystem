using Application.UserManagers.Commands.VerifyOtpLogin;
using AutoMapper;
using Domain.Common;
using Domain.MemberProfiles;
using Domain.Users;
using Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.VerifyOtpLogin
{
    public class VerifyOtpLoginCommandHandler : IRequestHandler<VerifyOtpLoginCommand, TokenDto>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public VerifyOtpLoginCommandHandler(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<TokenDto> Handle(VerifyOtpLoginCommand request, CancellationToken cancellationToken)
        {
            await _userManager.VerifyOtpAsync(request.PhoneNumber.Trim(), request.VerifyCode, cancellationToken);
            try
            {
                await _userManager.AuthenticateAsync(request.PhoneNumber.Trim());
            }
            catch (UserFriendlyException ex)
            {
                var userid = await _userManager.RegisterUser(request.PhoneNumber, cancellationToken);
                var profile = _mapper.Map<MemberProfile>(request);
                profile.UserId = userid;
                await profile.SaveAsync(cancellationToken);
                await _userManager.AuthenticateAsync(request.PhoneNumber.Trim());
            }

            return await _userManager.GetUserTokenIdAsync(request.PhoneNumber.Trim());
        }
    }
}
