using MediatR;

namespace Application.BookingParticipants.Queries.GetBookingParticipant
{
    public class GetBookingParticipantByIdQuery : IRequest<GetBookingParticipantByIdDto>
    {
        public long Id { get; set; }
    }
}
