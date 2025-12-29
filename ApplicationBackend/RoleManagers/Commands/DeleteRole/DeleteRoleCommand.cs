using Application.RoleManagers.Commands.DeleteRole;
using Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.RoleManagers.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IUserManager _userManager;
        public DeleteRoleCommandHandler(IUserManager userManager = null)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            await _userManager.DeleteRoleAsync(request.Id);
            return Unit.Value;
        }
    }
}
