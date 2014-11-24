using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class DateTimeConverter
    {
        public static string ConvertDateTimeToString(DateTime dt)
        {
            return String.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }
        public static DateTime ConvertStringToDateTime(string str)
        {
            return Convert.ToDateTime(str);
            //return DateTime.ParseExact(str, "yyyy-MM-dd hh:MM:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
        public static string ConvertDateTimeToStringDMYH(DateTime dt)
        {
            return String.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}:{5:00}", dt.Day, dt.Month, dt.Year, dt.Hour, dt.Minute, dt.Second);
        }
        public static DateTime GetDateTimeFrom(DateTime dt, TimeSpan time)
        {
            if (time!=null)
            {
                return new DateTime(dt.Year, dt.Month, dt.Day, time.Hours, time.Minutes, time.Seconds);
            }
            return dt;
        }
    }
}
