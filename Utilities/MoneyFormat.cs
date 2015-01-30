using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class MoneyFormat
    {
        private static string[] NUMBER_ARRAY = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
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
            //return data.ToString("0,0", System.Globalization.CultureInfo.InvariantCulture);
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
        //Đọc số hàng chục
        private static string ReadTenLevel(double so, bool daydu)
        {
            string chuoi = "";
            int chuc = (int)Math.Floor(so / 10);
            int donvi = (int)so % 10;
            if (chuc > 1)
            {
                chuoi = " " + NUMBER_ARRAY[chuc] + " mươi";
                if (donvi == 1)
                {
                    chuoi += " mốt";
                }
            }
            else if (chuc == 1)
            {
                chuoi = " mười";
                if (donvi == 1)
                {
                    chuoi += " một";
                }
            }
            else if (daydu && donvi > 0)
            {
                chuoi = " lẻ";
            }
            if (donvi == 5 && chuc >= 1)
            {
                chuoi += " lăm";
            }
            else if (donvi > 1 || (donvi == 1 && chuc == 0))
            {
                chuoi += " " + NUMBER_ARRAY[donvi];
            }
            return chuoi;
        }
        //Đọc block 3 số
        private static string ReadBlock(double so, bool daydu)
        {
            string chuoi = "";
            int tram = (int)Math.Floor(so / 100);
            so = so % 100;
            if (daydu || tram > 0)
            {
                chuoi = " " + NUMBER_ARRAY[tram] + " trăm";
                chuoi += ReadTenLevel(so, true);
            }
            else
            {
                chuoi = ReadTenLevel(so, false);
            }
            return chuoi;
        }
        //Đọc số hàng triệu
        private static string ReadMilionLevel(double so, bool daydu)
        {
            string chuoi = "";
            int trieu = (int)Math.Floor(so / 1000000);
            so = so % 1000000;
            if (trieu > 0)
            {
                chuoi = ReadBlock(trieu, daydu) + " triệu, ";
                daydu = true;
            }
            double nghin = Math.Floor(so / 1000);
            so = so % 1000;
            if (nghin > 0)
            {
                chuoi += ReadBlock(nghin, daydu) + " nghìn, ";
                daydu = true;
            }
            if (so > 0)
            {
                chuoi += ReadBlock(so, daydu);
            }
            return chuoi;
        }
        public static decimal Round(decimal data)
        {
            return Math.Round(data, 0);
        }
        //Đọc số
        public static string ReadNumber(double so)
        {
            if (so == 0) return NUMBER_ARRAY[0];
            string chuoi = "", hauto = "";
            do
            {
                double ty = so % 1000000000;
                so = Math.Floor(so / 1000000000);
                if (so > 0)
                {
                    chuoi = ReadMilionLevel(ty, true) + hauto + chuoi;
                }
                else
                {
                    chuoi = ReadMilionLevel(ty, false) + hauto + chuoi;
                }
                hauto = " tỷ, ";
            } while (so > 0);
            try
            {
                if (chuoi.Trim().Substring(chuoi.Trim().Length - 1, 1) == ",")
                { chuoi = chuoi.Trim().Substring(0, chuoi.Trim().Length - 1); }
            }
            catch { }            
            return chuoi.Trim() + " đồng";
        }
    }
}
