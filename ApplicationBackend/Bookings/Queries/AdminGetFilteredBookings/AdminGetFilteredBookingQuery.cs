using Application.Bookings.Queries.AdminGetFilteredBookings;
using Domain.Common;
using Domain.Bookings;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Bookings.Queries.GetFilteredSpace
{
    public class AdminGetFilteredBookingsQueryHandler : IRequestHandler<AdminGetFilteredBookingsQuery, PagedList<AdminFilteredBookingsDto>>
    {
        public Task<PagedList<AdminFilteredBookingsDto>> Handle(AdminGetFilteredBookingsQuery request, CancellationToken cancellationToken)
        {
            return new Booking().Repository.GetFilteredAsync<AdminFilteredBookingsDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, true, cancellationToken);
        }
    }
}
