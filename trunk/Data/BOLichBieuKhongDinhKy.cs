using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichBieuKhongDinhKy
    {
        public LICHBIEUKHONGDINHKY LichBieuKhongDinhKy { get; set; }
        public MENULOAIGIA MenuLoaiGia { get; set; }
        FrameworkRepository<LICHBIEUKHONGDINHKY> frmLichBieuKhongDinhKy = null;
        FrameworkRepository<MENULOAIGIA> frmMenuLoaiGia = null;
        public BOLichBieuKhongDinhKy(Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmLichBieuKhongDinhKy = new FrameworkRepository<LICHBIEUKHONGDINHKY>(transit.KaraokeEntities, transit.KaraokeEntities.LICHBIEUKHONGDINHKies);
            frmMenuLoaiGia = new FrameworkRepository<MENULOAIGIA>(transit.KaraokeEntities, transit.KaraokeEntities.MENULOAIGIAs);
        }

        public BOLichBieuKhongDinhKy()
        {

        }

        public IQueryable<BOLichBieuKhongDinhKy> GetAll(Transit mTransit)
        {
            var res = (from lb in frmLichBieuKhongDinhKy.Query()
                       join l in frmMenuLoaiGia.Query() on lb.LoaiGiaID equals l.LoaiGiaID
                       where lb.LoaiGiaID == l.LoaiGiaID
                       orderby l.Ten ascending, lb.TenLichBieu ascending
                       select new BOLichBieuKhongDinhKy
                       {
                           LichBieuKhongDinhKy = lb,
                           MenuLoaiGia = l
                       });
            return res;
        }

        public int Them(BOLichBieuKhongDinhKy item, Transit mTransit)
        {
            frmLichBieuKhongDinhKy.AddObject(item.LichBieuKhongDinhKy);
            return item.LichBieuKhongDinhKy.LichBieuKhongDinhKyID;
        }

        public int Xoa(BOLichBieuKhongDinhKy item, Transit mTransit)
        {
            item.LichBieuKhongDinhKy.Deleted = true;
            frmLichBieuKhongDinhKy.Update(item.LichBieuKhongDinhKy);
            return item.LichBieuKhongDinhKy.LichBieuKhongDinhKyID;
        }

        public int Sua(BOLichBieuKhongDinhKy item, Transit mTransit)
        {
            item.LichBieuKhongDinhKy.Edit = false;
            frmLichBieuKhongDinhKy.Update(item.LichBieuKhongDinhKy);
            return item.LichBieuKhongDinhKy.LichBieuKhongDinhKyID;
        }

        public void Luu(List<BOLichBieuKhongDinhKy> lsArray, List<BOLichBieuKhongDinhKy> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOLichBieuKhongDinhKy item in lsArray)
                {
                    if (item.LichBieuKhongDinhKy.LichBieuKhongDinhKyID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOLichBieuKhongDinhKy item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }

            frmLichBieuKhongDinhKy.Commit();
        }


    }
}
