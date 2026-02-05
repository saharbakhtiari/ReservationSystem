using MediatR;

namespace Application.Bookings.Commands.CreateBooking
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingManager)]
    public class CreateBookingCommand : IRequest<long>
    {
        public long TimeSlotId { get; set; }
    }
}
