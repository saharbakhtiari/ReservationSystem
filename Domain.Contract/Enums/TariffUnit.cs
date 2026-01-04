using System.ComponentModel;

namespace Domain.Contract.Enums
{
    public enum TariffUnit
    {
        None = 0,
        [Description("ریال")]
        Hourly = 1,   
        Daily = 2,    
        Monthly = 3,  
        HalfDay = 4,  
    }
}
