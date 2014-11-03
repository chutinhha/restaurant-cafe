using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLoaiNhanVien
    {
        public static List<LOAINHANVIEN> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.LOAINHANVIENs.ToList();
        }

        public static int Them(LOAINHANVIEN item, Transit mTransit)
        {
            mTransit.KaraokeEntities.LOAINHANVIENs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.LoaiNhanVienID;
        }

        public static int Xoa(int LoaiNhanVienID, Transit mTransit)
        {
            LOAINHANVIEN item = (from x in mTransit.KaraokeEntities.LOAINHANVIENs where x.LoaiNhanVienID == LoaiNhanVienID select x).First();
            mTransit.KaraokeEntities.LOAINHANVIENs.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.LoaiNhanVienID;
        }

        public static int CapNhat(LOAINHANVIEN item, Transit mTransit)
        {
            LOAINHANVIEN m = (from x in mTransit.KaraokeEntities.LOAINHANVIENs where x.LoaiNhanVienID == item.LoaiNhanVienID select x).First();
            m.TenLoaiNhanVien = item.TenLoaiNhanVien;
            m.LoaiNhanVienID = item.LoaiNhanVienID;
            m.Visual = item.Visual;
            m.Edit = false;
            mTransit.KaraokeEntities.SaveChanges();
            return item.LoaiNhanVienID;
        }
    }
}
