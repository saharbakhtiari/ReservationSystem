using Application.UserManagers.Commands.LoginUser;
using Application_Frontend.Common;
using Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.UserManagers.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand,string>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        private readonly IAuthService _authService;
        public LoginUserCommandHandler(IClientSideRequestHandler clientRequestHandler, IAuthService authService)
        {
            _clientRequestHandler = clientRequestHandler;
            _authService = authService;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            string token =  await _clientRequestHandler.PostAsync<string, LoginUserCommand>(request, ApiAddress.LoginUser);
            await _authService.UpdateToken(token);
            return token;
        }
    }
}