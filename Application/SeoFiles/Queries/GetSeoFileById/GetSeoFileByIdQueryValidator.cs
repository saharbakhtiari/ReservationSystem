using Application_Backend.SeoFiles.Queries.GetSeoFileById;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.SeoFiles.Queries.GetSeoFileById
{
    public class GetSeoFileByIdQueryValidator : AbstractValidator<GetSeoFileByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetSeoFileByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
