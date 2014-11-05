using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOQuyen
    {
        public static List<QUYEN> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.QUYENs.Where(s => s.Deleted == false).ToList();
        }

        public static int Them(QUYEN item, Transit mTransit)
        {
            mTransit.KaraokeEntities.QUYENs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.MaQuyen;
        }

        public static int Xoa(int MaQuyen, Transit mTransit)
        {
            QUYEN item = (from x in mTransit.KaraokeEntities.QUYENs where x.MaQuyen == MaQuyen select x).First();
            mTransit.KaraokeEntities.QUYENs.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.MaQuyen;
        }

        public static int Sua(QUYEN item, Transit mTransit)
        {
            QUYEN m = (from x in mTransit.KaraokeEntities.QUYENs where x.MaQuyen == item.MaQuyen select x).First();
            m.TenQuen = item.TenQuen;
            m.Visual = item.Visual;
            m.Edit = false;
            mTransit.KaraokeEntities.SaveChanges();
            return item.MaQuyen;
        }

        public static void Luu(List<QUYEN> lsArray, List<QUYEN> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (QUYEN item in lsArray)
                {
                    if (item.MaQuyen > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (QUYEN item in lsArrayDeleted)
                {
                    Xoa(item.MaQuyen, mTransit);
                }
        }
    }
}
