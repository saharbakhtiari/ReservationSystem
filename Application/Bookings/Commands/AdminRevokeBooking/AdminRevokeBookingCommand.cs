using MediatR;

namespace Application.Bookings.Commands.AdminRevokeBooking;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingManager)]
public class AdminRevokeBookingCommand : IRequest
{
    public long Id { get; set; }
}
