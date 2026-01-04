using Domain.Common;
using MediatR;

namespace Application.Headers.Queries.GetFilteredHeaders
{
    public class GetFilteredHeadersQuery : IRequest<PagedList<FilteredHeadersDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
