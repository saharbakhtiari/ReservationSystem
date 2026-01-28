using MediatR;

namespace Application.BookingHolds.Commands.DeleteBookingHold;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingHoldManager)]
public class DeleteBookingHoldCommand : IRequest
{
    public long Id { get; set; }
}
