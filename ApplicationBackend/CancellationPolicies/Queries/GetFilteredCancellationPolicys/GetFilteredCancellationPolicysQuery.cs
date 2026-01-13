using Application.CancellationPolicys.Queries.GetFilteredCancellationPolicys;
using Domain.Common;
using Domain.CancellationPolicys;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.CancellationPolicys.Queries.GetFilteredSpace
{
    public class GetFilteredCancellationPolicysQueryHandler : IRequestHandler<GetFilteredCancellationPolicysQuery, PagedList<FilteredCancellationPolicysDto>>
    {
        public Task<PagedList<FilteredCancellationPolicysDto>> Handle(GetFilteredCancellationPolicysQuery request, CancellationToken cancellationToken)
        {
            return new CancellationPolicy().Repository.GetFilteredAsync<FilteredCancellationPolicysDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
