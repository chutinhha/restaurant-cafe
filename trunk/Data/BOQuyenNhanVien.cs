using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOQuyenNhanVien
    {
        public static List<QUYENNHANVIEN> GetAll(int MaQuyen, Transit mTransit)
        {
            var res = (from mi in mTransit.KaraokeEntities.QUYENNHANVIENs
                       join m in mTransit.KaraokeEntities.QUYENs on mi.QuyenID equals m.MaQuyen
                       join i in mTransit.KaraokeEntities.NHANVIENs on mi.NhanVienID equals i.NhanVienID
                       where mi.Deleted == false && mi.Deleted == false && mi.QuyenID == MaQuyen
                       select new
                       {
                           QUYENNHANVIENs = mi,
                           QUYENs = m,
                           NHANVIENs = i
                       }).ToList().Select(s => s.QUYENNHANVIENs);
            return res.ToList();
        }

        public static int Them(QUYENNHANVIEN item, Transit mTransit)
        {
            mTransit.KaraokeEntities.QUYENNHANVIENs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return (int)item.QuyenID;
        }

        public static int Xoa(int QuyenID, int NhanVienID, Transit mTransit)
        {
            QUYENNHANVIEN item = (from x in mTransit.KaraokeEntities.QUYENNHANVIENs where x.QuyenID == QuyenID && x.NhanVienID == NhanVienID select x).First();
            mTransit.KaraokeEntities.QUYENNHANVIENs.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return (int)item.QuyenID;
        }


        public static void Luu(List<QUYENNHANVIEN> lsArray, Transit mTransit)
        {
            foreach (QUYENNHANVIEN item in lsArray)
            {
                if (item.Deleted == true)
                {
                    Xoa((int)item.QuyenID, (int)item.NhanVienID, mTransit);
                }
                else
                {
                    int count = (from x in mTransit.KaraokeEntities.QUYENNHANVIENs where x.QuyenID == item.QuyenID && x.NhanVienID == item.NhanVienID select x).Count();
                    if (count == 0)
                    {
                        Them(item, mTransit);
                    }
                }
            }
        }
    }
}
