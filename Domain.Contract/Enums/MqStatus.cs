using System.ComponentModel;

namespace Domain.Contract.Enums
{
    public enum MqStatus
    {
        [Description("Mq Started")]
        Started = 1,
        [Description("Mq Stopped")]
        Stopped = 2
    }
}
