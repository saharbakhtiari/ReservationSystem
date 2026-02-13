using Domain.Common;
using MediatR;

namespace Application.BookingParticipants.Queries.AdminGetFilteredBookingParticipants
{
    public class AdminGetFilteredBookingParticipantsQuery : IRequest<PagedList<AdminFilteredBookingParticipantsDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long BookingId { get; set; }
    }
}
