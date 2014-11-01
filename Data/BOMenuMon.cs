using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuMon
    {
        public static List<MENUMON> GetAll(int GroupID, Transit mTransit)
        {

            if (GroupID > -1)
                return mTransit.KaraokeEntities.MENUMONs.Where(s => s.NhomID == GroupID && s.Deleted == false).OrderBy(s => s.SapXep).ToList();
            else
                return mTransit.KaraokeEntities.MENUMONs.ToList();

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
