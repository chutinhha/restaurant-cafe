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
        public string TenThe { get; set; }
        public KHACHHANG KhachHang { get; set; }
        public DateTime NgayBan { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public BANHANG BanHang { get; set; }
        public BOPrintOrder()
        { }
        public decimal TienPhiDichVu
        {
            get
            {
                return BanHang.PhiDichVu * BanHang.TongTien / 100;
            }
        }
        public decimal TienGiam
        {
            get
            {
                return BanHang.GiamGia * BanHang.TongTien / 100;
            }
        }
        public decimal TienThueVAT
        {
            get
            {
                return BanHang.ThueVAT * (BanHang.TongTien - TienGiam + TienPhiDichVu) / 100;
            }
        }
        public decimal TongTienPhaiTra
        {
            get
            {
                return BanHang.TongTien - TienGiam + TienPhiDichVu + TienThueVAT;
            }
        }
    }
}
