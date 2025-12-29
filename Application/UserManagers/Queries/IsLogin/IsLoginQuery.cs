using Application.Common.Security;
using MediatR;

namespace Application.UserManagers.Queries.IsLogin
{
    [Authorize]
    public class IsLoginQuery : IRequest
    {
    }
}
