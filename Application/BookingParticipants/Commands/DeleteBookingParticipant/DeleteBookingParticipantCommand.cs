using MediatR;

namespace Application.BookingParticipants.Commands.DeleteBookingParticipant;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingManager)]
public class DeleteBookingParticipantCommand : IRequest
{
    public long Id { get; set; }
}
