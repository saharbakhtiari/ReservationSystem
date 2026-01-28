using Application.Bookings.Queries.GetFilteredBookings;
using Domain.Common;
using Domain.Bookings;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Bookings.Queries.GetFilteredSpace
{
    public class GetFilteredBookingsQueryHandler : IRequestHandler<GetFilteredBookingsQuery, PagedList<FilteredBookingsDto>>
    {
        public Task<PagedList<FilteredBookingsDto>> Handle(GetFilteredBookingsQuery request, CancellationToken cancellationToken)
        {
            return new Booking().Repository.GetFilteredAsync<FilteredBookingsDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, false, cancellationToken);
        }
    }
}
