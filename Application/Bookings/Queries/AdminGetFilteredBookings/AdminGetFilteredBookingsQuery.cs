using Domain.Common;
using MediatR;

namespace Application.Bookings.Queries.AdminGetFilteredBookings
{
    public class AdminGetFilteredBookingsQuery : IRequest<PagedList<AdminFilteredBookingsDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
