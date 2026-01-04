using Extensions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace Application.Sliders.Queries.GetFilteredSliders
{
    public class GetFilteredSlidersQueryValidator : AbstractValidator<GetFilteredSlidersQuery>
    {
        private readonly IStringLocalizer _localizer;
        private readonly IConfiguration _configuration;
        public GetFilteredSlidersQueryValidator(IStringLocalizer localizer, IConfiguration configuration)
        {
            _localizer = localizer;
            _configuration = configuration;
            var pageSize = _configuration.GetSection("MaxPageSize").Value.ToInt();
            RuleFor(p => p.PageNumber).GreaterThan(0).WithMessage(_localizer["Page number is not valid"]);
            RuleFor(p => p.PageSize).InclusiveBetween(0, pageSize).WithMessage(_localizer["Page size is not valid"]);
        }
    }
}
