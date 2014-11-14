using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOQuyen
    {
        public static List<QUYEN> GetAll(Transit mTransit)
        {
            FrameworkRepository<QUYEN> frm = new FrameworkRepository<QUYEN>(mTransit.KaraokeEntities);
            return frm.Query().ToList();
        }
        public static List<QUYEN> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<QUYEN>.QueryNoTracking(mTransit.KaraokeEntities.QUYENs).ToList();
        }

        private static int Them(QUYEN item, Transit mTransit, FrameworkRepository<QUYEN> frm)
        {
            frm.AddObject(item);
            return item.MaQuyen;
        }

        private static int Xoa(QUYEN item, Transit mTransit, FrameworkRepository<QUYEN> frm)
        {
            frm.DeleteObject(item);
            return item.MaQuyen;
        }

        private static int Sua(QUYEN item, Transit mTransit, FrameworkRepository<QUYEN> frm)
        {
            frm.Update(item);
            return item.MaQuyen;
        }

        public static void Luu(List<QUYEN> lsArray, List<QUYEN> lsArrayDeleted, Transit mTransit)
        {
            FrameworkRepository<QUYEN> frm = new FrameworkRepository<QUYEN>(mTransit.KaraokeEntities);
            if (lsArray != null)
                foreach (QUYEN item in lsArray)
                {
                    if (item.MaQuyen > 0)
                        Sua(item, mTransit, frm);
                    else
                        Them(item, mTransit, frm);
                }
            if (lsArrayDeleted != null)
                foreach (QUYEN item in lsArrayDeleted)
                {
                    Xoa(item, mTransit, frm);
                }
            frm.Commit();
        }
    }
}
