using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiNhanVien
    {
        public static List<LOAINHANVIEN> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<LOAINHANVIEN>.QueryNoTracking(mTransit.KaraokeEntities.LOAINHANVIENs).ToList();
        }        

        private static int Them(LOAINHANVIEN item, Transit mTransit, FrameworkRepository<LOAINHANVIEN> frm)
        {
            frm.AddObject(item);
            return item.LoaiNhanVienID;
        }

        private static int Xoa(LOAINHANVIEN item, Transit mTransit, FrameworkRepository<LOAINHANVIEN> frm)
        {
            frm.DeleteObject(item);
            return item.LoaiNhanVienID;
        }

        private static int Sua(LOAINHANVIEN item, Transit mTransit, FrameworkRepository<LOAINHANVIEN> frm)
        {
            frm.Update(item);
            return item.LoaiNhanVienID;
        }
    }
}
