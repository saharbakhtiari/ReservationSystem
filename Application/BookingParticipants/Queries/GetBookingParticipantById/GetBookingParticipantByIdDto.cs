using Domain.Contract.Enums;
using System;

namespace Application.BookingParticipants.Queries.GetBookingParticipant
{
    public class GetBookingParticipantByIdDto
    {
        public long ParticipantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
