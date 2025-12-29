using Application.RoleManagers.Commands.EditRole;
using Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.RoleManagers.Commands.EditRole
{
    public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand>
    {
        private readonly IUserManager _userManager;
        public EditRoleCommandHandler(IUserManager userManager = null)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            await _userManager.UpdateRoleAsync(request.Id,request.RoleKey,request.RoleTitle);
            return Unit.Value;
        }
    }
}
