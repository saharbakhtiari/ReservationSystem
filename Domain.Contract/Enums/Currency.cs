using System.ComponentModel;

namespace Domain.Contract.Enums
{
    public enum Currency
    {
        None = 0,
        [Description("ریال")]

        IRR = 1,    // Iranian Rial
        USD = 2,   // US Dollar
        EUR = 3,   // Euro
        AED = 4,   // UAE Dirham
        GBP = 5,   // British Pound
        JPY = 6,   // Japanese Yen
        CNY = 7,   // Chinese Yuan
        CHF = 8,   // Swiss Franc
        CAD = 9,   // Canadian Dollar
        AUD = 10,   // Australian Dollar
    }
}
