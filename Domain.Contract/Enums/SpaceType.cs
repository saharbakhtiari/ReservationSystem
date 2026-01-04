using System.ComponentModel;

namespace Domain.Contract.Enums
{
    public enum SpaceType
    {
        None = 0,
        [Description("")]
        Desk = 1,
        [Description("")]
        MeetingRoom = 2,
        [Description("")]
        Office = 3,
        [Description("")]
        ConferenceHall = 4
    }
}
