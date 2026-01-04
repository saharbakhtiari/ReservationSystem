using Application.UserManagers.Commands.LoginUser;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand,TokenDto>
    {
        private readonly IUserManager _userManager;

        public LoginUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            await _userManager.AuthenticateAsync(request.UserName.Trim(), request.Password.Trim());

            return await _userManager.GetUserTokenIdAsync(request.UserName.Trim());
        }
    }
}