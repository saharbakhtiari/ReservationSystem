using Domain.Common;
using MediatR;

namespace Application.Cartable.Queries.GetFilteredCartables
{
    public class GetFilteredCartablesQuery : IRequest<PagedList<FilteredCartablesDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
