using Domain.Contract.Enums;
using Domain.Spaces;
using Domain.Tariffs;
using System;

namespace Application.AdminBookingHolds.Queries.GetAdminBookingHold
{
    public class GetAdminBookingHoldByIdDto
    {
        public long Id { get; set; }
        public GetAdminBookingHoldByIdSpaceDto Space { get; set; }
        public GetAdminBookingHoldByIdProfileDto Profile { get; set; }
        public GetAdminBookingHoldByIdTimeSlotDto TimeSlot { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public BookingHoldStatus Status { get; set; }
    }
    public class GetAdminBookingHoldByIdSpaceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public SpaceType Type { get; set; }
    }
    public class GetAdminBookingHoldByIdTariffDto
    {
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
    }
    public class GetAdminBookingHoldByIdTimeSlotDto
    {
        public long Id { get; set; }
        public GetAdminBookingHoldByIdSpaceDto Space { get; set; }
        public GetAdminBookingHoldByIdTariffDto Tariff { get; set; } = null!;
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
        public bool IsBooked { get; set; }
        public bool IsHeld { get; set; }
        public TimeSlotType Type { get; set; }
    }
    public class GetAdminBookingHoldByIdProfileDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
