using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiKhachHang
    {

        FrameworkRepository<LOAIKHACHHANG> frmLoaiKhachHang = null;
        public BOLoaiKhachHang(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmLoaiKhachHang = new FrameworkRepository<LOAIKHACHHANG>(transit.KaraokeEntities, transit.KaraokeEntities.LOAIKHACHHANGs);
        }

        public IQueryable<LOAIKHACHHANG> GetAll(Transit mTransit)
        {
            return frmLoaiKhachHang.Query().Where(s => s.Deleted == false);
        }
        public static IQueryable<LOAIKHACHHANG> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<LOAIKHACHHANG>.QueryNoTracking(mTransit.KaraokeEntities.LOAIKHACHHANGs).Where(s => s.Deleted == false);
        }

        private int Them(LOAIKHACHHANG item, Transit mTransit)
        {
            frmLoaiKhachHang.AddObject(item);
            return item.LoaiKhachHangID;
        }

        private int Xoa(LOAIKHACHHANG item, Transit mTransit)
        {
            item.Deleted = true;
            frmLoaiKhachHang.Update(item);
            return item.LoaiKhachHangID;
        }

        private int Sua(LOAIKHACHHANG item, Transit mTransit)
        {
            item.Edit = true;
            frmLoaiKhachHang.Update(item);
            return item.LoaiKhachHangID;
        }

        public void Luu(List<LOAIKHACHHANG> lsArray, List<LOAIKHACHHANG> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (LOAIKHACHHANG item in lsArray)
                {
                    if (item.LoaiKhachHangID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (LOAIKHACHHANG item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmLoaiKhachHang.Commit();
        }
    }
}
