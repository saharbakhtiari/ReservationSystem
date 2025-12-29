using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.RoleManagers.Commands.EditRole
{
    public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
    {
        private readonly IStringLocalizer _localizer;
        public EditRoleCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).NotEmpty().WithMessage(_localizer["Select a role"]);
            RuleFor(p => p.RoleKey).NotEmpty().WithMessage(_localizer["Role key is empty"]);
            RuleFor(p => p.RoleTitle).NotEmpty().WithMessage(_localizer["Title is empty"]);
        }
    }
}
