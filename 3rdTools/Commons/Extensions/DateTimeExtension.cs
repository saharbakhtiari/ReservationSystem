using System;
using System.Globalization;

namespace Extensions
{
    public static class DateTimeExtension
    {
        public static string ToPersianDate(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            string pdate = $"{pc.GetYear(date).ToString().PadLeft(4, '0')}/{pc.GetMonth(date).ToString().PadLeft(2, '0')}/{pc.GetDayOfMonth(date).ToString().PadLeft(2, '0')}";
            return pdate;
        }

        public static string ToPersianDate(this DateTime? date)
        {
            if(date== null)
                return string.Empty;
            var dt = date.GetValueOrDefault();
            PersianCalendar pc = new PersianCalendar();
            string pdate = $"{pc.GetYear(dt).ToString().PadLeft(4, '0')}/{pc.GetMonth(dt).ToString().PadLeft(2, '0')}/{pc.GetDayOfMonth(dt).ToString().PadLeft(2, '0')}";
            return pdate;
        }
    }
}
