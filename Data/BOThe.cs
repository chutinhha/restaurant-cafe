using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class BOThe
    {
        private KaraokeEntities mKaraokeEntities = null;

        public BOThe()
        {
        }

        public BOThe(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }

        public string TenThe { get; set; }

        public int TheID { get; set; }

        public static IQueryable<THE> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<THE>.QueryNoTracking(mTransit.KaraokeEntities.THEs).Where(s => s.Deleted == false);
        }

        public static IQueryable<BOThe> GetAllVisual(Transit mTransit)
        {
            return from x in FrameworkRepository<THE>.QueryNoTracking(mTransit.KaraokeEntities.THEs)
                   where x.Visual == true && x.Deleted == false
                   select new BOThe
                   {
                       TheID = x.TheID,
                       TenThe = x.TenThe
                   };
        }

        public static IQueryable<THE> GetQueryNoTracking(KaraokeEntities karaokeEntities)
        {
            return karaokeEntities.THEs.Where(s => s.Deleted == false);
        }

        public IQueryable<THE> GetAll()
        {
            return mKaraokeEntities.THEs.Where(s => s.Deleted == false);
        }
        public void Luu(List<THE> lsArray)
        {
            foreach (THE item in lsArray)
            {
                if (item.TheID == 0)
                {
                    mKaraokeEntities.THEs.AddObject(item);
                }
            }
            mKaraokeEntities.SaveChanges();
        }

        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.THEs);
        }
    }
}