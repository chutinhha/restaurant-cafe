using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichSuDangNhap
    {
        public static List<LICHSUDANGNHAP> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.LICHSUDANGNHAPs.Where(s => s.Deleted == false).ToList();
        }

        public static int Them(LICHSUDANGNHAP item, Transit mTransit)
        {
            mTransit.KaraokeEntities.LICHSUDANGNHAPs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.ID;
        }
    }
}
