using Application.RoleManagers.Commands.AssignPermission;
using Application_Frontend.Common;
using Domain.Common;
using Domain.Security;
using Extensions;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.RoleManagers.Commands.AssignPermission
{
    public class AssignPermissionCommandValidator : AbstractValidator<AssignPermissionCommand>
    {
        private readonly IAuthService _authService;
        public AssignPermissionCommandValidator(IAuthService authService)
        {
            _authService = authService;
            RuleFor(v => v.Permissions).MustAsync(IncludeInUserPermission).WithMessage("نمی توانید دسترسی که خود ندارید به نقشی اعطا کنید");
        }
        private async Task<bool> IncludeInUserPermission(List<string> permissions, CancellationToken cancellationToken)
        {
            var userPermission = await _authService.GetUserPermissions();
            return await _authService.HasPermissionOrRole("", DefaultRoleNames.Admin) || permissions.Any(x => userPermission.Contains(x).Not()).Not();
        }
    }
}
