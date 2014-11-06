using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOInHoaDon
    {
        public static List<INHOADON> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.INHOADONs.Where(s => s.Deleted == false).ToList();
        }

        public static int Them(INHOADON item, Transit mTransit)
        {
            mTransit.KaraokeEntities.INHOADONs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.InHoaDonID;
        }
    }
}
