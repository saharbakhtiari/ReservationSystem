using Application.Common.Security;
using Domain.Users;
using MediatR;

namespace Application.UserManagers.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<TokenDto>
    {
        public string UserName { get; set; }

        [RedactFromLogs]
        public string Password { get; set; }
    }
}