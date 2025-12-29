using Application.UserManagers.Queries.IsLogin;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Queries.IsLogin
{
    public class IsLoginQueryHandler : IRequestHandler<IsLoginQuery>
    {
        public Task<Unit> Handle(IsLoginQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}
