using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.RoleManagers.Commands.CreateRole
{
   public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateRoleCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.RoleKey).NotEmpty().WithMessage(_localizer["Role key is empty"]);
        }
    }
}
