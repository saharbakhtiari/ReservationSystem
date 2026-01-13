using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.CancellationPolicys.Queries.GetCancellationPolicy
{
    public class GetCancellationPolicyByIdQueryValidator : AbstractValidator<GetCancellationPolicyByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetCancellationPolicyByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
