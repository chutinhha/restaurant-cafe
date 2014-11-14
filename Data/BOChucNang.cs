using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChucNang
    {
        public static IQueryable<CHUCNANG> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<CHUCNANG>.QueryNoTracking(mTransit.KaraokeEntities.CHUCNANGs).Where(s => s.Deleted == false);
        }

        public static List<CHUCNANG> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.CHUCNANGs.Where(s => s.Deleted == false).ToList();
        }

        public static int Them(CHUCNANG item, Transit mTransit)
        {
            mTransit.KaraokeEntities.CHUCNANGs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.ChucNangID;
        }
    }
}
