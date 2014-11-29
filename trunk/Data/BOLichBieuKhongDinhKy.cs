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
        FrameworkRepository<LICHBIEUKHONGDINHKY> frmLichBieuKhongDinhKy = null;
        FrameworkRepository<MENULOAIGIA> frmMenuLoaiGia = null;
        FrameworkRepository<KHU> frmKhu = null;
        public BOLichBieuKhongDinhKy(Transit transit)
        {            
            frmLichBieuKhongDinhKy = new FrameworkRepository<LICHBIEUKHONGDINHKY>(transit.KaraokeEntities, transit.KaraokeEntities.LICHBIEUKHONGDINHKies);
            frmMenuLoaiGia = new FrameworkRepository<MENULOAIGIA>(transit.KaraokeEntities, transit.KaraokeEntities.MENULOAIGIAs);
            frmKhu = new FrameworkRepository<KHU>(transit.KaraokeEntities, transit.KaraokeEntities.KHUs);
        }

        public BOLichBieuKhongDinhKy()
        {
            LichBieuKhongDinhKy = new LICHBIEUKHONGDINHKY();
            MenuLoaiGia = new MENULOAIGIA();
            TenKhu = "";
        }
        public IQueryable<MENULOAIGIA> GetMenuLoaiGia()
        {
            DateTime dt = DateTime.Now;            
            TimeSpan ts = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
            var querya = from a in frmMenuLoaiGia.Query()
                         where
                              a.Visual == true &&
                              a.Deleted == false
                         select a;
            var queryb = from b in frmLichBieuKhongDinhKy.Query()
                         where                             
                             b.Deleted == false &&
                             b.Visual == true &&
                             ts.CompareTo(b.GioBatDau.Value) >= 0 && ts.CompareTo(b.GioKetThuc.Value) <= 0 &&
                             dt.CompareTo(b.NgayBatDau.Value)>=0 && dt.CompareTo(b.NgayKetThuc.Value)<=0
                         select b;
            var query = from a in querya
                        join b in queryb on a.LoaiGiaID equals b.LoaiGiaID
                        //select new BOLichBieuKhongDinhKy
                        //{
                        //    MenuLoaiGia = a,
                        //    LichBieuKhongDinhKy = b
                        //};
                        select a;
            return query.Distinct();
        }
        public IQueryable<BOLichBieuKhongDinhKy> GetAll(Transit mTransit)
        {
            var res = (from lb in frmLichBieuKhongDinhKy.Query()
                       join k in frmKhu.Query() on lb.KhuID equals k.KhuID into k1
                       from khu in k1.DefaultIfEmpty()
                       join l in frmMenuLoaiGia.Query() on lb.LoaiGiaID equals l.LoaiGiaID
                       where lb.LoaiGiaID == l.LoaiGiaID
                       orderby l.Ten ascending, lb.TenLichBieu ascending
                       select new BOLichBieuKhongDinhKy
                       {
                           LichBieuKhongDinhKy = lb,
                           MenuLoaiGia = l,
                           TenKhu = (khu.TenKhu == null ? "Tất cả khu" : khu.TenKhu)
                       });
            return res;
        }

        public int Them(BOLichBieuKhongDinhKy item, Transit mTransit)
        {
            frmLichBieuKhongDinhKy.AddObject(item.LichBieuKhongDinhKy);
            return item.LichBieuKhongDinhKy.LichBieuKhongDinhKyID;
        }

        public int Xoa(BOLichBieuKhongDinhKy item, Transit mTransit)
        {
            item.LichBieuKhongDinhKy.Deleted = true;
            frmLichBieuKhongDinhKy.Update(item.LichBieuKhongDinhKy);
            return item.LichBieuKhongDinhKy.LichBieuKhongDinhKyID;
        }

        public int Sua(BOLichBieuKhongDinhKy item, Transit mTransit)
        {
            item.LichBieuKhongDinhKy.Edit = false;
            frmLichBieuKhongDinhKy.Update(item.LichBieuKhongDinhKy);
            return item.LichBieuKhongDinhKy.LichBieuKhongDinhKyID;
        }

        public void Luu(List<BOLichBieuKhongDinhKy> lsArray, List<BOLichBieuKhongDinhKy> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOLichBieuKhongDinhKy item in lsArray)
                {
                    if (item.LichBieuKhongDinhKy.LichBieuKhongDinhKyID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOLichBieuKhongDinhKy item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }

            frmLichBieuKhongDinhKy.Commit();
        }


    }
}
