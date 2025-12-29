using Application.RoleManagers.Commands.CreateRole;
using Domain.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.RoleManagers.Commands.CreateRole
{

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand,Guid>
    {
        private readonly IUserManager _userManager;
        public CreateRoleCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var id = await _userManager.CreateRoleAsync(request.RoleKey.Trim(),request.RoleTitle.Trim());
            return id;
        }
    }
}
