using Application.RoleManagers.Commands.AssignPermission;
using Domain.Common;
using Domain.Common.Interfaces;
using Domain.Security;
using Extensions;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.RoleManagers.Commands.AssignPermission
{
    public class AssignPermissionCommandValidator : AbstractValidator<AssignPermissionCommand>
    {
        private readonly IUserManager _userManager;
        private readonly ICurrentUserService _currentUserService;
        private List<string> _userPermissions;
        public AssignPermissionCommandValidator(IUserManager userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            RuleFor(v => v.Permissions).MustAsync(IncludeInUserPermission).WithMessage("نمی توانید دسترسی که خود ندارید به نقشی اعطا کنید");
            RuleFor(v => v).MustAsync(UserHasMorePermission).WithMessage("نمی توانید نقشی که دسترسی متفاوت از شما دارد را تغییر دهید");
        }
        private async Task<bool> IncludeInUserPermission(List<string> permissions, CancellationToken cancellationToken)
        {
            _userPermissions = _userPermissions is null ?
                await _userManager.GetAllPermissionIdAsync(_currentUserService.UserId.Value, cancellationToken) : _userPermissions;
            return await _userManager.UserIsInRoleAsync(_currentUserService.UserId.Value, DefaultRoleNames.Admin) || permissions.Any(x => _userPermissions.Contains(x).Not()).Not();
        }
        private async Task<bool> UserHasMorePermission(AssignPermissionCommand assignPermission, CancellationToken cancellationToken)
        {
            _userPermissions = _userPermissions is null ? await _userManager.GetAllPermissionIdAsync(_currentUserService.UserId.Value, cancellationToken) : _userPermissions;
            var rolePermission = await _userManager.GetRolePermissionIdsAsync(assignPermission.RoleId, cancellationToken);
            return await _userManager.UserIsInRoleAsync(_currentUserService.UserId.Value, DefaultRoleNames.Admin) || rolePermission.Any(x => _userPermissions.Contains(x).Not()).Not();
        }
    }
}
