using MediatR;

namespace Application.BookingParticipants.Commands.CreateBookingParticipant
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingParticipantManager)]
    public class CreateBookingParticipantCommand : IRequest<long>
    {
        public long BookingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
