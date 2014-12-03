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
        public FrameworkRepository<MAYIN> frMayIn;
        public FrameworkRepository<MENUITEMMAYIN> frMenuMayIn;        
        public FrameworkRepository<MENUMON> frMenuMon;
        public FrameworkRepository<MENUKICHTHUOCMON> frMenuKichThuocMon;        
        public FrameworkRepository<CHITIETLICHSUBANHANG> frChiTietLichSuBanHang;
        public FrameworkRepository<LICHSUBANHANG> frLichSuBanHang;
        public FrameworkRepository<NHANVIEN> frNhanVien;
        public FrameworkRepository<BANHANG> frBanHang;
        public FrameworkRepository<CHITIETBANHANG> frChiTietBanHang;
        public FrameworkRepository<BAN> frBan;
        public FrameworkRepository<THE> frThe;

        public BOXuliMayIn(Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
            mKaraokeEntities.ContextOptions.LazyLoadingEnabled = false;            
            frMayIn = new FrameworkRepository<MAYIN>(mKaraokeEntities, mKaraokeEntities.MAYINs);
            frMenuMayIn = new FrameworkRepository<MENUITEMMAYIN>(mKaraokeEntities, mKaraokeEntities.MENUITEMMAYINs);
            frMenuMon = new FrameworkRepository<MENUMON>(mKaraokeEntities, mKaraokeEntities.MENUMONs);
            frMenuKichThuocMon = new FrameworkRepository<MENUKICHTHUOCMON>(mKaraokeEntities, mKaraokeEntities.MENUKICHTHUOCMONs);
            frChiTietLichSuBanHang = new FrameworkRepository<CHITIETLICHSUBANHANG>(mKaraokeEntities, mKaraokeEntities.CHITIETLICHSUBANHANGs);
            frLichSuBanHang = new FrameworkRepository<LICHSUBANHANG>(mKaraokeEntities, mKaraokeEntities.LICHSUBANHANGs);
            frNhanVien = new FrameworkRepository<NHANVIEN>(mKaraokeEntities, mKaraokeEntities.NHANVIENs);
            frBanHang = new FrameworkRepository<BANHANG>(mKaraokeEntities, mKaraokeEntities.BANHANGs);
            frChiTietBanHang = new FrameworkRepository<CHITIETBANHANG>(mKaraokeEntities, mKaraokeEntities.CHITIETBANHANGs);
            frBan = new FrameworkRepository<BAN>(mKaraokeEntities, mKaraokeEntities.BANs);
            frThe = new FrameworkRepository<THE>(mKaraokeEntities, mKaraokeEntities.THEs);

            _CAIDATMAYINBEP = BOCaiDatMayInBep.GetQueryNoTracking(mTransit);            
            _CAIDATMAYINHOADON = BOCaiDatMayInHoaDon.GetQueryNoTracking(mTransit);
            _ImageLogo = Utilities.ImageHandler.BitmapImage2Bitmap(this._CAIDATMAYINHOADON.Logo);
        }
        public IQueryable<BOMayIn> AllPrinting(int lichSuBanHang)
        {
            var queryMenuMon = from x in frMenuMon.Query()
                               join y in frMenuKichThuocMon.Query() on x.MonID equals y.MonID
                               join z in frChiTietLichSuBanHang.Query() on y.KichThuocMonID equals z.KichThuocMonID
                               where z.LichSuBanHangID == lichSuBanHang
                               select x;
            var queryMenuMayIn = from x in queryMenuMon
                                 join y in frMenuMayIn.Query() on x.MonID equals y.MonID
                                 where y.Deleted==false && y.Visual==true
                                 select y;
            var resuilt = from x in frMayIn.Query()
                          join y in frMenuMayIn.Query() on x.MayInID equals y.MayInID
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
            var resuilt= from a in frMayIn.Query()
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
            var query = from a in frBanHang.Query().Where(o => o.BanHangID == banHangID)
                        join b in frNhanVien.Query() on a.NhanVienID equals b.NhanVienID
                        join c in frBan.Query() on a.BanID equals c.BanID
                        join d in frThe.Query() on a.TheID equals d.TheID into list
                        from e in list.DefaultIfEmpty()
                        where a.BanHangID == banHangID
                        select new BOPrintOrder
                        {
                            TenThe=e.TenThe,
                            TenNhanVien = b.TenNhanVien,
                            MaHoaDon = a.MaHoaDon,
                            TenBan = c.TenBan,
                            NgayBan = (DateTime)a.NgayBan,
                            BanHang = a
                        };
            return query;
        }
        public IQueryable<BOPrintOrder> GetOrderFromLichSuBanHang(int lichSuBanHangID)
        {
            var query = from x in frLichSuBanHang.Query().Where(o=>o.LichSuBanHangID==lichSuBanHangID)
                        //where x.LichSuBanHangID == lichSuBanHangID
                        join y in frBanHang.Query() on x.BanHangID equals y.BanHangID
                        join z in frNhanVien.Query() on x.NhanVienID equals z.NhanVienID
                        join a in frBan.Query() on y.BanID equals a.BanID                                                
                        select new BOPrintOrder
                        {
                            TenNhanVien=z.TenNhanVien,
                            MaHoaDon=y.MaHoaDon,
                            TenBan=a.TenBan,
                            NgayBan=(DateTime)x.NgayBan
                        };
            return query;
        }
        public IQueryable<BOPrintOrderItem> GetPrintOrderItemFromBanHangID(int banHangID, int printID)
        {            
            var query =
                        from a in frChiTietBanHang.Query().Where(o => o.BanHangID == banHangID)
                        join b in frMenuKichThuocMon.Query() on a.KichThuocMonID equals b.KichThuocMonID
                        join c in frMenuMon.Query() on b.MonID equals c.MonID
                        where a.BanHangID == banHangID
                        select new
                        {
                            KichThuoc=b.KichThuocMonID,
                            TenDai = c.TenDai,
                            TenLoaiBan=b.TenLoaiBan,
                            GiamGia=a.GiamGia,
                            GiaBan=a.GiaBan,
                            SoLuong = (int)a.SoLuongBan,
                            ThanhTien=(decimal)a.ThanhTien
                        } into x
                        group x by new { x.KichThuoc,x.TenDai,x.TenLoaiBan,x.GiaBan,x.GiamGia } into y
                        select new BOPrintOrderItem
                        {
                            TenMon = y.Key.TenDai + "(" + y.Key.TenLoaiBan + ")",
                            SoLuong = y.Sum(c => c.SoLuong),
                            GiaBan=y.Key.GiaBan,
                            GiamGia=y.Key.GiamGia,
                            ThanhTien=y.Sum(c=>c.ThanhTien)
                        };
            return query;
        }
        public IQueryable<BOPrintOrderItem> GetPrintOrderItem(int lichSuBanHangID, int printID)
        {
            var queryCheck = from a in frMenuMon.Query()
                             join b in frMenuMayIn.Query() on a.MonID equals b.MonID
                             where b.MayInID == printID
                             select a;
            var queryChiTiet=from ct in frChiTietLichSuBanHang.Query() 
                             where ct.LichSuBanHangID==lichSuBanHangID select ct;
            //var query =
            //            from a in frMenuMon.Query()
            //            join b in frMenuKichThuocMon.Query() on a.MonID equals b.MonID
            //            join c in queryChiTiet on b.KichThuocMonID equals c.KichThuocMonID
            //            where queryCheck.Contains(a) && c.LichSuBanHangID == lichSuBanHangID
            //            select new
            //            {
            //                TenMon = a.TenDai + "(" + b.TenLoaiBan + ")",
            //                SoLuong = (int)c.SoLuong,
            //                TrangThai = (int)c.TrangThai
            //            } into x
            //            group x by new { x.TenMon,x.TrangThai } into y
            //            select new BOPrintOrderItem
            //            {
            //                TenMon = y.Key.TenMon,
            //                TrangThai=(int)y.Key.TrangThai,
            //                SoLuong=y.Sum(c=>c.SoLuong)
            //            }
            //            ;
            var query =
                        from a in frMenuMon.Query()
                        join b in frMenuKichThuocMon.Query() on a.MonID equals b.MonID
                        join c in queryChiTiet on b.KichThuocMonID equals c.KichThuocMonID
                        where queryCheck.Contains(a) && c.LichSuBanHangID == lichSuBanHangID
                        select new
                        {
                            KichThuoc = b.KichThuocMonID,
                            TenDai=a.TenDai,
                            TenLoaiBan=b.TenLoaiBan,
                            SoLuong = (int)c.SoLuong,
                            TrangThai = (int)c.TrangThai
                        } into x
                        group x by new { x.KichThuoc,x.TenDai,x.TenLoaiBan ,x.TrangThai } into y
                        select new BOPrintOrderItem
                        {
                            TenMon = y.Key.TenDai + "(" + y.Key.TenLoaiBan + ")",
                            TrangThai = (int)y.Key.TrangThai,
                            SoLuong = y.Sum(c => c.SoLuong)
                        }
                   ;
            return query;
        }
    }
}
