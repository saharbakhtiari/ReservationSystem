using Domain.Common;
using MediatR;

namespace Application.Bookings.Queries.GetFilteredBookings
{
    public class GetFilteredBookingsQuery : IRequest<PagedList<FilteredBookingsDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
