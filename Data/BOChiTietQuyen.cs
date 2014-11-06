using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChiTietQuyen
    {
        public static List<CHITIETQUYEN> GetAll(int MaQuyen, Transit mTransit)
        {
            var res = (from mi in mTransit.KaraokeEntities.CHITIETQUYENs
                       join m in mTransit.KaraokeEntities.QUYENs on mi.QuyenID equals m.MaQuyen
                       join i in mTransit.KaraokeEntities.CHUCNANGs on mi.ChucNangID equals i.ChucNangID
                       where mi.Deleted == false && mi.Deleted == false && mi.QuyenID == MaQuyen
                       select new
                       {
                           CHITIETQUYENs = mi,
                           QUYENs = m,
                           CHUCNANGs = i
                       }).ToList().Select(s => s.CHITIETQUYENs);
            return res.ToList();
        }

        public static int Them(CHITIETQUYEN item, Transit mTransit)
        {
            mTransit.KaraokeEntities.CHITIETQUYENs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return (int)item.QuyenID;
        }

        public static int Xoa(int QuyenID, int ChucNangID, Transit mTransit)
        {
            CHITIETQUYEN item = (from x in mTransit.KaraokeEntities.CHITIETQUYENs where x.QuyenID == QuyenID && x.ChucNangID == ChucNangID select x).First();
            mTransit.KaraokeEntities.CHITIETQUYENs.DeleteObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return (int)item.QuyenID;
        }

        public static int Sua(CHITIETQUYEN item, Transit mTransit)
        {
            CHITIETQUYEN m = (from x in mTransit.KaraokeEntities.CHITIETQUYENs where x.QuyenID == item.QuyenID && x.ChucNangID == item.ChucNangID select x).First();
            m.QuyenID = item.QuyenID;
            m.ChucNangID = item.ChucNangID;
            m.Xem = item.Xem;
            m.Them = item.Them;
            m.Xoa = item.Xoa;
            m.Sua = item.Sua;
            m.DangNhap = item.DangNhap;
            m.Deleted = item.Deleted;
            m.Visual = item.Visual;
            m.Edit = false;
            mTransit.KaraokeEntities.SaveChanges();
            return item.ChiTietQuyenID;
        }


        public static void Luu(List<CHITIETQUYEN> lsArray, Transit mTransit)
        {
            foreach (CHITIETQUYEN item in lsArray)
            {
                if (item.Deleted == true)
                {
                    Xoa((int)item.QuyenID, (int)item.ChucNangID, mTransit);
                }
                else
                {
                    int count = (from x in mTransit.KaraokeEntities.CHITIETQUYENs where x.QuyenID == item.QuyenID && x.ChucNangID == item.ChucNangID select x).Count();
                    if (count == 0)
                    {
                        Them(item, mTransit);
                    }
                    else
                    {
                        Sua(item, mTransit);
                    }
                }
            }
        }
    }
}
