using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class BOMayIn
    {
        public int MayInID { get; set; }

        public string TenMayIn { get; set; }

        public string TieuDeIn { get; set; }

        public bool HocDungTien { get; set; }

        public int SoLanIn { get; set; }

        private KaraokeEntities mKaraokeEntities = null;
        private Transit mTransit;

        public BOMayIn()
        { }

        public BOMayIn(Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
        }
        
        public static IQueryable<MAYIN> GetAllNoTracking(Transit mTransit, bool IsMayInHoaDon)
        {
            return FrameworkRepository<MAYIN>.QueryNoTracking(mTransit.KaraokeEntities.MAYINs).Where(s => s.Deleted == false && s.MayInHoaDon == IsMayInHoaDon);
        }

        public IQueryable<MAYIN> GetAll()
        {
            return mKaraokeEntities.MAYINs.Where(s => s.Deleted == false);
        }

        public static IQueryable<MAYIN> GetQueryNoTracking(KaraokeEntities karaokeEntities)
        {
            return karaokeEntities.MAYINs.Where(s => s.Deleted == false);
        }

        public void Luu(List<MAYIN> lsArray)
        {
            foreach (MAYIN item in lsArray)
            {
                if (item.MayInID == 0)
                {
                    mKaraokeEntities.MAYINs.AddObject(item);
                }
            }
            mKaraokeEntities.SaveChanges();
        }

        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.KHACHHANGs);
        }
    }
}