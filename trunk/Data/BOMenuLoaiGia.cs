using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMenuLoaiGia
    {
        FrameworkRepository<MENULOAIGIA> frmLoaiGia = null;      
        private Transit mTransit;
        public BOMenuLoaiGia(Data.Transit transit)
        {
            mTransit = transit;                        
            frmLoaiGia = new FrameworkRepository<MENULOAIGIA>(transit.KaraokeEntities, transit.KaraokeEntities.MENULOAIGIAs);
        }
        public static IQueryable<MENULOAIGIA> GetAllLoaiGiaRun(Transit transit)
        {
            var lichBieuDinhKy = Data.BOLichBieuDinhKy.GetAllVisualRun(transit);
            var lichBieuKhongDinhKy = Data.BOLichBieuKhongDinhKy.GetAllVisualRun(transit);                        
            return (from a in lichBieuDinhKy select a.MenuLoaiGia).Union(from b in lichBieuKhongDinhKy select b.MenuLoaiGia).Distinct();            
        }
        public IQueryable<MENULOAIGIA> GetAllMenuLoaiGia()
        {
            return GetAll(mTransit);
        }
        public static IQueryable<MENULOAIGIA> GetAll(Transit transit)
        {
            return FrameworkRepository<MENULOAIGIA>.QueryNoTracking(transit.KaraokeEntities.MENULOAIGIAs).Where(o => o.Deleted==false);
        }
        public static IQueryable<MENULOAIGIA> GetAllVisual(Transit transit)
        {
            return FrameworkRepository<MENULOAIGIA>.QueryNoTracking(transit.KaraokeEntities.MENULOAIGIAs).Where(o => o.Deleted == false && o.Visual==true);
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
