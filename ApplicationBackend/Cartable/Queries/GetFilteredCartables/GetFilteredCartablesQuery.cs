using Application.Cartable.Queries.GetFilteredCartables;
using Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Cartable.Queries.GetFilteredCartables
{
    public class GetFilteredCartablesQueryHandler : IRequestHandler<GetFilteredCartablesQuery, PagedList<FilteredCartablesDto>>
    {
        public Task<PagedList<FilteredCartablesDto>> Handle(GetFilteredCartablesQuery request, CancellationToken cancellationToken)
        {
            return new Domain.Cartables.Cartable().Repository.GetFilteredCartablesAsync<FilteredCartablesDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
