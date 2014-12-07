using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoTonKho
    {
        public static IQueryable<BAOCAOTONKHO> GetQueryNoTracking(Transit mTransit)
        {
            return FrameworkRepository<BAOCAOTONKHO>.QueryNoTracking(mTransit.KaraokeEntities.BAOCAOTONKHOes).OrderBy(s => s.SoluongTon);
        }
    }
}
