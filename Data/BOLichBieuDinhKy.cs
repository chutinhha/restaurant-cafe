using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichBieuDinhKy
    {
        public static List<LICHBIEUDINHKY> GetAll()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                var res = (from lb in ke.LICHBIEUDINHKies
                           join l in ke.MENULOAIGIAs on lb.LoaiGiaID equals l.LoaiGiaID
                           where lb.LoaiGiaID == l.LoaiGiaID
                           orderby lb.UuTien ascending, l.Ten ascending, lb.TenLichBieu ascending
                           select new
                           {
                               LICHBIEUDINHKies = lb,
                               MENULOAIGIAs = l
                           }).ToList().Select(s => s.LICHBIEUDINHKies);
                return res.ToList();

            }
        }

        public static int Them(LICHBIEUDINHKY item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.LICHBIEUDINHKies.AddObject(item);
                ke.SaveChanges();
                return item.LichBieuDinhKyID;
            }
        }

        public static int Xoa(int LichBieuDinhKyID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                LICHBIEUDINHKY item = (from x in ke.LICHBIEUDINHKies where x.LichBieuDinhKyID == LichBieuDinhKyID select x).First();
                item.Deleted = true;
                ke.SaveChanges();
                return item.LichBieuDinhKyID;
            }
        }

        public static int Sua(LICHBIEUDINHKY item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                LICHBIEUDINHKY m = (from x in ke.LICHBIEUDINHKies where x.LichBieuDinhKyID == item.LichBieuDinhKyID select x).First();
                m.LoaiGiaID = item.LoaiGiaID;
                m.TenLichBieu = item.TenLichBieu;
                m.GioBatDau = item.GioBatDau;
                m.GioKetThuc = item.GioKetThuc;
                m.UuTien = item.UuTien;
                m.GiaTriBatDau = item.GiaTriBatDau;
                m.GiaTriKetThuc = item.GiaTriKetThuc;
                m.Visual = item.Visual;
                m.Deleted = item.Deleted;
                ke.SaveChanges();
                return item.LichBieuDinhKyID;
            }
        }
    }
}
