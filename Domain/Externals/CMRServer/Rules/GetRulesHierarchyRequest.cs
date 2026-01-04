using CustomLoggers;
using Domain.Contract.Common;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.CMRServer.Rules
{
    public class GetRulesHierarchyRequest : CMRRequest
    {
        public Task<List<RulesHierarchyResponse>> SendAsync(CancellationToken cancellationToken)
        {
            return base.SendGetAsync<List<RulesHierarchyResponse>>(CMRServerApis.GetHierarchy, cancellationToken);
        }
    }
    public class RulesHierarchyResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }
        public List<HierarchyRuleWithoutPartInfo> Rules { get; set; }
        public List<RulesHierarchyResponse> Childs { get; set; }
        public RulesHierarchyResponse()
        {
            Rules = new();
            Childs = new();
        }

        public class HierarchyRuleWithoutPartInfo
        {
            public long Id { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }

        }
    }
}
