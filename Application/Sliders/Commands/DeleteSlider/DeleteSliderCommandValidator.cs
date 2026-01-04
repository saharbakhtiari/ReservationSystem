using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Sliders.Commands.DeleteSlider
{
    public class DeleteSliderCommandValidator : AbstractValidator<DeleteSliderCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteSliderCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
