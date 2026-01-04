using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Sliders.Commands.UpdateSlider
{
    public class UpdateSliderCommandValidator : AbstractValidator<UpdateSliderCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateSliderCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
            RuleFor(p => p.Image).NotEmpty().WithMessage(_localizer["Image is empty"]);
        }
    }
}
