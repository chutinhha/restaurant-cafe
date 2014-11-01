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
            return mTransit.KaraokeEntities.LOAIKHACHHANGs.Where(s => s.Deleted == false).ToList();
        }

        public static int Them(LOAIKHACHHANG item, Transit mTransit)
        {
            mTransit.KaraokeEntities.LOAIKHACHHANGs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.LoaiKhachHangID;
        }

        public static int Xoa(int LoaiKhachHangID, Transit mTransit)
        {
            LOAIKHACHHANG item = (from x in mTransit.KaraokeEntities.LOAIKHACHHANGs where x.LoaiKhachHangID == LoaiKhachHangID select x).First();
            mTransit.KaraokeEntities.LOAIKHACHHANGs.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.LoaiKhachHangID;
        }

        public static int Sua(LOAIKHACHHANG item, Transit mTransit)
        {
            LOAIKHACHHANG m = (from x in mTransit.KaraokeEntities.LOAIKHACHHANGs where x.LoaiKhachHangID == item.LoaiKhachHangID select x).First();
            m.TenLoaiKhachHang = item.TenLoaiKhachHang;
            m.PhanTramGiamGia = item.PhanTramGiamGia;
            mTransit.KaraokeEntities.SaveChanges();
            return item.LoaiKhachHangID;
        }
    }
}
