using Domain.Contract.Enums;
using System;

namespace Application.BookingHolds.Queries.GetBookingHold
{
    public class GetBookingHoldByIdDto
    {
        public long Id { get; set; }
        public GetBookingHoldByIdSpaceDto Space { get; set; } 
        public GetBookingHoldByIdProfileDto Profile { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public BookingHoldStatus Status { get; set; }
    }
    public class GetBookingHoldByIdSpaceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public SpaceType Type { get; set; }
    }
    public class GetBookingHoldByIdProfileDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
