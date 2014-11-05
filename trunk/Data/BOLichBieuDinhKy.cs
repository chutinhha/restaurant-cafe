using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichBieuDinhKy
    {
        public static List<LICHBIEUDINHKY> GetAll(Transit mTransit)
        {
            var res = (from lb in mTransit.KaraokeEntities.LICHBIEUDINHKies
                       join l in mTransit.KaraokeEntities.MENULOAIGIAs on lb.LoaiGiaID equals l.LoaiGiaID
                       where lb.LoaiGiaID == l.LoaiGiaID
                       orderby lb.UuTien ascending, l.Ten ascending, lb.TenLichBieu ascending
                       select new
                       {
                           LICHBIEUDINHKies = lb,
                           MENULOAIGIAs = l
                       }).ToList().Select(s => s.LICHBIEUDINHKies);
            return res.ToList();

        }

        public static int Them(LICHBIEUDINHKY item, Transit mTransit)
        {
            mTransit.KaraokeEntities.LICHBIEUDINHKies.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.LichBieuDinhKyID;
        }

        public static int Xoa(int LichBieuDinhKyID, Transit mTransit)
        {
            LICHBIEUDINHKY item = (from x in mTransit.KaraokeEntities.LICHBIEUDINHKies where x.LichBieuDinhKyID == LichBieuDinhKyID select x).First();
            item.Deleted = true;
            mTransit.KaraokeEntities.SaveChanges();
            return item.LichBieuDinhKyID;
        }

        public static int Sua(LICHBIEUDINHKY item, Transit mTransit)
        {
            LICHBIEUDINHKY m = (from x in mTransit.KaraokeEntities.LICHBIEUDINHKies where x.LichBieuDinhKyID == item.LichBieuDinhKyID select x).First();
            m.LoaiGiaID = item.LoaiGiaID;
            m.TenLichBieu = item.TenLichBieu;
            m.GioBatDau = item.GioBatDau;
            m.GioKetThuc = item.GioKetThuc;
            m.UuTien = item.UuTien;
            m.GiaTriBatDau = item.GiaTriBatDau;
            m.GiaTriKetThuc = item.GiaTriKetThuc;
            m.Visual = item.Visual;
            m.Deleted = item.Deleted;
            m.Edit = false;
            mTransit.KaraokeEntities.SaveChanges();
            return item.LichBieuDinhKyID;
        }

        public static void Luu(List<LICHBIEUDINHKY> lsArray, List<LICHBIEUDINHKY> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (LICHBIEUDINHKY item in lsArray)
                {
                    if (item.LichBieuDinhKyID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (LICHBIEUDINHKY item in lsArrayDeleted)
                {
                    Xoa(item.LichBieuDinhKyID, mTransit);
                }
        }
    }
}
