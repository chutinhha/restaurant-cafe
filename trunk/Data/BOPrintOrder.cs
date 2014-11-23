using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOPrintOrder
    {
        public string TenNhanVien { get; set; }
        public string MaHoaDon { get; set; }
        public string TenBan { get; set; }
        public DateTime NgayBan { get; set; }
        public BANHANG BanHang { get; set; }
        public BOPrintOrder()
        { }        
    }
}
