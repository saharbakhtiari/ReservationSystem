using System;
using System.Globalization;

namespace Domain.Common.Utilities
{
    public static class PersianCalenderHelper
    {
        public static string GetPersianDate()
        {
            PersianCalendar pc = new PersianCalendar();
            string pdate = pc.GetYear(DateTime.Now).ToString().PadLeft(4, '0') + pc.GetMonth(DateTime.Now).ToString().PadLeft(2, '0')
                + pc.GetDayOfMonth(DateTime.Now).ToString().PadLeft(2, '0');
            return pdate;
        }

        public static string GetTime()
        {
            return DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        }
        public static int GetYesterdayIntegerDate()
        {
            PersianCalendar jc = new PersianCalendar();
            var year = jc.GetYear(DateTime.Now.AddDays(-1));
            var month = jc.GetMonth(DateTime.Now.AddDays(-1));
            var day = jc.GetDayOfMonth(DateTime.Now.AddDays(-1));
            var date = (year * 100 + month) * 100 + day;
            return date;
        }
        public static int GetFivedayLaterIntegerDate()
        {
            PersianCalendar jc = new PersianCalendar();
            var year = jc.GetYear(DateTime.Now.AddDays(-5));
            var month = jc.GetMonth(DateTime.Now.AddDays(-5));
            var day = jc.GetDayOfMonth(DateTime.Now.AddDays(-5));
            var date = (year * 100 + month) * 100 + day;
            return date;
        }
    }
}
