using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiKhachHang
    {
        public static List<LOAIKHACHHANG> GetAll()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                return ke.LOAIKHACHHANGs.Where(s => s.Deleted == false).ToList();
            }
        }

        public static int Them(LOAIKHACHHANG item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.LOAIKHACHHANGs.AddObject(item);
                ke.SaveChanges();
                return item.LoaiKhachHangID;
            }
        }

        public static int Xoa(int LoaiKhachHangID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                LOAIKHACHHANG item = (from x in ke.LOAIKHACHHANGs where x.LoaiKhachHangID == LoaiKhachHangID select x).First();
                ke.LOAIKHACHHANGs.DeleteObject(item);
                ke.SaveChanges();
                return item.LoaiKhachHangID;
            }
        }

        public static int Sua(LOAIKHACHHANG item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                LOAIKHACHHANG m = (from x in ke.LOAIKHACHHANGs where x.LoaiKhachHangID == item.LoaiKhachHangID select x).First();
                m.TenLoaiKhachHang = item.TenLoaiKhachHang;
                m.PhanTramGiamGia = item.PhanTramGiamGia;
                ke.SaveChanges();
                return item.LoaiKhachHangID;
            }
        }
    }
}
