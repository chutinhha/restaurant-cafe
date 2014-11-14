using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOKho
    {
        public static List<KHO> GetAll(Transit mTransit)
        {
            FrameworkRepository<KHO> frm = new FrameworkRepository<KHO>(mTransit.KaraokeEntities);
            return frm.Query().ToList();
        }
        public static List<KHO> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<KHO>.QueryNoTracking(mTransit.KaraokeEntities.KHOes).ToList();
        }

        private static int Them(KHO item, Transit mTransit, FrameworkRepository<KHO> frm)
        {
            frm.AddObject(item);
            return item.KhoID;
        }

        private static int Xoa(KHO item, Transit mTransit, FrameworkRepository<KHO> frm)
        {
            frm.DeleteObject(item);
            return item.KhoID;
        }

        private static int Sua(KHO item, Transit mTransit, FrameworkRepository<KHO> frm)
        {
            frm.Update(item);
            return item.KhoID;
        }

        public static void Luu(List<KHO> lsArray, List<KHO> lsArrayDeleted, Transit mTransit)
        {
            FrameworkRepository<KHO> frm = new FrameworkRepository<KHO>(mTransit.KaraokeEntities);
            if (lsArray != null)
                foreach (KHO item in lsArray)
                {
                    if (item.KhoID > 0)
                        Sua(item, mTransit, frm);
                    else
                        Them(item, mTransit, frm);
                }
            if (lsArrayDeleted != null)
                foreach (KHO item in lsArrayDeleted)
                {
                    Xoa(item, mTransit, frm);
                }
            frm.Commit();
        }
    }
}
