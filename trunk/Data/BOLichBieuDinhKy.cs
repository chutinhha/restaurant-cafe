using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichBieuDinhKy
    {
        public LICHBIEUDINHKY LichBieuDinhKy { get; set; }
        public MENULOAIGIA MenuLoaiGia { get; set; }
        FrameworkRepository<LICHBIEUDINHKY> frmLichBieuDinhKy = null;
        FrameworkRepository<MENULOAIGIA> frmMenuLoaiGia = null;
        public BOLichBieuDinhKy(Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmLichBieuDinhKy = new FrameworkRepository<LICHBIEUDINHKY>(transit.KaraokeEntities, transit.KaraokeEntities.LICHBIEUDINHKies);
            frmMenuLoaiGia = new FrameworkRepository<MENULOAIGIA>(transit.KaraokeEntities, transit.KaraokeEntities.MENULOAIGIAs);
        }
        public BOLichBieuDinhKy()
        {
            LichBieuDinhKy = new LICHBIEUDINHKY();
            MenuLoaiGia = new MENULOAIGIA();
        }

        public IQueryable<BOLichBieuDinhKy> GetAll(Transit mTransit)
        {
            var res = (from lb in frmLichBieuDinhKy.Query()
                       join l in frmMenuLoaiGia.Query() on lb.LoaiGiaID equals l.LoaiGiaID
                       where lb.LoaiGiaID == l.LoaiGiaID
                       orderby lb.UuTien ascending, l.Ten ascending, lb.TenLichBieu ascending
                       select new BOLichBieuDinhKy
                       {
                           LichBieuDinhKy = lb,
                           MenuLoaiGia = l
                       });
            return res;

        }

        public int Them(BOLichBieuDinhKy item, Transit mTransit)
        {
            frmLichBieuDinhKy.AddObject(item.LichBieuDinhKy);
            return item.LichBieuDinhKy.LichBieuDinhKyID;
        }

        public int Xoa(BOLichBieuDinhKy item, Transit mTransit)
        {
            item.LichBieuDinhKy.Deleted = true;
            frmLichBieuDinhKy.Update(item.LichBieuDinhKy);
            return item.LichBieuDinhKy.LichBieuDinhKyID;
        }

        public int Sua(BOLichBieuDinhKy item, Transit mTransit)
        {
            frmLichBieuDinhKy.Update(item.LichBieuDinhKy);
            return item.LichBieuDinhKy.LichBieuDinhKyID;
        }

        public void Luu(List<BOLichBieuDinhKy> lsArray, List<BOLichBieuDinhKy> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOLichBieuDinhKy item in lsArray)
                {
                    if (item.LichBieuDinhKy.LichBieuDinhKyID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOLichBieuDinhKy item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }

            frmLichBieuDinhKy.Commit();
        }
    }
}
