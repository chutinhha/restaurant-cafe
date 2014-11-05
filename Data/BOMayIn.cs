using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMayIn
    {
        public static List<MAYIN> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.MAYINs.ToList();
        }

        public static List<MAYIN> GetAll(int[] IDs, Transit mTransit)
        {
            if (IDs != null)
                return mTransit.KaraokeEntities.MAYINs.Where(s => !IDs.Contains(s.MayInID)).ToList();
            else
                return mTransit.KaraokeEntities.MAYINs.ToList();
        }

        public static int Them(MAYIN item, Transit mTransit)
        {
            mTransit.KaraokeEntities.MAYINs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.MayInID;
        }

        public static int Xoa(int MayInID, Transit mTransit)
        {
            MAYIN item = (from x in mTransit.KaraokeEntities.MAYINs where x.MayInID == MayInID select x).First();
            mTransit.KaraokeEntities.MAYINs.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.MayInID;
        }

        public static int Sua(MAYIN item, Transit mTransit)
        {
            MAYIN m = (from x in mTransit.KaraokeEntities.MAYINs where x.MayInID == item.MayInID select x).First();
            m.TenMayIn = item.TenMayIn;
            m.TieuDeIn = item.TieuDeIn;            
            m.HopDungTien = item.HopDungTien;
            m.SoLanIn = item.SoLanIn;            
            m.Visual = item.Visual;
            m.Edit = false;
            mTransit.KaraokeEntities.SaveChanges();
            return item.MayInID;
        }

        public static void Luu(List<MAYIN> lsArray, List<MAYIN> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (MAYIN item in lsArray)
                {
                    if (item.MayInID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (MAYIN item in lsArrayDeleted)
                {
                    Xoa(item.MayInID, mTransit);
                }
        }
    }
}
