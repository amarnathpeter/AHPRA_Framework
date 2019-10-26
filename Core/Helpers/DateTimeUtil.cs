using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAutoFramework.Helpers
{
    class DateTimeUtil
    {
        public static string DateTimeConversion(string timeZone)
        {
            string dateTime;
            switch (timeZone)
            {                
                case "Pacific Standard Time":
                    dateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, timeZone).ToString();
                    break;
                case "India Standard Time":
                    dateTime=TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, timeZone).ToString();
                    break;
                case "Eastern Standard Time":
                    dateTime=TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, timeZone).ToString();
                    break;
                case "GMT Standard Time":
                    dateTime=TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, timeZone).ToString();
                    break;
                default:
                    dateTime = DateTime.Now.ToString();
                    break;
            }
            return dateTime;
        }

        public static string GetFirstDayOfMonth(string timeZone)
        {
            DateTime dateTime =Convert.ToDateTime(DateTimeConversion(timeZone));
            return new DateTime(dateTime.Year, dateTime.Month, 1).ToString();
        }
        public static string GetLastDayOfMonth(string timeZone)
        {
            DateTime dateTime = Convert.ToDateTime(DateTimeConversion(timeZone));
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1).ToString();
        }
        public static string GetCurrentDay(string timeZone) => Convert.ToDateTime(DateTimeConversion(timeZone)).Day.ToString();
        public static string GetCurrentMonth(string timeZone) => Convert.ToDateTime(DateTimeConversion(timeZone)).Month.ToString();
        public static string GetCurrentYear(string timeZone) => Convert.ToDateTime(DateTimeConversion(timeZone)).Year.ToString();

    }
}
