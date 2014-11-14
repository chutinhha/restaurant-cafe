using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOTheLoaiLichBieu
    {
        public static List<THELOAILICHBIEU> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<THELOAILICHBIEU>.QueryNoTracking(mTransit.KaraokeEntities.THELOAILICHBIEUx).ToList();
        }
    }
}
