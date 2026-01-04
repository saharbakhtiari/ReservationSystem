using Application.Common.Security;
using Domain.Users;
using MediatR;

namespace Application.UserManagers.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<TokenDto>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
