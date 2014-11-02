using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuNhom
    {
        public static List<MENUNHOM> GetAll(int LoaiNhomID, bool IsBanHang, Transit mTransit)
        {
            return GetAll(LoaiNhomID, IsBanHang, false, mTransit);
        }
        public static List<MENUNHOM> GetAll(int LoaiNhomID, bool IsBanHang, bool IsVisual, Transit mTransit)
        {
            System.Linq.IOrderedQueryable<MENUNHOM> lsArray = mTransit.KaraokeEntities.MENUNHOMs;
            if (LoaiNhomID > 0)
                lsArray = lsArray.Where(s => s.LoaiNhomID == LoaiNhomID && s.Deleted == false).OrderBy(s => s.SapXep);
            if (IsBanHang)
                lsArray = lsArray.Where(s => s.Visual == true && s.SoLuongMon > 0).OrderBy(s => s.SapXep);
            if (IsVisual)
                lsArray = lsArray.Where(s => s.Visual == true).OrderBy(s => s.SapXep);
            return lsArray.ToList();

        }

        public static int Them(MENUNHOM item, Transit mTransit)
        {
            mTransit.KaraokeEntities.MENUNHOMs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.NhomID;
        }

        public static int Xoa(int NhomID, Transit mTransit)
        {
            MENUNHOM item = (from x in mTransit.KaraokeEntities.MENUNHOMs where x.NhomID == NhomID select x).First();
            item.Deleted = true;
            mTransit.KaraokeEntities.SaveChanges();
            return item.NhomID;
        }

        public static int CapNhat(MENUNHOM item, Transit mTransit)
        {
            MENUNHOM m = (from x in mTransit.KaraokeEntities.MENUNHOMs where x.NhomID == item.NhomID select x).First();
            m.SapXep = item.SapXep;
            m.TenDai = item.TenDai;
            m.TenNgan = item.TenNgan;
            m.Hinh = item.Hinh;
            mTransit.KaraokeEntities.SaveChanges();
            return item.NhomID;
        }
    }
}
