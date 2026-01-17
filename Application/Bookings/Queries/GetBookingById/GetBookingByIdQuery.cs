using MediatR;

namespace Application.Bookings.Queries.GetBooking
{
    public class GetBookingByIdQuery : IRequest<GetBookingByIdDto>
    {
        public long Id { get; set; }
    }
}
