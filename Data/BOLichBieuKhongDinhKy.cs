using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichBieuKhongDinhKy
    {
        public LICHBIEUKHONGDINHKY LichBieuKhongDinhKy { get; set; }
        public MENULOAIGIA MenuLoaiGia { get; set; }
        public string TenKhu { get; set; }
        KaraokeEntities mKaraokeEntities = null;
        public BOLichBieuKhongDinhKy(Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }

        public BOLichBieuKhongDinhKy()
        {
            LichBieuKhongDinhKy = new LICHBIEUKHONGDINHKY();
            MenuLoaiGia = new MENULOAIGIA();
            TenKhu = "";
        }

        public static IQueryable<LICHBIEUKHONGDINHKY> GetAllVisual(KaraokeEntities kara)
        {
            return kara.LICHBIEUKHONGDINHKies.Where(o => o.Deleted == false && o.Visual == true);
        }

        public static IQueryable<BOLichBieuKhongDinhKy> GetAllVisualRun(KaraokeEntities kara,BAN ban)
        {
            DateTime dtNow = DateTime.Now;
            DateTime dt = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day);
            TimeSpan ts = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
            var querya = BOMenuLoaiGia.GetAllVisual(kara);
            var queryb = from b in GetAllVisual(kara)
                         where
                             ts.CompareTo(b.GioBatDau.Value) >= 0 && ts.CompareTo(b.GioKetThuc.Value) <= 0 &&
                             dt.CompareTo(b.NgayBatDau.Value) >= 0 && dt.CompareTo(b.NgayKetThuc.Value) <= 0 &&
                             (
                                b.KhuID == null ||
                                b.KhuID == ban.KhuID
                             )
                         select b;
            var query = from a in querya
                        join b in queryb on a.LoaiGiaID equals b.LoaiGiaID
                        select new BOLichBieuKhongDinhKy
                        {
                            MenuLoaiGia = a,
                            LichBieuKhongDinhKy = b
                        };
            return query.Distinct();
        }
        //public IQueryable<MENULOAIGIA> GetMenuLoaiGia()
        //{
        //    DateTime dt = DateTime.Now;            
        //    TimeSpan ts = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
        //    var querya = from a in frmMenuLoaiGia.Query()
        //                 where
        //                      a.Visual == true &&
        //                      a.Deleted == false
        //                 select a;
        //    var queryb = from b in frmLichBieuKhongDinhKy.Query()
        //                 where                             
        //                     b.Deleted == false &&
        //                     b.Visual == true &&
        //                     ts.CompareTo(b.GioBatDau.Value) >= 0 && ts.CompareTo(b.GioKetThuc.Value) <= 0 &&
        //                     dt.CompareTo(b.NgayBatDau.Value)>=0 && dt.CompareTo(b.NgayKetThuc.Value)<=0
        //                 select b;
        //    var query = from a in querya
        //                join b in queryb on a.LoaiGiaID equals b.LoaiGiaID
        //                //select new BOLichBieuKhongDinhKy
        //                //{
        //                //    MenuLoaiGia = a,
        //                //    LichBieuKhongDinhKy = b
        //                //};
        //                select a;
        //    return query.Distinct();
        //}

        public IQueryable<BOLichBieuKhongDinhKy> GetAll()
        {
            var res = from lb in mKaraokeEntities.LICHBIEUKHONGDINHKies
                       join k in mKaraokeEntities.KHUs on lb.KhuID equals k.KhuID into k1
                       from khu in k1.DefaultIfEmpty()
                       join l in mKaraokeEntities.MENULOAIGIAs on lb.LoaiGiaID equals l.LoaiGiaID
                       where lb.LoaiGiaID == l.LoaiGiaID && lb.Deleted==false
                       orderby l.Ten ascending, lb.TenLichBieu ascending
                       select new BOLichBieuKhongDinhKy
                       {
                           LichBieuKhongDinhKy = lb,
                           MenuLoaiGia = l,
                           TenKhu = (khu.TenKhu == null ? "Tất cả khu" : khu.TenKhu)
                       };
            return res;
        }
        public void Luu(List<BOLichBieuKhongDinhKy> lsArray)
        {
            foreach (BOLichBieuKhongDinhKy item in lsArray)
            {
                if (item.LichBieuKhongDinhKy.LichBieuKhongDinhKyID == 0)
                {
                    mKaraokeEntities.LICHBIEUKHONGDINHKies.AddObject(item.LichBieuKhongDinhKy);
                }

            }
            mKaraokeEntities.SaveChanges();
        }

        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.LICHBIEUKHONGDINHKies);
        }

        public IQueryable<KHU> GetKhu()
        {
            return BOKhu.GetQueryNoTracking(mKaraokeEntities);
        }

        public IQueryable<MENULOAIGIA> GetMenuLoaiGia()
        {
            return BOMenuLoaiGia.GetQueryNoTracking(mKaraokeEntities);
        }        
    }
}
