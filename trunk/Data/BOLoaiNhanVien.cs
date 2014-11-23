using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiNhanVien
    {
        public static IQueryable<LOAINHANVIEN> GetAllNoTracking(Transit mTransit, int CapDo)
        {
            return FrameworkRepository<LOAINHANVIEN>.QueryNoTracking(mTransit.KaraokeEntities.LOAINHANVIENs).Where(s => s.CapDo > CapDo).OrderBy(s => s.CapDo);
        }
    }
}
