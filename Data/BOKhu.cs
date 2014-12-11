using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOKhu
    {
        private Transit mTransit;
        KaraokeEntities mKaraokeEntities = null;
        public BOKhu(Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
        }
        public BOKhu()
        {

        }

        public static IQueryable<KHU> GetAllVisual(Transit transit)
        {
            var query = FrameworkRepository<KHU>.QueryNoTracking(transit.KaraokeEntities.KHUs).Where(o => o.Deleted == false && o.Visual == true);
            return query;
        }
        public static IQueryable<KHU> GetAllNoTracking(Transit transit)
        {
            return FrameworkRepository<KHU>.QueryNoTracking(transit.KaraokeEntities.KHUs).Where(k => k.Deleted == false);
        }
        public static List<KHU> GetAllNoTrackingToList(Transit transit)
        {
            return FrameworkRepository<KHU>.QueryNoTracking(transit.KaraokeEntities.KHUs).Where(k => k.Deleted == false).ToList();
        }

        public IQueryable<KHU> GetAll()
        {
            return mKaraokeEntities.KHUs.Where(s => s.Deleted == false);
        }

        public static IQueryable<KHU> GetQueryNoTracking(KaraokeEntities karaokeEntities)
        {
            return karaokeEntities.KHUs.Where(s => s.Deleted == false);
        }

        public void Luu(List<KHU> lsArray)
        {
            foreach (KHU item in lsArray)
            {
                if (item.KhuID == 0)
                {
                    mKaraokeEntities.KHUs.AddObject(item);
                }

            }
            mKaraokeEntities.SaveChanges();
        }
        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.KHUs);
        }

    }
}
