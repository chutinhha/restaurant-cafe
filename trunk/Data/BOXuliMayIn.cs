using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOXuliMayIn
    {
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

        public BOXuliMayIn(Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
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
        public IQueryable<BOPrintOrder> GetOrderFromBanHangID(int banHangID)
        {
            var query = from a in frBanHang.Query()
                        join b in frNhanVien.Query() on a.NhanVienID equals b.NhanVienID
                        join c in frBan.Query() on a.BanID equals c.BanID
                        select new BOPrintOrder
                        {
                            TenNhanVien = b.TenNhanVien,
                            MaHoaDon = (int)a.MaHoaDon,
                            TenBan = c.TenBan,
                            NgayBan = (DateTime)a.NgayBan
                        };
            return query;
        }
        public IQueryable<BOPrintOrder> GetOrderFromLichSuBanHang(int lichSuBanHangID)
        {
            var query = from x in frLichSuBanHang.Query()
                        where x.LichSuBanHangID == lichSuBanHangID
                        join y in frBanHang.Query() on x.BanHangID equals y.BanHangID
                        join z in frNhanVien.Query() on x.NhanVienID equals z.NhanVienID
                        join a in frBan.Query() on y.BanID equals a.BanID                                                
                        select new BOPrintOrder
                        {
                            TenNhanVien=z.TenNhanVien,
                            MaHoaDon=(int)y.MaHoaDon,
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
                            TenMon = c.TenDai + "(" + b.TenLoaiBan + ")",
                            SoLuong = (int)a.SoLuongBan                            
                        } into x
                        group x by new { x.TenMon } into y
                        select new BOPrintOrderItem
                        {
                            TenMon = y.Key.TenMon,                            
                            SoLuong = y.Sum(c => c.SoLuong)
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
            var query =
                        from a in frMenuMon.Query()
                        join b in frMenuKichThuocMon.Query() on a.MonID equals b.MonID
                        join c in queryChiTiet on b.KichThuocMonID equals c.KichThuocMonID
                        where queryCheck.Contains(a) && c.LichSuBanHangID == lichSuBanHangID
                        select new
                        {
                            TenMon = a.TenDai + "(" + b.TenLoaiBan + ")",                        
                            SoLuong = (int)c.SoLuong,
                            TrangThai = (int)c.TrangThai
                        } into x
                        group x by new { x.TenMon,x.TrangThai } into y
                        select new BOPrintOrderItem
                        {
                            TenMon = y.Key.TenMon,
                            TrangThai=(int)y.Key.TrangThai,
                            SoLuong=y.Sum(c=>c.SoLuong)
                        }
                        ;
            return query;
        }
    }
}
