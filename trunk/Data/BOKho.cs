using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOKho
    {
        FrameworkRepository<KHO> frmKho = null;
        public BOKho(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmKho = new FrameworkRepository<KHO>(transit.KaraokeEntities, transit.KaraokeEntities.KHOes);
        }

        public IQueryable<KHO> GetAll(Transit mTransit)
        {
            return frmKho.Query().Where(s => s.Deleted == false);
        }
        public static IQueryable<KHO> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<KHO>.QueryNoTracking(mTransit.KaraokeEntities.KHOes).Where(s => s.Deleted == false);
        }

        private int Them(KHO item, Transit mTransit)
        {
            frmKho.AddObject(item);
            return item.KhoID;
        }

        private int Xoa(KHO item, Transit mTransit)
        {
            item.Deleted = true;
            frmKho.Update(item);
            return item.KhoID;
        }

        private int Sua(KHO item, Transit mTransit)
        {
            item.Deleted = true;
            frmKho.Update(item);
            return item.KhoID;
        }

        public void Luu(List<KHO> lsArray, List<KHO> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (KHO item in lsArray)
                {
                    if (item.KhoID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (KHO item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmKho.Commit();
        }
    }
}
