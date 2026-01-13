using Domain.Common;
using MediatR;

namespace Application.Tariffs.Queries.GetFilteredTariffs
{
    public class GetFilteredTariffsQuery : IRequest<PagedList<FilteredTariffsDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
