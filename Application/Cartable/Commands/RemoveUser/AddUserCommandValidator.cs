using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Cartable.Commands.RemoveUser
{
    public class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
    {
        private readonly IStringLocalizer _localizer;
        public RemoveUserCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.CartableId).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
            RuleFor(p => p.UserId).NotEmpty().WithMessage(_localizer["UserId is empty"]);

        }
    }
}
