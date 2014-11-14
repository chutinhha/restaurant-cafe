using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiLichBieu
    {
        public static IQueryable<LOAILICHBIEU> GetAll(int TheLoaiID, Transit mTransit)
        {
            return FrameworkRepository<LOAILICHBIEU>.QueryNoTracking(mTransit.KaraokeEntities.LOAILICHBIEUx).Where(s => s.TheLoaiID == TheLoaiID);
        }
    }
}
