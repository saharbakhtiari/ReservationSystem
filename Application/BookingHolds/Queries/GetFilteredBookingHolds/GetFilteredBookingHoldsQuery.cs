using Domain.Common;
using MediatR;

namespace Application.BookingHolds.Queries.GetFilteredBookingHolds
{
    public class GetFilteredBookingHoldsQuery : IRequest<PagedList<FilteredBookingHoldsDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
