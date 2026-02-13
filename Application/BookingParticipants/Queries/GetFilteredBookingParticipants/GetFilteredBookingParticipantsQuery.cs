using Domain.Common;
using MediatR;

namespace Application.BookingParticipants.Queries.GetFilteredBookingParticipants
{
    public class GetFilteredBookingParticipantsQuery : IRequest<PagedList<FilteredBookingParticipantsDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long BookingId { get; set; }
    }
}
