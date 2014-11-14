using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChiTietBanHang
    {
        public bool IsDeleted { get; set; }        
        public CHITIETBANHANG CHITIETBANHANG { get; set; }
        public MENUKICHTHUOCMON MENUKICHTHUOCMON { get; set; }
        public MENUMON MENUMON { get; set; }
        public int SoLuongBanTam { get; set; }
        public bool XoaMon { get; set; }
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
                    MENUKICHTHUOCMON=kichthuoc,
                    CHITIETBANHANG=chitiet,
                    MENUMON=menu                    
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
            m.TonKhoID = item.TonKhoID;
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
        public BOChiTietBanHang(CHITIETBANHANG chiTiet,Transit transit)
        {
            CHITIETBANHANG = chiTiet;
            MENUKICHTHUOCMON = CHITIETBANHANG.MENUKICHTHUOCMON;
            MENUMON = CHITIETBANHANG.MENUKICHTHUOCMON.MENUMON;
            mTransit = transit;
            SoLuongBanTam = (int)CHITIETBANHANG.SoLuongBan;            
        }
        public BOChiTietBanHang(MENUKICHTHUOCMON ktm,Transit transit)
        {
            mTransit = transit;
            this.CHITIETBANHANG = new CHITIETBANHANG();
            this.CHITIETBANHANG.SoLuongBan = ktm.SoLuongBanBan;
            this.CHITIETBANHANG.GiaBan = ktm.GiaBanMacDinh;
            this.CHITIETBANHANG.ThanhTien=this.CHITIETBANHANG.SoLuongBan*this.CHITIETBANHANG.GiaBan;
            //this.CHITIETBANHANG.MENUKICHTHUOCMON = ktm;
            //this.CHITIETBANHANG.NHANVIEN = mTransit.NhanVien;
            this.CHITIETBANHANG.KichThuocMonID = 2;            
            this.MENUKICHTHUOCMON = ktm;
            this.MENUMON = ktm.MENUMON;

            SoLuongBanTam = (int)this.CHITIETBANHANG.SoLuongBan;
        }
        public void ChangeQty(int qty)
        {
            this.CHITIETBANHANG.SoLuongBan = qty;
            this.CHITIETBANHANG.ThanhTien = this.CHITIETBANHANG.SoLuongBan * this.CHITIETBANHANG.GiaBan;
        }
        public string TenMon
        {
            get
            {
                //return this.MENUMON.TenDai + " (" + this.MENUKICHTHUOCMON.TenLoaiBan + ")";
                return this.MENUMON==null?"Mon": this.MENUMON.TenDai + " (" + this.MENUKICHTHUOCMON.TenLoaiBan + ")";
            }
        }
        public string ThanhTien
        {

            get
            {
                return (CHITIETBANHANG.GiaBan * CHITIETBANHANG.SoLuongBan).ToString();
            }
        }
        public string SoLuongBan 
        {
            get
            {
                return CHITIETBANHANG.SoLuongBan.ToString();
            }
        }
    }
}
