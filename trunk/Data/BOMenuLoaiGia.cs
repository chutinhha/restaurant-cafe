using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuLoaiGia
    {
        public static List<MENULOAIGIA> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.MENULOAIGIAs.Where(s => s.Deleted == false).OrderBy(s => s.Ten).ToList();
        }

        public static int Them(MENULOAIGIA item, Transit mTransit)
        {
            mTransit.KaraokeEntities.MENULOAIGIAs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.LoaiGiaID;
        }

        public static int Xoa(int LoaiGiaID, Transit mTransit)
        {
            MENULOAIGIA item = (from x in mTransit.KaraokeEntities.MENULOAIGIAs where x.LoaiGiaID == LoaiGiaID select x).First();
            item.Deleted = true;
            mTransit.KaraokeEntities.SaveChanges();
            return item.LoaiGiaID;
        }

        public static int Sua(MENULOAIGIA item, Transit mTransit)
        {
            MENULOAIGIA m = (from x in mTransit.KaraokeEntities.MENULOAIGIAs where x.LoaiGiaID == item.LoaiGiaID select x).First();
            m.DienGiai = item.DienGiai;
            m.Ten = item.Ten;
            m.Visual = item.Visual;
            m.Deleted = item.Deleted;
            mTransit.KaraokeEntities.SaveChanges();
            return item.LoaiGiaID;
        }

        public static void Luu(List<MENULOAIGIA> lsArray, List<MENULOAIGIA> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (MENULOAIGIA item in lsArray)
                {
                    if (item.LoaiGiaID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (MENULOAIGIA item in lsArrayDeleted)
                {
                    Xoa(item.LoaiGiaID, mTransit);
                }
        }
    }
}
