using Domain.Common;
using MediatR;

namespace Application.Amenities.Queries.GetFilteredAmenities
{
    public class GetFilteredAmenitiesQuery : IRequest<PagedList<FilteredAmenitiesDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
