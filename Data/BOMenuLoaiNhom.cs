using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuLoaiNhom
    {
        public static List<MENULOAINHOM> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.MENULOAINHOMs.ToList();
        }
        public static IQueryable<MENULOAINHOM> GetAll(KaraokeEntities kara)
        {
            return kara.MENULOAINHOMs.Where(s => s.Deleted == false);
        }

        private KaraokeEntities mKaraokeEntities = null;

        public BOMenuLoaiNhom()
        {
        }

        public BOMenuLoaiNhom(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }

        public static IQueryable<MENULOAINHOM> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<MENULOAINHOM>.QueryNoTracking(mTransit.KaraokeEntities.MENULOAINHOMs).Where(s => s.Deleted == false);
        }

        public static IQueryable<MENULOAINHOM> GetQueryNoTracking(KaraokeEntities karaokeEntities)
        {
            return karaokeEntities.MENULOAINHOMs;
        }

        public IQueryable<MENULOAINHOM> GetAll()
        {
            return mKaraokeEntities.MENULOAINHOMs.Where(s => s.Deleted == false);
        }
        public void Luu(List<MENULOAINHOM> lsArray)
        {
            foreach (MENULOAINHOM item in lsArray)
            {
                if (item.LoaiNhomID == 0)
                {
                    mKaraokeEntities.MENULOAINHOMs.AddObject(item);
                }
            }
            mKaraokeEntities.SaveChanges();
        }

        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.MENULOAINHOMs);
        }
    }
}
