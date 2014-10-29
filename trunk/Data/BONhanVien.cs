using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BONhanVien
    {
        public static List<NHANVIEN> GetAll()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                var res = (from n in ke.NHANVIENs
                           join l in ke.LOAINHANVIENs on n.LoaiNhanVienID equals l.LoaiNhanVienID
                           where n.Deleted == false
                           select new
                           {
                               NHANVIENs = n,
                               LOAINHANVIENs = l
                           }).ToList().Select(s => s.NHANVIENs);
                return res.ToList();
            }
        }

        public static int Them(NHANVIEN item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.NHANVIENs.AddObject(item);
                ke.SaveChanges();
                return item.NhanVienID;
            }
        }

        public static int Xoa(int NhanVienID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                NHANVIEN item = (from x in ke.NHANVIENs where x.NhanVienID == NhanVienID select x).First();
                item.Deleted = true;
                ke.SaveChanges();
                return item.NhanVienID;
            }
        }

        public static int CapNhat(NHANVIEN item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                NHANVIEN m = (from x in ke.NHANVIENs where x.NhanVienID == item.NhanVienID select x).First();
                m.TenNhanVien = item.TenNhanVien;
                if (item.MatKhau != null)
                    m.TenDangNhap = item.TenDangNhap;
                m.LoaiNhanVienID = item.LoaiNhanVienID;
                m.MatKhau = item.MatKhau;
                m.Visual = item.Visual;
                m.Deleted = item.Deleted;
                ke.SaveChanges();
                return item.NhanVienID;
            }
        }
    }
}
