using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class MoneyFormat
    {
        //public static string ConvertToStringFull(double data)
        //{
        //    return String.Format("{0:0,0} đ",data);
        //}
        public static string ConvertToStringFull(decimal data)
        {
            return String.Format("{0:0,0} đ", data);
        }
        public static string ConvertToString(string data)
        {
            if (data.Length<=3)            
                return data;            
            return ConvertToString(ConvertToDecimal(data));
        }
        public static string ConvertToString(decimal data)
        {
            return String.Format("{0:0,0}", data);
        }
        public static int ConvertToInt(string data)
        {
            if (CheckIsDigit(data))
            {
                return Convert.ToInt32(data);
            }
            return 0;
        }
        public static decimal ConvertToDecimal(string data)
        {
            
            if (CheckIsDigit(data))
            {
                return Convert.ToDecimal(data);
            }
            return 0;
        }
        public static bool CheckIsDigit(string data)
        {
            if (data.Length==0)
            {
                return false;
            }
            foreach (var item in data)
            {
                if ((item<'0' || item>'9')&& item!='.' && item!=',')
                {
                    return false;
                }                
            }
            return true;
        }
    }
}
