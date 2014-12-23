using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace License
{
    public class LicenseType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public static List<LicenseType> GetListSecurityType()
        {
            List<LicenseType> resuilt = new List<LicenseType>();
            resuilt.Add(new LicenseType { ID = 1, Name = "Dùng Thử 1" });
            resuilt.Add(new LicenseType { ID = 2, Name = "Dùng Thử 2" });
            resuilt.Add(new LicenseType { ID = 3, Name = "Bản Quyền" });
            return resuilt;
        }
    }
}
