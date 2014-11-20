using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichSuDangNhap
    {
        public static IQueryable<BAOCAOLICHDANGNHAP> GetNoTracking(Transit transit)
        {
            return FrameworkRepository<BAOCAOLICHDANGNHAP>.QueryNoTracking(transit.KaraokeEntities.BAOCAOLICHDANGNHAPs);
        }
    }
}
