using System;
using System.Collections.Generic;

namespace Application.Bookings.Queries.AdminGetFilteredBookings
{
    public class AdminFilteredBookingsDto
    {
        public long Id { get; set; }
        public string ProfileUserName { get; set; }
        public List<AdminFilteredBookingTimeSlotDto> TimeSlots { get; set; }
    }
    public class AdminFilteredBookingTimeSlotDto
    {
        public long Id { get; set; }
        public string SpaceTitle { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
        public int Count { get; set; }
    }
}
