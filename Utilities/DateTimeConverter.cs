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
            if (dt==null)
            {
                return "";
            }
            return String.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }
        /// <summary>
        /// day//mon//year
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(string dt)
        {
            try
            {
                string[] s = dt.Split('/');
                if (s.Length==3)
                {
                    return new DateTime(Convert.ToInt32(s[2]),Convert.ToInt32(s[1]),Convert.ToInt32(s[0]));
                }
            }
            catch (Exception)
            {                
            }
            return DateTime.Now;
        }
        public static DateTime ConvertStringToDateTime(string str)
        {
            return Convert.ToDateTime(str);
            //return DateTime.ParseExact(str, "yyyy-MM-dd hh:MM:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
        public static string ConvertToDateString(DateTime dt)
        {
            if (dt == null)
            {
                return "";
            }
            return String.Format("{0:00}/{1:00}/{2:0000}", dt.Day, dt.Month, dt.Year);
        }
        public static string ConvertToTimeString(DateTime dt)
        {
            if (dt == null)
            {
                return "";
            }
            return String.Format("{0:00}:{1:00}:{2:00}", dt.Hour, dt.Minute, dt.Second);
        }
        public static string ConvertDateTimeToStringDMYH(DateTime dt)
        {
            if (dt==null)
            {
                return "";
            }
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
