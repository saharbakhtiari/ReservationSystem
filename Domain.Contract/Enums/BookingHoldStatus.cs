namespace Domain.Contract.Enums
{
    public enum BookingHoldStatus
    {
        Unknown = 0,
        Requested = 1,
        PendngPayment = 2,
        Completed = 3,
        Expired = 4,
        Cancelled = 5,
        FailedPayment = 6,
    }
}