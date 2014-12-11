using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiLichBieu
    {        

        public static IQueryable<LOAILICHBIEU> GetQueryNoTracking(int TheLoaiID, KaraokeEntities karaokeEntities)
        {
            return karaokeEntities.LOAILICHBIEUx.Where(s => s.TheLoaiID == TheLoaiID);
        }
    }
}
