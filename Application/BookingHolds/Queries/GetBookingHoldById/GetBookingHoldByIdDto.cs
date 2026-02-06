using Domain.Contract.Enums;
using System;
using System.Collections.Generic;

namespace Application.BookingHolds.Queries.GetBookingHold
{
    public class GetBookingHoldByIdDto
    {
        public long Id { get; set; }
        public GetBookingHoldByIdProfileDto Profile { get; set; }
        public List<GetBookingHoldByIdTimeSlotDto> TimeSlots { get; set; }
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
    public class GetBookingHoldByIdTariffDto
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
    }
    public class GetBookingHoldByIdTimeSlotDto
    {
        public long Id { get; set; }
        public GetBookingHoldByIdSpaceDto Space { get; set; }
        public GetBookingHoldByIdTariffDto Tariff { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
        public TimeSlotType Type { get; set; }
        public int Count { get; set; }
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
