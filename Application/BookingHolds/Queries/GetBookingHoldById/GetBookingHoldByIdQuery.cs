using MediatR;

namespace Application.BookingHolds.Queries.GetBookingHold
{
    public class GetBookingHoldByIdQuery : IRequest<GetBookingHoldByIdDto>
    {
        public long Id { get; set; }
    }
}
