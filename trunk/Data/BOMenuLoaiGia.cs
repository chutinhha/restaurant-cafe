using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuLoaiGia
    {
        public static List<MENULOAIGIA> GetAll(Transit mTransit)
        {
            FrameworkRepository<MENULOAIGIA> frm = new FrameworkRepository<MENULOAIGIA>(mTransit.KaraokeEntities);
            return frm.Query().ToList();
        }
        public static List<MENULOAIGIA> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<MENULOAIGIA>.QueryNoTracking(mTransit.KaraokeEntities.MENULOAIGIAs).ToList();
        }

        private static int Them(MENULOAIGIA item, Transit mTransit, FrameworkRepository<MENULOAIGIA> frm)
        {
            frm.AddObject(item);
            return item.LoaiGiaID;
        }

        private static int Xoa(MENULOAIGIA item, Transit mTransit, FrameworkRepository<MENULOAIGIA> frm)
        {
            frm.DeleteObject(item);
            return item.LoaiGiaID;
        }

        private static int Sua(MENULOAIGIA item, Transit mTransit, FrameworkRepository<MENULOAIGIA> frm)
        {
            frm.Update(item);
            return item.LoaiGiaID;
        }

        public static void Luu(List<MENULOAIGIA> lsArray, List<MENULOAIGIA> lsArrayDeleted, Transit mTransit)
        {
            FrameworkRepository<MENULOAIGIA> frm = new FrameworkRepository<MENULOAIGIA>(mTransit.KaraokeEntities);
            if (lsArray != null)
                foreach (MENULOAIGIA item in lsArray)
                {
                    if (item.LoaiGiaID > 0)
                        Sua(item, mTransit, frm);
                    else
                        Them(item, mTransit, frm);
                }
            if (lsArrayDeleted != null)
                foreach (MENULOAIGIA item in lsArrayDeleted)
                {
                    Xoa(item, mTransit, frm);
                }
            frm.Commit();
        }
    }
}
