using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOTheLoaiLichBieu
    {
        public static IQueryable<THELOAILICHBIEU> GetQueryNoTracking(KaraokeEntities karaokeEntities)
        {
            return karaokeEntities.THELOAILICHBIEUx;
        }
    }
}
