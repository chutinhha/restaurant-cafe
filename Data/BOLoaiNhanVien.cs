using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiNhanVien
    {
        public static IQueryable<LOAINHANVIEN> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<LOAINHANVIEN>.QueryNoTracking(mTransit.KaraokeEntities.LOAINHANVIENs);
        }
    }
}
