using System;
using System.Security.Cryptography;
using System.Text;
using System.Management;
using System.Collections.Generic;

namespace Utilities
{
    public class SecurityKaraoke
    {
        public static string HashMD5 = "KTr";
        public static String getMotherDeviceID()
        {
            string cpuInfo = string.Empty;
            try
            {
                
                ManagementClass mc = new ManagementClass("win32_processor");
                ManagementObjectCollection moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    if (cpuInfo == "")
                    {
                        //Get only the first CPU's ID
                        cpuInfo = mo.Properties["processorID"].Value.ToString();
                        break;
                    }
                }
            }
            catch (Exception)
            {
                cpuInfo = "TranMinhTien";
            }
            
            return cpuInfo;
        }
        public static bool CheckIsFirst(string key)
        {
            return key == "Tran Thu Khoa";
        }
        public static bool CheckProductID(string productID, string hash)
        {
            if (productID==null|| productID.Length<15)
            {
                return false;
            }
            if (!Char.IsDigit(productID[0]))
            {
                return false;
            }
            int version = Convert.ToInt16(productID[0]+"");
            string machineID = productID.Substring(1, 5);
            string function = productID.Substring(6, 5);
            string productIDCheck = GetProductID(version, machineID,function, hash);
            return productIDCheck == productID;
        }
        public static bool CheckKey(string key)
        {
            if (key == null || key.Length < 2)
            {
                return false;
            }
            if (!Char.IsDigit(key[0]))
            {
                return false;
            }
            if (!Char.IsDigit(key[1]))
            {
                return false;
            }
            return true;
        }
        public static bool CheckLisence(string key, string hash)
        {
            if (!CheckKey(key))
            {
                return false;
            }
            int keyType = Convert.ToInt16(key[0]+"");
            int version = Convert.ToInt16(key[1]+"");
            string productID = GetProductID(version, hash);
            string keyID = GetKey(keyType,productID, hash);
            return key == keyID;
        }
        public static bool CheckDate(DateTime date, string dateCheck, string hash)
        {
            string str = GetHashDay(date, hash);
            return str == dateCheck;
        }
        public static string GetKey(int keyType,string productID, string hash)
        {            
            string key=""+keyType+productID[0]+SecurityKaraoke.GetMd5Hash(keyType+productID, hash);
            if (key.Length>15)
            {
                key = key.Substring(0, 15);
            }
            return key.ToUpper();            
        }        
        public static string GetProductID(int version, string machine,string functionID, string hash)
        {            
            string id= SecurityKaraoke.GetMd5Hash(version + machine+functionID, hash);
            if (id.Length > 5)
            {
                id = id.Substring(0, 5);
            }
            return String.Format("{0}{1}{2}{3}",version,machine,functionID,id.ToUpper());
        }
        public static string GetProductID(int version, string hash)
        {
            if (version>9)
            {
                version = 0;
            }
            string machine = GetMachineID(hash);
            string function = GetFunctionID();
            return GetProductID(version, machine,function, hash);
        }
        public static string GetMachineID(string hash)
        {
            string device = getMotherDeviceID();            
            string machineID= SecurityKaraoke.GetMd5Hash(device, hash);
            if (machineID.Length>5)
            {
                machineID = machineID.Substring(0, 5);
            }
            return machineID.ToUpper();            
        }
        private static string GetFunctionID()
        {
            return "AAAAA";
        }
        public static string GetMd5Hash(string input, string hash)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, input, hash);
            }
        }
        public static string GetHashDay(DateTime dt, string hash)
        {
            string str = DateTimeConverter.ConvertDateTimeToString(dt);
            return GetMd5Hash(str, hash);
        }
        private static string GetMd5Hash(MD5 md5Hash, string input, string hash)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }    
    }
}