using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichBieuKhongDinhKy
    {
        public static List<LICHBIEUKHONGDINHKY> GetAll(Transit mTransit)
        {
            var res = (from lb in mTransit.KaraokeEntities.LICHBIEUKHONGDINHKies
                       join l in mTransit.KaraokeEntities.MENULOAIGIAs on lb.LoaiGiaID equals l.LoaiGiaID
                       where lb.LoaiGiaID == l.LoaiGiaID
                       orderby l.Ten ascending, lb.TenLichBieu ascending
                       select new
                       {
                           LICHBIEUKHONGDINHKies = lb,
                           MENULOAIGIAs = l
                       }).ToList().Select(s => s.LICHBIEUKHONGDINHKies);
            return res.ToList();

        }

        public static int Them(LICHBIEUKHONGDINHKY item, Transit mTransit)
        {
            mTransit.KaraokeEntities.LICHBIEUKHONGDINHKies.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.LichBieuKhongDinhKyID;
        }

        public static int Xoa(int LichBieuKhongDinhKyID, Transit mTransit)
        {
            LICHBIEUKHONGDINHKY item = (from x in mTransit.KaraokeEntities.LICHBIEUKHONGDINHKies where x.LichBieuKhongDinhKyID == LichBieuKhongDinhKyID select x).First();
            item.Deleted = true;
            mTransit.KaraokeEntities.SaveChanges();
            return item.LichBieuKhongDinhKyID;
        }

        public static int Sua(LICHBIEUKHONGDINHKY item, Transit mTransit)
        {
            LICHBIEUKHONGDINHKY m = (from x in mTransit.KaraokeEntities.LICHBIEUKHONGDINHKies where x.LichBieuKhongDinhKyID == item.LichBieuKhongDinhKyID select x).First();
            m.LoaiGiaID = item.LoaiGiaID;
            m.TenLichBieu = item.TenLichBieu;
            m.GioBatDau = item.GioBatDau;
            m.GioKetThuc = item.GioKetThuc;
            m.Visual = item.Visual;
            m.Deleted = item.Deleted;
            mTransit.KaraokeEntities.SaveChanges();
            return item.LichBieuKhongDinhKyID;
        }
    }
}
