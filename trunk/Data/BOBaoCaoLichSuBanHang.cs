using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoLichSuBanHang
    {
        public static IQueryable<BAOCAOLICHSUBANHANG> GetNoTracking(Transit transit)
        {
            return FrameworkRepository<BAOCAOLICHSUBANHANG>.QueryNoTracking(transit.KaraokeEntities.BAOCAOLICHSUBANHANGs);
        }
    }
}
