using Application.UserManagers.Commands.VerifyOtpLogin;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.VerifyOtpLogin
{
    public class VerifyOtpLoginCommandHandler : IRequestHandler<VerifyOtpLoginCommand, TokenDto>
    {
        private readonly IUserManager _userManager;

        public VerifyOtpLoginCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<TokenDto> Handle(VerifyOtpLoginCommand request, CancellationToken cancellationToken)
        {
            await _userManager.VerifyOtpAsync(request.PhoneNumber.Trim(), request.VerifyCode, cancellationToken);
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
