using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOPrintOrderItem
    {
        public int ID { get; set; }
        public int MonID { get; set; }
        public string TenMon { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public int GiamGia { get; set; }
        public decimal ThanhTien { get; set; }
        public int TrangThai { get; set; }
        public List<BOPrintOrderItem> _ListKhuyenMai { get; set; }
        public BOPrintOrderItem()
        {
            _ListKhuyenMai = new List<BOPrintOrderItem>();
        }
    }
}
