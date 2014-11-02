using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class POSBanHang : BANHANG
    {
        public POSChiTietBanHang ChiTietBanHangs { get; set; }
        public POSBanHang()
        {
            ChiTietBanHangs = new POSChiTietBanHang();
        }
    }

    public class POSChiTietBanHang : CHITIETBANHANG
    {
        public string TenMon
        {
            get
            {
                return MENUKICHTHUOCMON.MENUMON.TenDai + " (" + MENUKICHTHUOCMON.TenLoaiBan + ")";
            }
        }

        public string ThanhTien
        {

            get
            {
                return (GiaBan * SoLuongBan).ToString();
            }
        }
    }
}
