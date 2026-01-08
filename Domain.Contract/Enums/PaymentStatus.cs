namespace Domain.Contract.Enums
{
    public enum PaymentStatus
    {
        Unknown = 0,
        Initiated = 1,
        Redirected = 3,
        Paid = 4,
        Failed = 5,
        Refundes = 6
    }
}