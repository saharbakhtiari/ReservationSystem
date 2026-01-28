using Application.BookingHolds.Queries.GetBookingHold;
using Domain.Contract.Enums;
using System;

namespace Application.BookingHolds.Queries.GetFilteredBookingHolds
{
    public class FilteredBookingHoldsDto
    {
        public long Id { get; set; }
        public string SpaceTitle { get; set; }
        public string ProfileUserName { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public BookingHoldStatus Status { get; set; }
    }
}
