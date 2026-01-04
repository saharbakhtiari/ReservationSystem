using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Cartable.Queries.GetMyCartables
{
    public class GetMyCartableQueryValidator : AbstractValidator<GetMyCartablesQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetMyCartableQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.PageNumber).GreaterThan(0).WithMessage(_localizer["Page number is not valid"]);
            RuleFor(p => p.PageSize).GreaterThan(0).WithMessage(_localizer["Page size is not valid"]);
        }
    }
}
