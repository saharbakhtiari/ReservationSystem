using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.RoleManagers.Commands.AssignPermission
{
    public class AssignPermissionCommandValidator : AbstractValidator<AssignPermissionCommand>
    {
        private readonly IStringLocalizer _localizer;
        public AssignPermissionCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(v => v.RoleId).NotEmpty().WithMessage(_localizer["Select a role"]);
            RuleFor(v => v.Permissions).NotNull().WithMessage(_localizer["Permissions is not vlid"]);
        }
    }
}
