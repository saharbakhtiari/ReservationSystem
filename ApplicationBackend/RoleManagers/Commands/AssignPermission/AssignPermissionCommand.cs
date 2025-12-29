using Application.RoleManagers.Commands.AssignPermission;
using Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.RoleManagers.Commands.AssignPermission
{
    public class AssignPermissionCommandHandler : IRequestHandler<AssignPermissionCommand>
    {
        private readonly IUserManager _userManager;
        public AssignPermissionCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(AssignPermissionCommand request, CancellationToken cancellationToken)
        {
            await _userManager.AssignPermissionToRoleAsync(request.RoleId, request.Permissions, cancellationToken);
            return Unit.Value;
        }
    }
}