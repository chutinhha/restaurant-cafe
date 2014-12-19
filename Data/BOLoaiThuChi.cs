using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiThuChi
    {
        public static IQueryable<LOAITHUCHI> GetAllNoTracking(KaraokeEntities karaokeEntities)
        {
            return karaokeEntities.LOAITHUCHIs;
        }
    }
}
