using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.RoleManagers.Commands.DeleteRole
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteRoleCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).NotEmpty().WithMessage(_localizer["Select a role"]);
        }
    }
}
