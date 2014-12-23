using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOXuliMayIn
    {
        public CAIDATMAYINBEP _CAIDATMAYINBEP { get; set; }
        public CAIDATMAYINHOADON _CAIDATMAYINHOADON { get; set; }
        public System.Drawing.Image _ImageLogo { get; set; }
        private KaraokeEntities mKaraokeEntities;
        private Transit mTransit;        

        public BOXuliMayIn(Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
            
            _CAIDATMAYINBEP = BOCaiDatMayInBep.GetQueryNoTracking(mKaraokeEntities);            
            _CAIDATMAYINHOADON = BOCaiDatMayInHoaDon.GetQueryNoTracking(mKaraokeEntities);
            _ImageLogo = Utilities.ImageHandler.BitmapImage2Bitmap(this._CAIDATMAYINHOADON.Logo);
        }
        public IQueryable<BOMayIn> AllPrinting(int lichSuBanHang)
        {
            var queryMenuMon = from x in mKaraokeEntities.MENUMONs
                               join y in mKaraokeEntities.MENUKICHTHUOCMONs on x.MonID equals y.MonID
                               join z in mKaraokeEntities.CHITIETLICHSUBANHANGs on y.KichThuocMonID equals z.KichThuocMonID
                               where z.LichSuBanHangID == lichSuBanHang
                               select x;
            var queryMenuMayIn = from x in queryMenuMon
                                 join y in mKaraokeEntities.MENUITEMMAYINs on x.MonID equals y.MonID
                                 where y.Deleted==false && y.Visual==true
                                 select y;
            var resuilt = from x in mKaraokeEntities.MAYINs
                          join y in mKaraokeEntities.MENUITEMMAYINs on x.MayInID equals y.MayInID
                          where x.Visual == true && x.Deleted==false && queryMenuMayIn.Contains(y)
                          select new BOMayIn
                          {                              
                              MayInID=x.MayInID,
                              TenMayIn=x.TenMayIn,
                              TieuDeIn=x.TieuDeIn,
                              HocDungTien=(bool)x.HopDungTien,
                              SoLanIn=(int)x.SoLanIn
                          };            
            return resuilt.Distinct();
        }
        public IQueryable<BOMayIn> AllPrintingBill()
        {
            var resuilt = from a in mKaraokeEntities.MAYINs
                         where a.MayInHoaDon==true
                         select new BOMayIn
                          {
                              MayInID = a.MayInID,
                              TenMayIn = a.TenMayIn,
                              TieuDeIn = a.TieuDeIn,
                              HocDungTien = (bool)a.HopDungTien,
                              SoLanIn = (int)a.SoLanIn
                          };
            return resuilt;
        }
        public IQueryable<BOPrintOrder> GetOrderFromBanHangID(int banHangID)
        {
            var query = from a in mKaraokeEntities.BANHANGs.Where(o => o.BanHangID == banHangID)
                        join b in mKaraokeEntities.NHANVIENs on a.NhanVienID equals b.NhanVienID
                        join c in mKaraokeEntities.BANs on a.BanID equals c.BanID
                        join d in mKaraokeEntities.THEs on a.TheID equals d.TheID into list
                        from e in list.DefaultIfEmpty()
                        join f in mKaraokeEntities.KHACHHANGs on a.KhachHangID equals f.KhachHangID into listKh
                        from g in listKh.DefaultIfEmpty()
                        where a.BanHangID == banHangID
                        select new BOPrintOrder
                        {
                            TenThe=e.TenThe,
                            TenNhanVien = b.TenNhanVien,
                            MaHoaDon = a.MaHoaDon,
                            TenBan = c.TenBan,
                            NgayBan = (DateTime)a.NgayBan,
                            BanHang = a,
                            KhachHang=g
                        };
            return query;
        }
        public IQueryable<BOPrintOrder> GetOrderFromLichSuBanHang(int lichSuBanHangID)
        {
            var query = from x in mKaraokeEntities.LICHSUBANHANGs.Where(o => o.LichSuBanHangID == lichSuBanHangID)
                        //where x.LichSuBanHangID == lichSuBanHangID
                        join y in mKaraokeEntities.BANHANGs on x.BanHangID equals y.BanHangID
                        join z in mKaraokeEntities.NHANVIENs on x.NhanVienID equals z.NhanVienID
                        join a in mKaraokeEntities.BANs on y.BanID equals a.BanID                                                
                        select new BOPrintOrder
                        {
                            TenNhanVien=z.TenNhanVien,
                            MaHoaDon=y.MaHoaDon,
                            TenBan=a.TenBan,
                            NgayBan=(DateTime)x.NgayBan
                        };
            return query;
        }
        public IQueryable<BOPrintOrderItem> GetPrintOrderItemKM(BOPrintOrderItem item)
        {
            var query =
                        from a in mKaraokeEntities.CHITIETBANHANGs.Where(o => o.ChiTietBanHangID_Ref == item.ID)
                        join b in mKaraokeEntities.MENUKICHTHUOCMONs on a.KichThuocMonID equals b.KichThuocMonID
                        join c in mKaraokeEntities.MENUMONs on b.MonID equals c.MonID                        
                        select new
                        {
                            ID = a.ChiTietBanHangID,
                            MonID = b.KichThuocMonID,
                            TenDai = c.TenDai,
                            TenLoaiBan = b.TenLoaiBan,
                            GiamGia = a.GiamGia,
                            GiaBan = a.GiaBan,
                            SoLuong = (int)a.SoLuongBan,
                            ThanhTien = (decimal)a.ThanhTien
                        } into x
                        group x by new { x.ID, x.MonID, x.TenDai, x.TenLoaiBan, x.GiaBan, x.GiamGia } into y
                        select new BOPrintOrderItem
                        {
                            ID = y.Key.ID,
                            MonID = y.Key.MonID,
                            TenMon = y.Key.TenLoaiBan == "" ? y.Key.TenDai : y.Key.TenDai + "(" + y.Key.TenLoaiBan + ")",
                            SoLuong = y.Sum(c => c.SoLuong),
                            GiaBan = y.Key.GiaBan,
                            GiamGia = y.Key.GiamGia,
                            ThanhTien = y.Sum(c => c.ThanhTien)
                        };
            return query;
        }
        public IQueryable<BOPrintOrderItem> GetPrintOrderItemFromBanHangID(int banHangID)
        {            
            var query =
                        from a in mKaraokeEntities.CHITIETBANHANGs.Where(o => o.ChiTietBanHangID_Ref == null && o.BanHangID == banHangID)
                        join b in mKaraokeEntities.MENUKICHTHUOCMONs on a.KichThuocMonID equals b.KichThuocMonID
                        join c in mKaraokeEntities.MENUMONs on b.MonID equals c.MonID
                        where a.BanHangID == banHangID
                        select new
                        {   
                            ID=a.ChiTietBanHangID,
                            MonID=b.KichThuocMonID,
                            TenDai = c.TenDai,
                            TenLoaiBan=b.TenLoaiBan,
                            GiamGia=a.GiamGia,
                            GiaBan=a.GiaBan,
                            SoLuong = (int)a.SoLuongBan,
                            ThanhTien=(decimal)a.ThanhTien
                        } into x
                        orderby x.ID descending
                        group x by new { x.ID,x.MonID,x.TenDai,x.TenLoaiBan,x.GiaBan,x.GiamGia } into y
                        select new BOPrintOrderItem
                        {
                            ID=y.Key.ID,
                            MonID=y.Key.MonID,
                            TenMon =y.Key.TenLoaiBan==""?y.Key.TenDai: y.Key.TenDai + "(" + y.Key.TenLoaiBan + ")",
                            SoLuong = y.Sum(c => c.SoLuong),
                            GiaBan=y.Key.GiaBan,
                            GiamGia=y.Key.GiamGia,
                            ThanhTien=y.Sum(c=>c.ThanhTien)
                        };
            return query;
        }
        public IQueryable<BOPrintOrderItem> GetPrintOrderItemKM(BOPrintOrderItem item, int printID)
        {
            var queryCheck = from a in mKaraokeEntities.MENUMONs
                             join b in mKaraokeEntities.MENUITEMMAYINs on a.MonID equals b.MonID
                             where b.MayInID == printID
                             select a;
            var queryChiTiet = from ct in mKaraokeEntities.CHITIETLICHSUBANHANGs
                               where ct.ChiTietLichSuBanHangID_Ref == item.ID
                               select ct;
            var query =
                        from a in mKaraokeEntities.MENUMONs
                        join b in mKaraokeEntities.MENUKICHTHUOCMONs on a.MonID equals b.MonID
                        join c in queryChiTiet on b.KichThuocMonID equals c.KichThuocMonID
                        where queryCheck.Contains(a)
                        select new
                        {
                            ID = c.ChiTietLichSuBanHangID,
                            MonID = b.KichThuocMonID,
                            TenDai = a.TenDai,
                            TenLoaiBan = b.TenLoaiBan,
                            SoLuong = (int)c.SoLuong,
                            TrangThai = (int)c.TrangThai
                        } into x
                        group x by new { x.ID, x.MonID, x.TenDai, x.TenLoaiBan, x.TrangThai } into y
                        select new BOPrintOrderItem
                        {
                            ID = y.Key.ID,
                            MonID = y.Key.MonID,
                            TenMon = y.Key.TenLoaiBan == "" ? y.Key.TenDai : y.Key.TenDai + "(" + y.Key.TenLoaiBan + ")",
                            TrangThai = (int)y.Key.TrangThai,
                            SoLuong = y.Sum(c => c.SoLuong)
                        }
                   ;
            return query;
        }
        public IQueryable<BOPrintOrderItem> GetPrintOrderItem(int lichSuBanHangID, int printID)
        {
            var queryCheck = from a in mKaraokeEntities.MENUMONs
                             join b in mKaraokeEntities.MENUITEMMAYINs on a.MonID equals b.MonID
                             where b.MayInID == printID
                             select a;
            var queryChiTiet = from ct in mKaraokeEntities.CHITIETLICHSUBANHANGs
                             where ct.LichSuBanHangID==lichSuBanHangID && ct.ChiTietLichSuBanHangID_Ref==null select ct;            
            var query =
                        from a in mKaraokeEntities.MENUMONs
                        join b in mKaraokeEntities.MENUKICHTHUOCMONs on a.MonID equals b.MonID
                        join c in queryChiTiet on b.KichThuocMonID equals c.KichThuocMonID
                        where queryCheck.Contains(a) && c.LichSuBanHangID == lichSuBanHangID
                        select new
                        {                   
                            ID=c.ChiTietLichSuBanHangID,
                            MonID = b.KichThuocMonID,
                            TenDai=a.TenDai,
                            TenLoaiBan=b.TenLoaiBan,
                            SoLuong = (int)c.SoLuong,
                            TrangThai = (int)c.TrangThai
                        } into x
                        orderby x.ID ascending
                        group x by new {x.ID, x.MonID,x.TenDai,x.TenLoaiBan ,x.TrangThai } into y
                        select new BOPrintOrderItem
                        {
                            ID=y.Key.ID,             
                            MonID=y.Key.MonID,
                            TenMon =y.Key.TenLoaiBan==""?y.Key.TenDai: y.Key.TenDai + "(" + y.Key.TenLoaiBan + ")",
                            TrangThai = (int)y.Key.TrangThai,
                            SoLuong = y.Sum(c => c.SoLuong)
                        }
                   ;
            return query;
        }
    }
}
