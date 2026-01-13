using Domain.Common;
using MediatR;

namespace Application.CancellationPolicys.Queries.GetFilteredCancellationPolicys
{
    public class GetFilteredCancellationPolicysQuery : IRequest<PagedList<FilteredCancellationPolicysDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
