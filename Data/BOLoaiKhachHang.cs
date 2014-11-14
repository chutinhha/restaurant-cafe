using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiKhachHang
    {
        public static List<LOAIKHACHHANG> GetAll(Transit mTransit)
        {
            FrameworkRepository<LOAIKHACHHANG> frm = new FrameworkRepository<LOAIKHACHHANG>(mTransit.KaraokeEntities);
            return frm.Query().ToList();
        }
        public static List<LOAIKHACHHANG> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<LOAIKHACHHANG>.QueryNoTracking(mTransit.KaraokeEntities.LOAIKHACHHANGs).ToList();
        }

        private static int Them(LOAIKHACHHANG item, Transit mTransit, FrameworkRepository<LOAIKHACHHANG> frm)
        {
            frm.AddObject(item);
            return item.LoaiKhachHangID;
        }

        private static int Xoa(LOAIKHACHHANG item, Transit mTransit, FrameworkRepository<LOAIKHACHHANG> frm)
        {
            frm.DeleteObject(item);
            return item.LoaiKhachHangID;
        }

        private static int Sua(LOAIKHACHHANG item, Transit mTransit, FrameworkRepository<LOAIKHACHHANG> frm)
        {
            frm.Update(item);
            return item.LoaiKhachHangID;
        }

        public static void Luu(List<LOAIKHACHHANG> lsArray, List<LOAIKHACHHANG> lsArrayDeleted, Transit mTransit)
        {
            FrameworkRepository<LOAIKHACHHANG> frm = new FrameworkRepository<LOAIKHACHHANG>(mTransit.KaraokeEntities);
            if (lsArray != null)
                foreach (LOAIKHACHHANG item in lsArray)
                {
                    if (item.LoaiKhachHangID > 0)
                        Sua(item, mTransit, frm);
                    else
                        Them(item, mTransit, frm);
                }
            if (lsArrayDeleted != null)
                foreach (LOAIKHACHHANG item in lsArrayDeleted)
                {
                    Xoa(item, mTransit, frm);
                }
            frm.Commit();
        }
    }
}
