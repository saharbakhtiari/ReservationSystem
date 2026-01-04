using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Cartable.Commands.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IStringLocalizer _localizer;
        public AddUserCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.CartableId).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
            RuleFor(p => p.UserId).NotEmpty().WithMessage(_localizer["UserId is empty"]);

        }
    }
}
