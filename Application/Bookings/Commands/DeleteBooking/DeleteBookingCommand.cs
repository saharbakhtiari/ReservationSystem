using MediatR;

namespace Application.Bookings.Commands.DeleteBooking;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingManager)]
public class DeleteBookingCommand : IRequest
{
    public long Id { get; set; }
}
