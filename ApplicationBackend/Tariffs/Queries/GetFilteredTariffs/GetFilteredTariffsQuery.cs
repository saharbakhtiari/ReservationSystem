using Application.Tariffs.Queries.GetFilteredTariffs;
using Domain.Common;
using Domain.Tariffs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Tariffs.Queries.GetFilteredSpace
{
    public class GetFilteredTariffsQueryHandler : IRequestHandler<GetFilteredTariffsQuery, PagedList<FilteredTariffsDto>>
    {
        public Task<PagedList<FilteredTariffsDto>> Handle(GetFilteredTariffsQuery request, CancellationToken cancellationToken)
        {
            return new Tariff().Repository.GetFilteredAsync<FilteredTariffsDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
