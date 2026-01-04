using Domain.Contract.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.AdvanceSearch.Queries.SearchRuleVersion
{
    public class SearchRuleVersionQuery : IRequest<List<SearchRuleVersionDto>>
    {
        public AdvanceSearchType SearchType { get; set; } = AdvanceSearchType.Normal;
        public string Content { get; set; }
        public SearchContentType SearchContentType { get; set; }
        public bool IsPartSerarch { get; set; } = false;
        public long? CategoryId { get; set; }
        public long? ApprovalInstitutionId { get; set; }
        public long? TanghihStatusId { get; set; }
        public long? RuleTypeId { get; set; }
        public long?[] TagIds { get; set; }
        public DateTime? FromApprovalDate { get; set; }
        public DateTime? ToApprovalDate { get; set; }
        public bool IsExactWord { get; set; }

    }
}
