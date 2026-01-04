using Domain.Common;
using MediatR;

namespace Application.Footers.Queries.GetFilteredFooters
{
    public class GetFilteredFootersQuery : IRequest<PagedList<FilteredFootersDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
