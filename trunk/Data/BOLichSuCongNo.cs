using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichSuCongNo
    {
        public static List<LICHSUCONGNO> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.LICHSUCONGNOes.Where(s => s.Deleted == false).ToList();
        }

        public static int Them(LICHSUCONGNO item, Transit mTransit)
        {
            mTransit.KaraokeEntities.LICHSUCONGNOes.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.ID;
        }
    }
}
