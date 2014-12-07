using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoLichSuBanHang
    {
        public static IQueryable<BAOCAOLICHSUBANHANG> GetNoTracking(Transit transit, DateTime dt)
        {
            return from x in FrameworkRepository<BAOCAOLICHSUBANHANG>.QueryNoTracking(transit.KaraokeEntities.BAOCAOLICHSUBANHANGs)
                   where x.NgayBan.Value.Year == dt.Year && x.NgayBan.Value.Month == dt.Month && x.NgayBan.Value.Day == dt.Day
                   select x;
        }
    }
}
