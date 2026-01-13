using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Tariffs.Queries.GetTariff
{
    public class GetTariffByIdQueryValidator : AbstractValidator<GetTariffByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetTariffByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
