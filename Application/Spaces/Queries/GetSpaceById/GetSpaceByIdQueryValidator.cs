using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Spaces.Queries.GetSpace
{
    public class GetSpaceByIdQueryValidator : AbstractValidator<GetSpaceByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetSpaceByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
