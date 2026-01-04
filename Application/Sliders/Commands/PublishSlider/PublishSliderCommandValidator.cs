using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.EducationCategories.Commands.PublishSlider
{
    public class PublishSliderCommandValidator : AbstractValidator<PublishSliderCommand>
    {
        private readonly IStringLocalizer _localizer;
        public PublishSliderCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).NotEmpty().WithMessage(_localizer["Id is not valid"]);
            //RuleFor(p => p.IsPublished).NotEmpty().WithMessage("IsPublished is not valid");
        }
    }
}
