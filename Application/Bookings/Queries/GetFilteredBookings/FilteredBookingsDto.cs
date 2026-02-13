using System;
using System.Collections.Generic;

namespace Application.Bookings.Queries.GetFilteredBookings
{
    public class FilteredBookingsDto
    {
        public long Id { get; set; }
        public string ProfileUserName { get; set; }
        public List<FilteredBookingTimeSlotDto> TimeSlots { get; set; }
    }
    public class FilteredBookingTimeSlotDto
    {
        public long Id { get; set; }
        public string SpaceTitle { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
        public int Count { get; set; }
    }
}
