using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Footers.Queries.GetFooter
{
    public class GetFooterQueryValidator : AbstractValidator<GetFooterQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetFooterQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
