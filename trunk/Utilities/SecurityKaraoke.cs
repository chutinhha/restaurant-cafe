using System;
using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    public class SecurityKaraoke
    {
        public static string GetMd5Hash(string input, string hash)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, input, hash);
            }
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