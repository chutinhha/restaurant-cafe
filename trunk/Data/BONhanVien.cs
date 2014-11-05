using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BONhanVien
    {
        public static List<NHANVIEN> GetAll(Transit mTransit)
        {
            var res = (from n in mTransit.KaraokeEntities.NHANVIENs
                       join l in mTransit.KaraokeEntities.LOAINHANVIENs on n.LoaiNhanVienID equals l.LoaiNhanVienID
                       where n.Deleted == false
                       select new
                       {
                           NHANVIENs = n,
                           LOAINHANVIENs = l
                       }).ToList().Select(s => s.NHANVIENs);
            return res.ToList();
        }

        public static int Them(NHANVIEN item, Transit mTransit)
        {
            mTransit.KaraokeEntities.NHANVIENs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.NhanVienID;
        }

        public static int Xoa(int NhanVienID, Transit mTransit)
        {
            NHANVIEN item = (from x in mTransit.KaraokeEntities.NHANVIENs where x.NhanVienID == NhanVienID select x).First();
            item.Deleted = true;
            mTransit.KaraokeEntities.SaveChanges();
            return item.NhanVienID;
        }

        public static int Sua(NHANVIEN item, Transit mTransit)
        {
            NHANVIEN m = (from x in mTransit.KaraokeEntities.NHANVIENs where x.NhanVienID == item.NhanVienID select x).First();
            m.TenNhanVien = item.TenNhanVien;
            if (item.MatKhau != null)
                m.TenDangNhap = item.TenDangNhap;
            m.LoaiNhanVienID = item.LoaiNhanVienID;
            m.MatKhau = item.MatKhau;
            m.Visual = item.Visual;
            m.Deleted = item.Deleted;
            mTransit.KaraokeEntities.SaveChanges();
            return item.NhanVienID;
        }

        public static void Luu(List<NHANVIEN> lsArray, List<NHANVIEN> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (NHANVIEN item in lsArray)
                {
                    if (item.NhanVienID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (NHANVIEN item in lsArrayDeleted)
                {
                    Xoa(item.NhanVienID, mTransit);
                }
        }
    }
}
