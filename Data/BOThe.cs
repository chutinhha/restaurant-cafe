using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOThe
    {
        public static List<THE> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.THEs.Where(s => s.Deleted == false).ToList();
        }

        public static int Them(THE item, Transit mTransit)
        {
            mTransit.KaraokeEntities.THEs.AddObject(item);
            return item.TheID;
        }

        public static int Xoa(int TheID, Transit mTransit)
        {
            THE item = (from x in mTransit.KaraokeEntities.THEs where x.TheID == TheID select x).First();
            mTransit.KaraokeEntities.THEs.Attach(item);
            mTransit.KaraokeEntities.THEs.DeleteObject(item);
            return item.TheID;
        }

        public static int Sua(THE item, Transit mTransit)
        {
            THE m = (from x in mTransit.KaraokeEntities.THEs where x.TheID == item.TheID select x).First();
            mTransit.KaraokeEntities.THEs.Attach(m);
            m.TenThe = item.TenThe;
            m.ChietKhau = item.ChietKhau;
            m.Visual = item.Visual;
            m.Edit = false;
            return item.TheID;
        }

        public static void Luu(List<THE> lsArray, List<THE> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (THE item in lsArray)
                {
                    if (item.TheID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (THE item in lsArrayDeleted)
                {
                    Xoa(item.TheID, mTransit);
                }
            mTransit.KaraokeEntities.SaveChanges();
        }
    }
}
