using Application.Headers.Queries.GetFilteredHeaders;
using Domain.Common;
using Domain.Headers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Headers.Queries.GetFilteredHeader
{
    public class GetFilteredHeadersQueryHandler : IRequestHandler<GetFilteredHeadersQuery, PagedList<FilteredHeadersDto>>
    {
        public Task<PagedList<FilteredHeadersDto>> Handle(GetFilteredHeadersQuery request, CancellationToken cancellationToken)
        {
            return new Header().Repository.GetFilteredHeadersAsync<FilteredHeadersDto>( request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
