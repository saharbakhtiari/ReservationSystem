using Application.UserManagers.Commands.RefreshToken;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.LoginUser
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
    {
        private readonly IUserManager _userManager;

        public RefreshTokenCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _userManager.RefreshToken(request.AccessToken, request.RefreshToken);
            return token;
        }
    }
}