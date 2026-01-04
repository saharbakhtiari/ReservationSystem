using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Cartable.Commands.CreateCartable
{
    public class CreateCartableCommandValidator : AbstractValidator<CreateCartableCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateCartableCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
        }
    }
}
