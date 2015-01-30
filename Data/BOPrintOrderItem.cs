using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOPrintOrderItem
    {        
        public int MonID { get; set; }
        public string TenMon
        {
            get
            {
                string tenLoaiBan = "";
                if (TenLoaiBan != "")
                {
                    tenLoaiBan = String.Format(" ({0})", TenLoaiBan);
                }
                string khoiLuong = "";
                if (KichThuocBan > 1)
                {
                    khoiLuong = GetDonVi(DonViID, KichThuocThuc, KichThuocBan);
                }
                return String.Format("{0}{1}{2}", TenDai, tenLoaiBan, khoiLuong);
            }
            
        }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public int GiamGia { get; set; }
        public decimal ThanhTien { get; set; }
        public int TrangThai { get; set; }
        public string TenDai { get; set; }
        public string TenLoaiBan { get; set; }
        public int DonViID { get; set; }
        public int KichThuocThuc { get; set; }
        public int KichThuocBan { get; set; }
        public List<BOPrintOrderItem> _ListKhuyenMai { get; set; }
        public BOPrintOrderItem()
        {
            _ListKhuyenMai = new List<BOPrintOrderItem>();
        }
        private string GetDonVi(int donviID, int soluong, int kichThuocLoaiBan)
        {
            switch (donviID)
            {
                case 2:
                    return String.Format(" {0:0,000}Kg", (double)soluong / kichThuocLoaiBan);
                case 3:
                    return String.Format(" {0:0,000}L", (double)soluong / kichThuocLoaiBan);
                case 4:
                    return String.Format(" {0} giờ {1} phút", soluong / 3600, soluong / 60 % 60);
                default:
                    return "";
            }
        }
    }
}
