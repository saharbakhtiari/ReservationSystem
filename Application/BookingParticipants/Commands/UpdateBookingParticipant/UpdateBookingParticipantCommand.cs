using Domain.Contract.Enums;
using MediatR;
using System;

namespace Application.BookingParticipants.Commands.UpdateBookingParticipant
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingParticipantManager)]
    public class UpdateBookingParticipantCommand : IRequest
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
    }

}
