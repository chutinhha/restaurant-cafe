using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichBieuDinhKy
    {
        public LICHBIEUDINHKY LichBieuDinhKy { get; set; }
        public MENULOAIGIA MenuLoaiGia { get; set; }
        public string TenKhu { get; set; }
        KaraokeEntities mKaraokeEntities = null;
        public BOLichBieuDinhKy(Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }
        public static IQueryable<LICHBIEUDINHKY> GetAll(Transit transit)
        {
            return FrameworkRepository<LICHBIEUDINHKY>.QueryNoTracking(transit.KaraokeEntities.LICHBIEUDINHKies).Where(o => o.Deleted == false);
        }
        public static IQueryable<LICHBIEUDINHKY> GetAllVisual(Transit transit)
        {
            return FrameworkRepository<LICHBIEUDINHKY>.QueryNoTracking(transit.KaraokeEntities.LICHBIEUDINHKies).Where(o => o.Deleted == false && o.Visual == true);
        }
        public BOLichBieuDinhKy()
        {
            LichBieuDinhKy = new LICHBIEUDINHKY();
            MenuLoaiGia = new MENULOAIGIA();
            TenKhu = "";
        }
        public static IQueryable<BOLichBieuDinhKy> GetAllVisualRun(Transit transit)
        {

            DateTime dt = DateTime.Now;
            int dayOfWeek = (int)dt.DayOfWeek;
            TimeSpan ts = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
            var querya = BOMenuLoaiGia.GetAllVisual(transit);
            var queryb = from b in GetAllVisual(transit)
                         where
                             ts.CompareTo(b.GioBatDau.Value) >= 0 &&
                             ts.CompareTo(b.GioKetThuc.Value) <= 0 &&
                             (
                                b.KhuID == null ||
                                b.KhuID == transit.Ban.KhuID
                             )
                         select b;

            //tim theo ngay trong tuan
            var query1 = from a in querya
                         join b in queryb on a.LoaiGiaID equals b.LoaiGiaID
                         where
                            b.TheLoaiID == 1 &&
                            (
                                (dayOfWeek >= b.GiaTriBatDau && dayOfWeek <= b.GiaTriKetThuc && b.GiaTriBatDau < b.GiaTriKetThuc) ||
                                (
                                    (dayOfWeek >= b.GiaTriBatDau && dayOfWeek <= 6) || (dayOfWeek <= b.GiaTriKetThuc && dayOfWeek >= 0) && b.GiaTriBatDau > b.GiaTriKetThuc
                                )
                            )
                         select new BOLichBieuDinhKy
                         {
                             MenuLoaiGia = a,
                             LichBieuDinhKy = b
                         };
            //select a;
            //tim theo ngay trong thang
            var query2 = from a in querya
                         join b in queryb on a.LoaiGiaID equals b.LoaiGiaID
                         where
                             b.TheLoaiID == 2 &&
                             dt.Day >= b.GiaTriBatDau && dt.Day <= b.GiaTriKetThuc
                         select new BOLichBieuDinhKy
                          {
                              MenuLoaiGia = a,
                              LichBieuDinhKy = b
                          };
            //select a;
            //tim theo ngay trong nam
            var query3 = from a in querya
                         join b in queryb on a.LoaiGiaID equals b.LoaiGiaID
                         where
                             b.TheLoaiID == 3 &&
                             b.GiaTriBatDau == dt.Day && b.GiaTriKetThuc == dt.Month
                         select new BOLichBieuDinhKy
                         {
                             MenuLoaiGia = a,
                             LichBieuDinhKy = b
                         };
            //select a;
            return
                    from a in query1.Union(query2).Union(query3).Distinct() select a;
        }

        public IQueryable<BOLichBieuDinhKy> GetAll()
        {
            var res = (from lb in mKaraokeEntities.LICHBIEUDINHKies
                       join k in mKaraokeEntities.KHUs on lb.KhuID equals k.KhuID into k1
                       from khu in k1.DefaultIfEmpty()
                       join l in mKaraokeEntities.MENULOAIGIAs on lb.LoaiGiaID equals l.LoaiGiaID
                       where lb.LoaiGiaID == l.LoaiGiaID
                       orderby lb.UuTien ascending, l.Ten ascending, lb.TenLichBieu ascending
                       select new BOLichBieuDinhKy
                       {
                           LichBieuDinhKy = lb,
                           MenuLoaiGia = l,
                           TenKhu = (khu.TenKhu == null ? "Tất cả khu" : khu.TenKhu)
                       });
            return res;

        }

        public void Luu(List<BOLichBieuDinhKy> lsArray)
        {
            foreach (BOLichBieuDinhKy item in lsArray)
            {
                if (item.LichBieuDinhKy.LichBieuDinhKyID == 0)
                {
                    mKaraokeEntities.LICHBIEUDINHKies.AddObject(item.LichBieuDinhKy);
                }

            }
            mKaraokeEntities.SaveChanges();
        }
        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.LICHBIEUDINHKies);
        }

        public IQueryable<KHU> GetKhu()
        {
            return BOKhu.GetQueryNoTracking(mKaraokeEntities);
        }

        public IQueryable<MENULOAIGIA> GetMenuLoaiGia()
        {
            return BOMenuLoaiGia.GetQueryNoTracking(mKaraokeEntities);
        }

        public IQueryable<LOAILICHBIEU> GetLoaiLichBieu(int TheLoaiID)
        {
            return BOLoaiLichBieu.GetQueryNoTracking(TheLoaiID, mKaraokeEntities);
        }
        public IQueryable<THELOAILICHBIEU> GetTheLoaiLichBieu()
        {
            return BOTheLoaiLichBieu.GetQueryNoTracking(mKaraokeEntities);
        }

    }
}
