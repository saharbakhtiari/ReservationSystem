using System.ComponentModel;

namespace Domain.Contract.Enums
{
    public enum LinkType
    {
        None = 0,
        [Description("اثرگذار")]
        Affect = 1,
        [Description("اثرپذیر")]
        Affected = 2,
        [Description("ارجاع")]
        Reference = 3,
        [Description("مرتبط")]
        Related = 4
    }
}
