using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOKho
    {
        public static List<KHO> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.KHOes.Where(s => s.Deleted == false).ToList();
        }

        public static int Them(KHO item, Transit mTransit)
        {
            mTransit.KaraokeEntities.KHOes.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.KhoID;
        }

        public static int Xoa(int KhoID, Transit mTransit)
        {
            KHO item = (from x in mTransit.KaraokeEntities.KHOes where x.KhoID == KhoID select x).First();
            mTransit.KaraokeEntities.KHOes.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.KhoID;
        }

        public static int Sua(KHO item, Transit mTransit)
        {
            KHO m = (from x in mTransit.KaraokeEntities.KHOes where x.KhoID == item.KhoID select x).First();
            m.TenKho = item.TenKho;
            m.Visual = item.Visual;
            m.Edit = false;
            mTransit.KaraokeEntities.SaveChanges();
            return item.KhoID;
        }

        public static void Luu(List<KHO> lsArray, List<KHO> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (KHO item in lsArray)
                {
                    if (item.KhoID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (KHO item in lsArrayDeleted)
                {
                    Xoa(item.KhoID, mTransit);
                }
        }
    }
}
