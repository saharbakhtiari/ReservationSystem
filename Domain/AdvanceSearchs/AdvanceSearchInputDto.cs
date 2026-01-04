using Domain.Contract.Enums;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.AdvanceSearchs
{
    public class AdvanceSearchInputDto
    {
        public string Content { get; set; }
        public SearchContentType SearchContentType { get; set; }
        public AdvanceSearchType SearchType { get; set; }
        public bool IsPartSearch { get; set; } = false;
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

        public ColumnSort?[] Sort { get; set; }
    }
    public class ColumnSort
    {

        public string id { get; set; }
        public bool desc { get; set; }

    }
}
