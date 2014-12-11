using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuLoaiGia
    {
        private Transit mTransit;
        KaraokeEntities mKaraokeEntities = null;
        public BOMenuLoaiGia(Data.Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
        }
        public static IQueryable<MENULOAIGIA> GetAllLoaiGiaRun(Transit transit)
        {
            var lichBieuDinhKy = Data.BOLichBieuDinhKy.GetAllVisualRun(transit);
            var lichBieuKhongDinhKy = Data.BOLichBieuKhongDinhKy.GetAllVisualRun(transit);
            return (from a in lichBieuDinhKy select a.MenuLoaiGia).Union(from b in lichBieuKhongDinhKy select b.MenuLoaiGia).Distinct();
        }

        public static IQueryable<MENULOAIGIA> GetAll(Transit transit)
        {
            return FrameworkRepository<MENULOAIGIA>.QueryNoTracking(transit.KaraokeEntities.MENULOAIGIAs).Where(o => o.Deleted == false);
        }

        public static IQueryable<MENULOAIGIA> GetAllVisual(Transit transit)
        {
            return FrameworkRepository<MENULOAIGIA>.QueryNoTracking(transit.KaraokeEntities.MENULOAIGIAs).Where(o => o.Deleted == false && o.Visual == true);
        }

        public IQueryable<MENULOAIGIA> GetAll()
        {
            return mKaraokeEntities.MENULOAIGIAs.Where(s => s.Deleted == false);
        }

        public static IQueryable<MENULOAIGIA> GetQueryNoTracking(KaraokeEntities karaokeEntities)
        {
            return karaokeEntities.MENULOAIGIAs.Where(s => s.Deleted == false);
        }

        public void Luu(List<MENULOAIGIA> lsArray)
        {
            foreach (MENULOAIGIA item in lsArray)
            {
                if (item.LoaiGiaID == 0)
                {
                    mKaraokeEntities.MENULOAIGIAs.AddObject(item);
                }

            }
            mKaraokeEntities.SaveChanges();
        }
        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.MENULOAIGIAs);
        }
    }
}
