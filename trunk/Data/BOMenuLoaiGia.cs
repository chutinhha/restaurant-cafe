using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuLoaiGia
    {
        FrameworkRepository<MENULOAIGIA> frmLoaiGia = null;
        public BOMenuLoaiGia(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmLoaiGia = new FrameworkRepository<MENULOAIGIA>(transit.KaraokeEntities, transit.KaraokeEntities.MENULOAIGIAs);
        }
        public IQueryable<MENULOAIGIA> GetAll(Transit mTransit)
        {
            return frmLoaiGia.Query().Where(s => s.Deleted == false);
        }
        public static IQueryable<MENULOAIGIA> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<MENULOAIGIA>.QueryNoTracking(mTransit.KaraokeEntities.MENULOAIGIAs);
        }

        private int Them(MENULOAIGIA item, Transit mTransit)
        {
            frmLoaiGia.AddObject(item);
            return item.LoaiGiaID;
        }

        private int Xoa(MENULOAIGIA item, Transit mTransit)
        {
            item.Deleted = true;
            frmLoaiGia.Update(item);
            return item.LoaiGiaID;
        }

        private int Sua(MENULOAIGIA item, Transit mTransit)
        {
            frmLoaiGia.Update(item);
            return item.LoaiGiaID;
        }

        public void Luu(List<MENULOAIGIA> lsArray, List<MENULOAIGIA> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (MENULOAIGIA item in lsArray)
                {
                    if (item.LoaiGiaID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (MENULOAIGIA item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmLoaiGia.Commit();
        }
    }
}
