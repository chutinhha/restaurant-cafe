using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class MoneyFormat
    {
        public static string ConvertToStringFull(double data)
        {
            return String.Format("{0:0,0} đ",data);
        }
        public static string ConvertToString(string data)
        {
            return ConvertToString(ConvertToDouble(data));
        }
        public static string ConvertToString(double data)
        {
            return String.Format("{0:0,0}", data);
        }
        public static double ConvertToDouble(string data)
        {
            try
            {                
                return Double.Parse(data);
            }
            catch (Exception){}
            return 0;
        }
    }
}
