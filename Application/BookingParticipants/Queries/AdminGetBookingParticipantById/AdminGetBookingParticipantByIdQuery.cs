using MediatR;

namespace Application.BookingParticipants.Queries.AdminGetBookingParticipant
{
    public class AdminGetBookingParticipantByIdQuery : IRequest<AdminGetBookingParticipantByIdDto>
    {
        public long Id { get; set; }
    }
}
