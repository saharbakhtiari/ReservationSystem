using MediatR;

namespace Application.AdminBookingHolds.Queries.GetAdminBookingHold
{
    public class AdminGetBookingHoldByIdQuery : IRequest<AdminGetBookingHoldByIdDto>
    {
        public long Id { get; set; }
    }
}
