using Domain.Contract.Enums;
using System;

namespace Application.TimeSlots.Queries.GetFilteredTimeSlots
{
    public class FilteredTimeSlotsDto
    {
        public long Id { get; set; }
        public string SpaceTitle { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
        public bool IsBooked { get; set; }
        public bool IsHeld { get; set; }
        public TimeSlotType Type { get; set; }
    }
}
