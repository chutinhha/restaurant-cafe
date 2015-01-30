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
        public List<BOChiTietBanHang> _ListKhuyenMai { get; set; }
        public MENUMON MenuMon { get; set; }
        public int SoLuongBanTam { get; set; }
        private Transit mTransit;
        
        public static IQueryable<BOChiTietBanHang> Query(BANHANG banhang, KaraokeEntities kara)
        {
            var iQuery =
                //from chitiet in banhang.CHITIETBANHANGs.Where(o=>o.ChiTietBanHangID_Ref==null)
                from chitiet in kara.CHITIETBANHANGs.Where(o => o.ChiTietBanHangID_Ref == null&&o.BanHangID==banhang.BanHangID)
                join kichthuoc in kara.MENUKICHTHUOCMONs on chitiet.KichThuocMonID equals kichthuoc.KichThuocMonID
                join menu in kara.MENUMONs on kichthuoc.MonID equals menu.MonID                
                select new BOChiTietBanHang
                {
                    MenuKichThuocMon = kichthuoc,
                    ChiTietBanHang = chitiet,
                    MenuMon = menu
                };
            return iQuery;
        }
        public static IQueryable<BOChiTietBanHang> QueryKhuyenMai(CHITIETBANHANG chitietbh, KaraokeEntities kara)
        {
            var iQuery =
                //from chitiet in banhang.CHITIETBANHANGs.Where(o=>o.ChiTietBanHangID_Ref==null)
                from chitiet in kara.CHITIETBANHANGs.Where(o => o.ChiTietBanHangID_Ref==chitietbh.ChiTietBanHangID)
                join kichthuoc in kara.MENUKICHTHUOCMONs on chitiet.KichThuocMonID equals kichthuoc.KichThuocMonID
                join menu in kara.MENUMONs on kichthuoc.MonID equals menu.MonID
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
            _ListKhuyenMai = new List<BOChiTietBanHang>();
        }
        public BOChiTietBanHang(CHITIETBANHANG chiTiet, Transit transit)
        {
            _ListKhuyenMai = new List<BOChiTietBanHang>();
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
            _ListKhuyenMai = new List<BOChiTietBanHang>();
            this.MenuKichThuocMon = ktm.MenuKichThuocMon;
            this.ChiTietBanHang.SoLuongBan = ktm.MenuKichThuocMon.SoLuongBanBan;
            this.ChiTietBanHang.GiaBan = ktm.MenuKichThuocMon.GiaBanMacDinh;            
            this.ChiTietBanHang.KichThuocLoaiBan = ktm.KichThuocLoaiBan;
            this.ChiTietBanHang.GiamGia = ktm.MenuMon.GiamGia;
            this.ChiTietBanHang.KichThuocMonID = ktm.MenuKichThuocMon.KichThuocMonID;            
            this.MenuMon = ktm.MenuMon;
            SoLuongBanTam = (int)this.ChiTietBanHang.SoLuongBan;
            ChangeThanhTien();
        }
        public void LoadKhuyenMai(KaraokeEntities kara)
        {
            var queryKhuyenMai = Data.BOMenuKhuyenMai.GetAllByKichThuocMon(kara, MenuKichThuocMon);
            foreach (var item in queryKhuyenMai)
            {
                BOChiTietBanHang ct = new BOChiTietBanHang(item.KichThuocMonTang, mTransit);
                _ListKhuyenMai.Add(ct);
            }
        }
        public void ChangeQtyChiTietBanHang(int qty)
        {
            this.ChiTietBanHang.SoLuongBan = qty;            
            this.ChangeThanhTien();
            foreach (var km in this._ListKhuyenMai)
            {
                km.ChangeQtyChiTietBanHang(qty);
            }
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
            decimal thanhtien = this.ChiTietBanHang.SoLuongBan * this.ChiTietBanHang.GiaBan*this.ChiTietBanHang.KichThuocLoaiBan/this.MenuKichThuocMon.KichThuocLoaiBan;
            this.ChiTietBanHang.ThanhTien = thanhtien - thanhtien * this.ChiTietBanHang.GiamGia / 100;            
            
        }
        public void ChangeQtyChiTietLichSuBanHang(CHITIETLICHSUBANHANG chitiet,int qty)
        {
            chitiet.SoLuong = qty;
            chitiet.ThanhTien = chitiet.SoLuong *chitiet.KichThuocLoaiBan* chitiet.GiaBan/this.MenuKichThuocMon.KichThuocLoaiBan;
        }
        public string MaVach 
        {
            get { return this.MenuMon.MaVach; }
        }
        public string TenMon
        {
            get
            {
                string tenLoaiBan = "";
                if (MenuKichThuocMon.TenLoaiBan!="")
                {
                    tenLoaiBan = String.Format(" ({0})",MenuKichThuocMon.TenLoaiBan);
                }
                string khoiLuong = "";
                if (MenuKichThuocMon.KichThuocLoaiBan>1)
                {
                    khoiLuong = GetDonVi(MenuKichThuocMon.DonViID.Value, ChiTietBanHang.KichThuocLoaiBan,MenuKichThuocMon.KichThuocLoaiBan);
                }

                //return this.MenuKichThuocMon.TenLoaiBan == "" ?
                //    this.MenuMon.TenDai :
                //    String.Format("{0} ({1})", this.MenuMon.TenDai, this.MenuKichThuocMon.TenLoaiBan);                
                return String.Format("{0}{1}{2}",MenuMon.TenDai,tenLoaiBan,khoiLuong);
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
                string s = "";   
                if (this.ChiTietBanHang.GiamGia>0)
                {
                    s= String.Format("Giảm giá {0}%,", this.ChiTietBanHang.GiamGia);
                }
                foreach (var item in _ListKhuyenMai)
                {
                    s += String.Format("(+{0}){1},", item.ChiTietBanHang.SoLuongBan, item.TenMon);
                }
                if (s.Length>0)
                {
                    s = s.Substring(0, s.Length - 1);
                }
                return s;
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

        private string GetDonVi(int donviID, int soluong, int kichThuocLoaiBan)
        {
            switch (donviID)
            {
                case 2:
                    return String.Format(" #{0:0,000}Kg", (double)soluong / kichThuocLoaiBan);
                case 3:
                    return String.Format(" #{0:0,000}L", (double)soluong / kichThuocLoaiBan);
                case 4:
                    return String.Format(" #{0} giờ {1} phút", soluong / 3600, soluong / 60 % 60);
                default:
                    return "";
            }
        }
    }
}
