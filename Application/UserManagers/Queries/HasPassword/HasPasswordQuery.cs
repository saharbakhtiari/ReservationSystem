using Application.Common.Security;
using MediatR;

namespace Application.UserManagers.Queries.HasPassword
{
    [Authorize]
    public class HasPasswordQuery : IRequest<bool>
    {
    }
}
