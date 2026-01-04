using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Sliders.Commands.CreateSlider
{
    public class CreateSliderCommandValidator : AbstractValidator<CreateSliderCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateSliderCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Image).NotEmpty().WithMessage(_localizer["Image is empty"]);
        }
    }
}
