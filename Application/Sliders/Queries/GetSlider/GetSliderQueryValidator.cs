using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Sliders.Queries.GetSlider
{
    public class GetSliderQueryValidator : AbstractValidator<GetSliderQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetSliderQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
