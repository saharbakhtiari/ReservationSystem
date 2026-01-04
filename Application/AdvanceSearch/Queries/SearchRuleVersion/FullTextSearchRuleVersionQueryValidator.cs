using Extensions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace Application.AdvanceSearch.Queries.SearchRuleVersion
{
    public class FullTextSearchRuleVersionQueryValidator : AbstractValidator<FullTextSearchRuleVersionQuery>
    {
        private readonly IStringLocalizer _localizer;
        private readonly IConfiguration _configuration;
        public FullTextSearchRuleVersionQueryValidator(IStringLocalizer localizer, IConfiguration configuration)
        {
            _localizer = localizer; 
            _configuration = configuration;
            var pageSize = _configuration.GetSection("GridMaxPageSize").Value.ToInt();
            RuleFor(p => p.PageNumber).GreaterThan(-1).WithMessage(_localizer["Page number is not valid"]);
             RuleFor(p => p.PageSize).InclusiveBetween(0, pageSize).WithMessage(_localizer["Page size is not valid"]);

        }


    }
}
