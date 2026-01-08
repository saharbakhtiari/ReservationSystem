using Domain.Common;
using MediatR;

namespace Application.Spaces.Queries.GetFilteredSpaces
{
    public class GetFilteredSpacesQuery : IRequest<PagedList<FilteredSpacesDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
