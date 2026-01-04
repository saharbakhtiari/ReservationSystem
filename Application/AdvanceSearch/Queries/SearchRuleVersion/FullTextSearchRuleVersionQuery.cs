using Domain.Common;
using Domain.Contract.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.AdvanceSearch.Queries.SearchRuleVersion
{
    public class FullTextSearchRuleVersionQuery : IRequest<PagedList<SearchRuleVersionDto>>
    {
        public AdvanceSearchType SearchType { get; set; } = AdvanceSearchType.Normal;
        public string Content { get; set; }
        public SearchContentType SearchContentType { get; set; }
        /// <summary>
        /// جستجو در بند ها ماده ها و تبصره ها
        /// </summary>
        public bool IsPartSearch { get; set; } = true;
        public long? CategoryId { get; set; }
        public long?[] ApprovalInstitutionIds { get; set; }
        public long?[] TanghihStatusIds { get; set; }
        public long?[] RuleTypeIds { get; set; }
        public long?[] TagIds { get; set; }
        public DateTime? FromApprovalDate { get; set; }
        public DateTime? ToApprovalDate { get; set; }
        public bool IsExactWord { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ColumnSortQuery?[] Sort { get; set; }
    }
    public class ColumnSortQuery
    {

        public string id { get; set; }
        public bool desc { get; set; }

    }
}
 