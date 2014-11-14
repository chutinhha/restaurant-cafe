using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BONhaCungCap
    {
        public static List<NHACUNGCAP> GetAll(Transit mTransit)
        {
            FrameworkRepository<NHACUNGCAP> frm = new FrameworkRepository<NHACUNGCAP>(mTransit.KaraokeEntities);
            return frm.Query().ToList();
        }
        public static List<NHACUNGCAP> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<NHACUNGCAP>.QueryNoTracking(mTransit.KaraokeEntities.NHACUNGCAPs).ToList();
        }

        private static int Them(NHACUNGCAP item, Transit mTransit, FrameworkRepository<NHACUNGCAP> frm)
        {
            frm.AddObject(item);
            return item.NhaCungCapID;
        }

        private static int Xoa(NHACUNGCAP item, Transit mTransit, FrameworkRepository<NHACUNGCAP> frm)
        {
            frm.DeleteObject(item);
            return item.NhaCungCapID;
        }

        private static int Sua(NHACUNGCAP item, Transit mTransit, FrameworkRepository<NHACUNGCAP> frm)
        {
            frm.Update(item);
            return item.NhaCungCapID;
        }

        public static void Luu(List<NHACUNGCAP> lsArray, List<NHACUNGCAP> lsArrayDeleted, Transit mTransit)
        {
            FrameworkRepository<NHACUNGCAP> frm = new FrameworkRepository<NHACUNGCAP>(mTransit.KaraokeEntities);
            if (lsArray != null)
                foreach (NHACUNGCAP item in lsArray)
                {
                    if (item.NhaCungCapID > 0)
                        Sua(item, mTransit, frm);
                    else
                        Them(item, mTransit, frm);
                }
            if (lsArrayDeleted != null)
                foreach (NHACUNGCAP item in lsArrayDeleted)
                {
                    Xoa(item, mTransit, frm);
                }
            frm.Commit();
        }
    }
}
