using MediatR;

namespace Application.Bookings.Queries.AdminGetBooking
{
    public class AdminGetBookingByIdQuery : IRequest<AdminGetBookingByIdDto>
    {
        public long Id { get; set; }
    }
}
