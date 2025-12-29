using Application.UserManagers.Commands.AssignRole;
using Domain.Common;
using Domain.Common.Interfaces;
using Domain.Security;
using Exceptions;
using Extensions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.AssignRole
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand>
    {
        private readonly IUserManager _userManager;
        private readonly ICurrentUserService _currentUserService;

        public AssignRoleCommandHandler(IUserManager userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {

            //var currentUserAllRoles = await _userManager.GetAllRoleAsync(_currentUserService.UserId.Value, cancellationToken);

            if (request.Roles.Any(x => x.ToUpper() == DefaultRoleNames.Admin.ToUpper()) && (await _userManager.UserIsInRoleAsync(_currentUserService.UserId.Value, DefaultRoleNames.Admin)).Not())
            {
                throw new UserFriendlyException("دسترسی ادمین را فقط ادمین می تواند تخصیص دهد");
            }
            if (request.Roles.Any(x => x.ToUpper() == DefaultRoleNames.Admin.ToUpper()).Not() && (await _userManager.UserIsInRoleAsync(request.UserId, DefaultRoleNames.Admin)) && (await _userManager.UserIsInRoleAsync(_currentUserService.UserId.Value, DefaultRoleNames.Admin)).Not())
            {
                throw new UserFriendlyException("دسترسی ادمین را فقط ادمین می تواند بازپس گیرد");
            }

            var userAllRoles = await _userManager.GetAllRoleAsync(request.UserId, cancellationToken);

            var deleteRoles = userAllRoles.Where(x => request.Roles.Contains(x.Id.ToString()).Not());
            var newRoles = request.Roles.Where(x => userAllRoles.Select(x=>x.Id.ToString()).Contains(x).Not());

            foreach (var roleName in newRoles)
            {
                await _userManager.AssignRoleAsyncByRoleId(request.UserId, roleName);
            }
            foreach (var roleName in deleteRoles)
            {
                await _userManager.UnAssignRoleAsync(request.UserId, roleName.Name);
            }

            return Unit.Value;
        }
    }
}