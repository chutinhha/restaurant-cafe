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
                                 //where y.Deleted==false && y.Visual==true
                                 select y;
            var resuilt = from x in mKaraokeEntities.MAYINs.Where(o=>o.MayInHoaDon==false && o.Deleted==false && o.Visual==true)
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
                         where a.MayInHoaDon==true && a.Deleted==false && a.Visual==true
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
                        join c in mKaraokeEntities.BANs on a.BanID equals c.BanID into listBan
                        from h in listBan.DefaultIfEmpty()
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
                            TenBan = h.TenBan,
                            NgayBan = a.NgayBan.Value,
                            NgayKetThuc=a.NgayKetThuc.Value,
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
        public IQueryable<BOPrintOrderItem> GetPrintOrderItemKM(int banHangID,BOPrintOrderItem item)
        {
            var query =
                        from a in mKaraokeEntities.CHITIETBANHANGs.Where(o => o.KichThuocMonID_Ref == item.MonID && o.BanHangID==banHangID)
                        join b in mKaraokeEntities.MENUKICHTHUOCMONs on a.KichThuocMonID equals b.KichThuocMonID
                        join c in mKaraokeEntities.MENUMONs on b.MonID equals c.MonID                        
                        select new
                        {
                            DonViID = b.DonViID,
                            KichThuocThuc = a.KichThuocLoaiBan,
                            KichThuocBan = b.KichThuocLoaiBan,
                            MonID = b.KichThuocMonID,
                            TenDai = c.TenDai,
                            TenLoaiBan = b.TenLoaiBan,
                            GiamGia = a.GiamGia,
                            GiaBan = a.GiaBan,
                            SoLuong = (int)a.SoLuongBan,
                            ThanhTien = (decimal)a.ThanhTien
                        } into x
                        group x by new {x.MonID, x.TenDai, x.TenLoaiBan, x.GiaBan, x.GiamGia,x.DonViID,x.KichThuocThuc,x.KichThuocBan } into y
                        select new BOPrintOrderItem
                        {
                            DonViID = y.Key.DonViID.Value,
                            KichThuocThuc = y.Key.KichThuocThuc,
                            KichThuocBan = y.Key.KichThuocBan,      
                            MonID = y.Key.MonID,                            
                            TenDai=y.Key.TenDai,
                            TenLoaiBan=y.Key.TenLoaiBan,
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
                        where a.BanHangID == banHangID && c.MonID!=mTransit.CaiDatBanHang.MonTinhGio
                        select new
                        {
                            DonViID = b.DonViID,
                            KichThuocThuc = a.KichThuocLoaiBan,
                            KichThuocBan = b.KichThuocLoaiBan,
                            MonID=b.KichThuocMonID,
                            TenDai = c.TenDai,
                            TenLoaiBan=b.TenLoaiBan,
                            GiamGia=a.GiamGia,
                            GiaBan=a.GiaBan,
                            SoLuong = (int)a.SoLuongBan,
                            ThanhTien=(decimal)a.ThanhTien
                        } into x
                        //orderby x.ID descending
                        group x by new { x.MonID,x.TenDai,x.TenLoaiBan,x.GiaBan,x.GiamGia,x.DonViID,x.KichThuocBan,x.KichThuocThuc } into y
                        select new BOPrintOrderItem
                        {
                            DonViID = y.Key.DonViID.Value,
                            KichThuocThuc = y.Key.KichThuocThuc,
                            KichThuocBan = y.Key.KichThuocBan,      
                            TenDai=y.Key.TenDai,
                            TenLoaiBan=y.Key.TenLoaiBan,
                            MonID=y.Key.MonID,                            
                            SoLuong = y.Sum(c => c.SoLuong),
                            GiaBan=y.Key.GiaBan,
                            GiamGia=y.Key.GiamGia,
                            ThanhTien=y.Sum(c=>c.ThanhTien)
                        };
            return query;
        }
        public IQueryable<BOPrintOrderItem> GetPrintOrderItemTimeFromBanHangID(int banHangID)
        {
            var query =
                        from a in mKaraokeEntities.CHITIETBANHANGs.Where(o => o.ChiTietBanHangID_Ref == null && o.BanHangID == banHangID)
                        join b in mKaraokeEntities.MENUKICHTHUOCMONs on a.KichThuocMonID equals b.KichThuocMonID
                        join c in mKaraokeEntities.MENUMONs on b.MonID equals c.MonID
                        where a.BanHangID == banHangID && c.MonID==mTransit.CaiDatBanHang.MonTinhGio
                        select new
                        {
                            DonViID = b.DonViID,
                            KichThuocThuc = a.KichThuocLoaiBan,
                            KichThuocBan = b.KichThuocLoaiBan,
                            MonID = b.KichThuocMonID,
                            TenDai = c.TenDai,
                            TenLoaiBan = b.TenLoaiBan,
                            GiamGia = a.GiamGia,
                            GiaBan = a.GiaBan,
                            SoLuong = (int)a.SoLuongBan,
                            ThanhTien = (decimal)a.ThanhTien
                        } into x
                        //orderby x.ID descending
                        group x by new { x.MonID, x.TenDai, x.TenLoaiBan, x.GiaBan, x.GiamGia, x.DonViID, x.KichThuocBan, x.KichThuocThuc } into y
                        select new BOPrintOrderItem
                        {
                            DonViID = y.Key.DonViID.Value,
                            KichThuocThuc = y.Key.KichThuocThuc,
                            KichThuocBan = y.Key.KichThuocBan,
                            TenDai = y.Key.TenDai,
                            TenLoaiBan = y.Key.TenLoaiBan,
                            MonID = y.Key.MonID,
                            SoLuong = y.Sum(c => c.SoLuong),
                            GiaBan = y.Key.GiaBan,
                            GiamGia = y.Key.GiamGia,
                            ThanhTien = y.Sum(c => c.ThanhTien)
                        };
            return query;
        }
        public IQueryable<BOPrintOrderItem> GetPrintOrderItemKM(int lichsuBanHang,BOPrintOrderItem item, int printID)
        {
            var queryCheck = from a in mKaraokeEntities.MENUMONs
                             join b in mKaraokeEntities.MENUITEMMAYINs on a.MonID equals b.MonID
                             where b.MayInID == printID
                             select a;
            var queryChiTiet = from ct in mKaraokeEntities.CHITIETLICHSUBANHANGs
                               where ct.KichThuocMonID_Ref == item.MonID && ct.LichSuBanHangID==lichsuBanHang
                               select ct;
            var query =
                        from a in mKaraokeEntities.MENUMONs
                        join b in mKaraokeEntities.MENUKICHTHUOCMONs on a.MonID equals b.MonID
                        join c in queryChiTiet on b.KichThuocMonID equals c.KichThuocMonID
                        where queryCheck.Contains(a)
                        select new
                        {
                            DonViID = b.DonViID,
                            KichThuocThuc = c.KichThuocLoaiBan,
                            KichThuocBan = b.KichThuocLoaiBan,
                            MonID = b.KichThuocMonID,
                            TenDai = a.TenDai,
                            TenLoaiBan = b.TenLoaiBan,                            
                            SoLuong = (int)c.SoLuong,
                            TrangThai = (int)c.TrangThai
                        } into x
                        group x by new { x.MonID, x.TenDai, x.TenLoaiBan, x.TrangThai, x.DonViID, x.KichThuocThuc, x.KichThuocBan } into y
                        select new BOPrintOrderItem
                        {                                                        
                            DonViID = y.Key.DonViID.Value,
                            KichThuocThuc = y.Key.KichThuocThuc,
                            KichThuocBan = y.Key.KichThuocBan,      
                            MonID=y.Key.MonID,
                            TenDai=y.Key.TenDai,
                            TenLoaiBan=y.Key.TenLoaiBan,                                                                                    
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
                            DonViID =b.DonViID,
                            KichThuocThuc =c.KichThuocLoaiBan,
                            KichThuocBan =b.KichThuocLoaiBan,
                            MonID = b.KichThuocMonID,
                            TenDai=a.TenDai,
                            TenLoaiBan = b.TenLoaiBan,
                            SoLuong = (int)c.SoLuong,
                            TrangThai = (int)c.TrangThai
                        } into x
                        //orderby x.ID ascending
                        group x by new {x.MonID,x.TenDai,x.TenLoaiBan ,x.TrangThai,x.DonViID,x.KichThuocThuc,x.KichThuocBan} into y
                        select new BOPrintOrderItem
                        {
                            DonViID = y.Key.DonViID.Value,
                            KichThuocThuc = y.Key.KichThuocThuc,
                            KichThuocBan = y.Key.KichThuocBan,      
                            MonID=y.Key.MonID,
                            TenDai=y.Key.TenDai,
                            TenLoaiBan=y.Key.TenLoaiBan,                                                        
                            //TenMon =y.Key.TenLoaiBan==""?y.Key.TenDai: y.Key.TenDai + "(" + y.Key.TenLoaiBan + ")",
                            TrangThai = (int)y.Key.TrangThai,
                            SoLuong = y.Sum(c => c.SoLuong)
                        }
                   ;
            return query;
        }
        public BOPrinterReportDaily GetBaoCaoNgayTong(DateTime dtFrom, DateTime dtTo)
        {
            BOBaoCaoNgay baocao = new BOBaoCaoNgay(mTransit);
            BOPrinterReportDaily print = new BOPrinterReportDaily();
            print.BAOCAONGAYTONG = baocao.GetBaoCaoNgayTong(dtFrom, dtTo).FirstOrDefault();
            print.CAIDATTHONGTINCONGTY = baocao.GetCaiDatThongTinCongTy().FirstOrDefault();
            return print;
        }
        public BOThuChi GetThuChi(int thuchiID)
        {
            return BOThuChi.GetThuChiByID(thuchiID,mKaraokeEntities);
        }
        public IQueryable<BOChiTietThuChi> GetChiTietThuChi(int thuchiID)
        {
            return BOChiTietThuChi.GetAllByThuChiID(thuchiID, mKaraokeEntities);
        }
    }
}
