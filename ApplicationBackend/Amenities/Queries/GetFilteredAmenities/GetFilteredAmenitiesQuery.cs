using Application.Amenities.Queries.GetFilteredAmenities;
using Domain.Amenitys;
using Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Amenities.Queries.GetFilteredSpace
{
    public class GetFilteredAmenitiesQueryHandler : IRequestHandler<GetFilteredAmenitiesQuery, PagedList<FilteredAmenitiesDto>>
    {
        public Task<PagedList<FilteredAmenitiesDto>> Handle(GetFilteredAmenitiesQuery request, CancellationToken cancellationToken)
        {
            return new Amenity().Repository.GetFilteredAsync<FilteredAmenitiesDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
