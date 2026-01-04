using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Cartable.Queries.GetCartableById
{
    public class GetCartableByIdQueryValidator : AbstractValidator<GetCartableByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetCartableByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
