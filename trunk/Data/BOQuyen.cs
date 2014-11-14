using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOQuyen
    {
        FrameworkRepository<QUYEN> frmLoaiKhachHang = null;
        public BOQuyen(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmLoaiKhachHang = new FrameworkRepository<QUYEN>(transit.KaraokeEntities, transit.KaraokeEntities.QUYENs);
        }

        public IQueryable<QUYEN> GetAll(Transit mTransit)
        {
            return frmLoaiKhachHang.Query().Where(s => s.Deleted == false);
        }
        public static IQueryable<QUYEN> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<QUYEN>.QueryNoTracking(mTransit.KaraokeEntities.QUYENs).Where(s => s.Deleted == false);
        }

        private int Them(QUYEN item, Transit mTransit)
        {
            frmLoaiKhachHang.AddObject(item);
            return item.MaQuyen;
        }

        private int Xoa(QUYEN item, Transit mTransit)
        {
            item.Deleted = true;
            frmLoaiKhachHang.Update(item);
            return item.MaQuyen;
        }

        private int Sua(QUYEN item, Transit mTransit)
        {
            item.Deleted = true;
            frmLoaiKhachHang.Update(item);
            return item.MaQuyen;
        }

        public void Luu(List<QUYEN> lsArray, List<QUYEN> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (QUYEN item in lsArray)
                {
                    if (item.MaQuyen > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (QUYEN item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmLoaiKhachHang.Commit();
        }
    }
}
