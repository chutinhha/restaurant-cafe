using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOQuyen
    {
        KaraokeEntities mKaraokeEntities = null;
        public BOQuyen(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }

        public static IQueryable<QUYEN> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<QUYEN>.QueryNoTracking(mTransit.KaraokeEntities.QUYENs).Where(s => s.Deleted == false);
        }

        public IQueryable<QUYEN> GetAll()
        {
            return mKaraokeEntities.QUYENs.Where(s => s.Deleted == false);
        }

        public static IQueryable<QUYEN> GetQueryNoTracking(KaraokeEntities karaokeEntities)
        {
            return karaokeEntities.QUYENs.Where(s => s.Deleted == false);
        }

        public void Luu(List<QUYEN> lsArray)
        {
            foreach (QUYEN item in lsArray)
            {
                if (item.MaQuyen == 0)
                {
                    mKaraokeEntities.QUYENs.AddObject(item);
                }

            }
            mKaraokeEntities.SaveChanges();
        }
        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.QUYENs);
        }
    }
}
