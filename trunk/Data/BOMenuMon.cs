using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuMon
    {
        public static List<MENUMON> GetAll(int GroupID, bool IsBanHang, Transit mTransit)
        {
            return GetAll(GroupID, IsBanHang, false, mTransit);
        }
        public static List<MENUMON> GetAll(int GroupID, bool IsBanHang, bool IsVisual, Transit mTransit)
        {
            System.Linq.IOrderedQueryable<MENUMON> lsArray = mTransit.KaraokeEntities.MENUMONs;
            if (GroupID > -1)
                lsArray = lsArray.Where(s => s.NhomID == GroupID && s.Deleted == false).OrderBy(s => s.SapXep);
            if (IsBanHang)
                lsArray = lsArray.Where(s => s.SoLuongKichThuocMon > 0).OrderBy(s => s.SapXep);
            if (IsVisual)
                lsArray = lsArray.Where(s => s.Visual == true).OrderBy(s => s.SapXep);
            return lsArray.OrderBy(s => s.SapXep).ToList();
        }

        public static int Them(MENUMON item, Transit mTransit)
        {
            mTransit.KaraokeEntities.MENUMONs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.MonID;
        }

        public static int Xoa(int MonID, Transit mTransit)
        {
            MENUMON item = (from x in mTransit.KaraokeEntities.MENUMONs where x.MonID == MonID select x).First();
            item.Deleted = true;
            mTransit.KaraokeEntities.SaveChanges();
            return item.MonID;
        }

        public static int CapNhat(MENUMON item, Transit mTransit)
        {

            MENUMON m = (from x in mTransit.KaraokeEntities.MENUMONs where x.MonID == item.MonID select x).First();
            m.SapXep = item.SapXep;
            m.TenDai = item.TenDai;
            m.TenNgan = item.TenNgan;
            m.Hinh = item.Hinh;
            mTransit.KaraokeEntities.SaveChanges();
            return item.MonID;

        }
    }
}
