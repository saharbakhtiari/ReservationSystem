using System.ComponentModel;

namespace Domain.Contract.Enums
{
    public enum TimeSlotType
    {
        None = 0,
        [Description("")]
        Hourly = 1,
        [Description("")]
        Daily = 2,
    }
}
