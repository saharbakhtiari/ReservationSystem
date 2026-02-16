using Domain.Common;
using MediatR;

namespace Application.BookingHolds.Queries.AdminGetFilteredBookingHolds
{
    public class AdminGetFilteredBookingHoldsQuery : IRequest<PagedList<AdminFilteredBookingHoldsDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
