using Application.BookingParticipants.Queries.GetBookingParticipant;
using Domain.Contract.Enums;
using System;

namespace Application.BookingParticipants.Queries.AdminGetFilteredBookingParticipants
{
    public class AdminFilteredBookingParticipantsDto
    {
        public long ParticipantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
