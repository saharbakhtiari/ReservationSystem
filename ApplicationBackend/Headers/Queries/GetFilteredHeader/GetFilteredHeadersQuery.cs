using Application.Footers.Queries.GetFilteredFooters;
using Domain.Common;
using Domain.Footers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Footers.Queries.GetFilteredFooter
{
    public class GetFilteredFootersQueryHandler : IRequestHandler<GetFilteredFootersQuery, PagedList<FilteredFootersDto>>
    {
        public Task<PagedList<FilteredFootersDto>> Handle(GetFilteredFootersQuery request, CancellationToken cancellationToken)
        {
            return new Footer().Repository.GetFilteredFootersAsync<FilteredFootersDto>( request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
