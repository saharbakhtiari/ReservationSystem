using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Headers.Queries.GetHeader
{
    public class GetHeaderQueryValidator : AbstractValidator<GetHeaderQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetHeaderQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
