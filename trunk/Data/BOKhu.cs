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
            return transit.KaraokeEntities.KHUs.Where(k => k.Deleted == false).Where(k => k.Visual == true).ToList();
        }
        public static List<KHU> GetAll(Transit transit)
        {
            return transit.KaraokeEntities.KHUs.Where(k=>k.Deleted==false).ToList();
        }

        public static int CapNhatHinh(KHU khu, Transit mTransit)
        {
            KHU m = (from x in mTransit.KaraokeEntities.KHUs where x.KhuID == khu.KhuID select x).First();
            m.Hinh = khu.Hinh;
            mTransit.KaraokeEntities.SaveChanges();
            return m.KhuID;
        }
    }
}
