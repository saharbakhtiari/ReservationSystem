using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Cartable.Commands.UpdateCartable
{
    public class UpdateCartableCommandValidator : AbstractValidator<UpdateCartableCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateCartableCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
            RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);

        }
    }
}
