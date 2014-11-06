using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOKhu
    {
        public static List<KHU> GetAllVisual(Transit transit)
        {
            return transit.KaraokeEntities.KHUs.Where(k => k.Deleted == false && k.Visual == true).ToList();
        }
        public static List<KHU> GetAll(Transit transit)
        {
            return transit.KaraokeEntities.KHUs.Where(k => k.Deleted == false).ToList();
        }

        public static int CapNhatHinh(KHU khu, Transit mTransit)
        {
            KHU m = (from x in mTransit.KaraokeEntities.KHUs where x.KhuID == khu.KhuID select x).First();
            m.Hinh = khu.Hinh;
            mTransit.KaraokeEntities.SaveChanges();
            return m.KhuID;
        }

        public static int Them(KHU item, Transit mTransit)
        {
            mTransit.KaraokeEntities.KHUs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.KhuID;
        }

        public static int Xoa(int KhuID, Transit mTransit)
        {
            KHU item = (from x in mTransit.KaraokeEntities.KHUs where x.KhuID == KhuID select x).First();
            item.Deleted = true;
            mTransit.KaraokeEntities.SaveChanges();
            return item.KhuID;
        }

        public static int Sua(KHU item, Transit mTransit)
        {
            KHU m = (from x in mTransit.KaraokeEntities.KHUs where x.KhuID == item.KhuID select x).First();
            m.TenKhu = item.TenKhu;
            m.LoaiGiaID = item.LoaiGiaID;
            m.Hinh = item.Hinh;
            m.MacDinhSoDoBan = m.MacDinhSoDoBan;
            m.Visual = item.Visual;
            m.Deleted = item.Deleted;
            m.Edit = false;
            mTransit.KaraokeEntities.SaveChanges();
            return item.KhuID;
        }

        public static void Luu(List<KHU> lsArray, List<KHU> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (KHU item in lsArray)
                {
                    if (item.KhuID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (KHU item in lsArrayDeleted)
                {
                    Xoa(item.KhuID, mTransit);
                }
        }
    }
}
