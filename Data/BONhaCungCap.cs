using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BONhaCungCap
    {
        FrameworkRepository<NHACUNGCAP> frmNhaCungCap = null;
        public BONhaCungCap(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmNhaCungCap = new FrameworkRepository<NHACUNGCAP>(transit.KaraokeEntities, transit.KaraokeEntities.NHACUNGCAPs);
        }

        public IQueryable<NHACUNGCAP> GetAll(Transit mTransit)
        {
            return frmNhaCungCap.Query().Where(s => s.Deleted == false);
        }
        public static IQueryable<NHACUNGCAP> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<NHACUNGCAP>.QueryNoTracking(mTransit.KaraokeEntities.NHACUNGCAPs).Where(s => s.Deleted == false);
        }

        private int Them(NHACUNGCAP item, Transit mTransit)
        {
            frmNhaCungCap.AddObject(item);
            return item.NhaCungCapID;
        }

        private int Xoa(NHACUNGCAP item, Transit mTransit)
        {
            item.Deleted = true;
            frmNhaCungCap.Update(item);
            return item.NhaCungCapID;
        }

        private int Sua(NHACUNGCAP item, Transit mTransit)
        {
            item.Edit = false;
            frmNhaCungCap.Update(item);
            return item.NhaCungCapID;
        }

        public void Luu(List<NHACUNGCAP> lsArray, List<NHACUNGCAP> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (NHACUNGCAP item in lsArray)
                {
                    if (item.NhaCungCapID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (NHACUNGCAP item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmNhaCungCap.Commit();
        }
    }
}
