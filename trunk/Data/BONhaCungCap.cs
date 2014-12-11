using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BONhaCungCap
    {
        KaraokeEntities mKaraokeEntities = null;
        public BONhaCungCap(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }

        public IQueryable<NHACUNGCAP> GetAll()
        {
            return mKaraokeEntities.NHACUNGCAPs.Where(s => s.Deleted == false);
        }
        public static IQueryable<NHACUNGCAP> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<NHACUNGCAP>.QueryNoTracking(mTransit.KaraokeEntities.NHACUNGCAPs).Where(s => s.Deleted == false);
        }

        public void Luu(List<NHACUNGCAP> lsArray)
        {
            foreach (NHACUNGCAP item in lsArray)
            {
                if (item.NhaCungCapID == 0)
                {
                    mKaraokeEntities.NHACUNGCAPs.AddObject(item);
                }

            }
            mKaraokeEntities.SaveChanges();
        }
        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.NHACUNGCAPs);
        }
    }
}
