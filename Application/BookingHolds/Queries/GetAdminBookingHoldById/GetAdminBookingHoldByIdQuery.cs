using MediatR;

namespace Application.AdminBookingHolds.Queries.GetAdminBookingHold
{
    public class GetAdminBookingHoldByIdQuery : IRequest<GetAdminBookingHoldByIdDto>
    {
        public long Id { get; set; }
    }
}
