using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoDinhLuong
    {
        public static IQueryable<BAOCAODINHLUONG> GetQueryNoTracking(Transit mTransit)
        {
            return FrameworkRepository<BAOCAODINHLUONG>.QueryNoTracking(mTransit.KaraokeEntities.BAOCAODINHLUONGs).OrderBy(s => s.TenMonChinh);
        }
    }
}
