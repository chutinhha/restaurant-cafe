using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiNhanVien
    {
        public static IQueryable<LOAINHANVIEN> GetAllNoTracking(KaraokeEntities karaokeEntities, int CapDo)
        {
            return karaokeEntities.LOAINHANVIENs.Where(s => s.Deleted == false && s.CapDo > CapDo).OrderBy(s => s.CapDo);
        }
    }
}
