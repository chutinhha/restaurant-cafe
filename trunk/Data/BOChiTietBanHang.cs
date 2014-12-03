using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChiTietBanHang
    {
        //public bool IsDeleted { get; set; }    
        public bool IsChanged { get; set; }
        public CHITIETBANHANG ChiTietBanHang { get; set; }
        public MENUKICHTHUOCMON MenuKichThuocMon { get; set; }
        public MENUMON MenuMon { get; set; }
        public int SoLuongBanTam { get; set; }
        private Transit mTransit;
        public static IQueryable<BOChiTietBanHang> Query(int banHangId, BOBanHang banhang)
        {
            var iQuery =
                from chitiet in banhang.frChiTietBanHang.Query()
                join kichthuoc in banhang.frMenuKichThuocMon.Query() on chitiet.KichThuocMonID equals kichthuoc.KichThuocMonID
                join menu in banhang.frMenuMon.Query() on kichthuoc.MonID equals menu.MonID
                where chitiet.BanHangID == banHangId
                select new BOChiTietBanHang
                {
                    MenuKichThuocMon = kichthuoc,
                    ChiTietBanHang = chitiet,
                    MenuMon = menu
                };
            return iQuery;
        }
        public static int Xoa(int chiTietBanHangId, Transit mTransit)
        {
            CHITIETBANHANG item = (from x in mTransit.KaraokeEntities.CHITIETBANHANGs where x.ChiTietBanHangID == chiTietBanHangId select x).First();
            mTransit.KaraokeEntities.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.ChiTietBanHangID;
        }
        public static int CapNhat(CHITIETBANHANG item, Transit mTransit)
        {
            CHITIETBANHANG m = (from x in mTransit.KaraokeEntities.CHITIETBANHANGs where x.ChiTietBanHangID == item.ChiTietBanHangID select x).First();
            m.BanHangID = item.BanHangID;            
            m.SoLuongBan = item.SoLuongBan;
            m.GiaBan = item.GiaBan;
            m.ThanhTien = item.ThanhTien;
            m.KichThuocMonID = item.KichThuocMonID;
            m.NhanVienID = item.NhanVienID;
            mTransit.KaraokeEntities.SaveChanges();
            return item.ChiTietBanHangID;
        }
        public BOChiTietBanHang()
        {
        }
        public BOChiTietBanHang(CHITIETBANHANG chiTiet, Transit transit)
        {
            ChiTietBanHang = chiTiet;
            MenuKichThuocMon = ChiTietBanHang.MENUKICHTHUOCMON;
            MenuMon = ChiTietBanHang.MENUKICHTHUOCMON.MENUMON;
            mTransit = transit;
            SoLuongBanTam = (int)ChiTietBanHang.SoLuongBan;
        }
        public BOChiTietBanHang(Data.BOMenuKichThuocMon ktm, Transit transit)
        {
            mTransit = transit;

            this.ChiTietBanHang = new CHITIETBANHANG();
            this.ChiTietBanHang.SoLuongBan = ktm.MenuKichThuocMon.SoLuongBanBan;
            this.ChiTietBanHang.GiaBan = ktm.MenuKichThuocMon.GiaBanMacDinh;
            this.ChiTietBanHang.ThanhTien = this.ChiTietBanHang.SoLuongBan * this.ChiTietBanHang.GiaBan;
            this.ChiTietBanHang.KichThuocMonID = ktm.MenuKichThuocMon.KichThuocMonID;
            this.MenuKichThuocMon = ktm.MenuKichThuocMon;
            this.MenuMon = ktm.MenuMon;
            SoLuongBanTam = (int)this.ChiTietBanHang.SoLuongBan;
        }
        public void ChangeQtyChiTietBanHang(int qty)
        {
            this.ChiTietBanHang.SoLuongBan = qty;
            this.ChangeThanhTien();
        }
        public void ChangePriceChiTietBanHang(decimal gia)
        {
            this.ChiTietBanHang.GiaBan = gia;
            this.ChangeThanhTien();
        }
        public void ChangeDiscountChiTietBanHang(int discount)
        {
            this.IsChanged = true;
            this.ChiTietBanHang.GiamGia = discount;
            this.ChangeThanhTien();
        }
        private void ChangeThanhTien()
        {
            decimal thanhtien = this.ChiTietBanHang.SoLuongBan * this.ChiTietBanHang.GiaBan;
            this.ChiTietBanHang.ThanhTien = thanhtien - thanhtien * this.ChiTietBanHang.GiamGia / 100;            
        }
        public void ChangeQtyChiTietLichSuBanHang(CHITIETLICHSUBANHANG chitiet,int qty)
        {
            chitiet.SoLuong = qty;
            chitiet.ThanhTien = chitiet.SoLuong * chitiet.GiaBan;
        }
        
        public string TenMon
        {
            get
            {
                return this.MenuMon.TenDai + " (" + this.MenuKichThuocMon.TenLoaiBan + ")";
                //return this.MENUMON==null?"Mon": this.MENUMON.TenDai + " (" + this.MENUKICHTHUOCMON.TenLoaiBan + ")";
            }
        }
        public string ThanhTien
        {

            get
            {
                return Utilities.MoneyFormat.ConvertToString(ChiTietBanHang.ThanhTien);
            }
        }
        public string SoLuongBan
        {
            get
            {
                return ChiTietBanHang.SoLuongBan.ToString();
            }
        }

        public string TenMonPhu
        {
            get
            {
                if (this.ChiTietBanHang.GiamGia>0)
                {
                    return String.Format("Giảm giá {0}%", this.ChiTietBanHang.GiamGia);
                }
                return "";
                //return "Giảm giá 5%, Tặng món A, khuyến mãi B, giá của khu A, phòng VIP";
            }
        }

        public int HienTenMonPhu
        {
            get
            {
                if (TenMonPhu != "")
                    return 1;
                else
                    return 2;

            }
        }


    }
}
