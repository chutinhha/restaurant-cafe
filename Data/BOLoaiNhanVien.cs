using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiNhanVien
    {
        public static List<LOAINHANVIEN> GetAll()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                return ke.LOAINHANVIENs.ToList();
            }
        }

        public static int Them(LOAINHANVIEN item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.LOAINHANVIENs.AddObject(item);
                ke.SaveChanges();
                return item.LoaiNhanVienID;
            }
        }

        public static int Xoa(int LoaiNhanVienID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                LOAINHANVIEN item = (from x in ke.LOAINHANVIENs where x.LoaiNhanVienID == LoaiNhanVienID select x).First();
                ke.LOAINHANVIENs.DeleteObject(item);
                ke.SaveChanges();
                return item.LoaiNhanVienID;
            }
        }

        public static int CapNhat(LOAINHANVIEN item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                LOAINHANVIEN m = (from x in ke.LOAINHANVIENs where x.LoaiNhanVienID == item.LoaiNhanVienID select x).First();
                m.TenLoaiNhanVien = item.TenLoaiNhanVien;
                ke.SaveChanges();
                return item.LoaiNhanVienID;
            }
        }
    }
}
