using Domain.Contract.Enums;
using System;

namespace Application.TimeSlots.Queries.GetTimeSlot
{
    public class GetTimeSlotByIdDto
    {
        public long Id { get; set; }
        public GetTimeSlotByIdSpaceDto Space { get; set; }
        public GetTimeSlotByIdProfileDto Profile { get; set; }
        public GetTimeSlotByIdTariffDto Tariff { get; set; }

        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
        public bool IsBooked { get; set; }
        public bool IsHeld { get; set; }
        public TimeSlotType Type { get; set; }
    }
    public class GetTimeSlotByIdTariffDto
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
        public string Rules { get; set; }
    }
    public class GetTimeSlotByIdSpaceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public SpaceType Type { get; set; }
    }
    public class GetTimeSlotByIdProfileDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
