using MediatR;

namespace Application.Bookings.Commands.RevokeBooking;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingManager)]
public class RevokeBookingCommand : IRequest
{
    public long Id { get; set; }
}
