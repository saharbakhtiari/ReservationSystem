using Application.Common.Security;
using Domain.Users;
using MediatR;

namespace Application.UserManagers.Queries.IsLogin
{
    [Authorize]
    public class GetCurrentUserInfoQuery : IRequest<UserDto>
    {
    }
}
