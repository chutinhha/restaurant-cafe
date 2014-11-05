using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOKhachHang
    {
        public static List<KHACHHANG> GetAll(Transit mTransit)
        {
            var res = (from k in mTransit.KaraokeEntities.KHACHHANGs
                       join l in mTransit.KaraokeEntities.LOAIKHACHHANGs on k.LoaiKhachHangID equals l.LoaiKhachHangID
                       where k.Deleted == false && l.Deleted == false
                       orderby k.TenKhachHang ascending
                       select new
                       {
                           KHACHHANGs = k,
                           LOAIKHACHHANGs = l
                       }).ToList().Select(s => s.KHACHHANGs);
            return res.ToList();
        }

        public static int Them(KHACHHANG item, Transit mTransit)
        {
            mTransit.KaraokeEntities.KHACHHANGs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.KhachHangID;
        }

        public static int Xoa(int KhachHangID, Transit mTransit)
        {
            KHACHHANG item = (from x in mTransit.KaraokeEntities.KHACHHANGs where x.KhachHangID == KhachHangID select x).First();
            item.Deleted = true;
            mTransit.KaraokeEntities.SaveChanges();
            return item.KhachHangID;
        }

        public static int Sua(KHACHHANG item, Transit mTransit)
        {
            KHACHHANG m = (from x in mTransit.KaraokeEntities.KHACHHANGs where x.LoaiKhachHangID == item.LoaiKhachHangID select x).First();
            m.TenKhachHang = item.TenKhachHang;
            m.SoNha = item.SoNha;
            m.TenDuong = item.TenDuong;
            m.Mobile = item.Mobile;
            m.Phone = item.Phone;
            m.Fax = item.Fax;
            m.DuNo = item.DuNo;
            m.DuNoToiThieu = item.DuNoToiThieu;
            m.Email = item.Email;
            m.Visual = item.Visual;
            m.Deleted = item.Deleted;
            m.LoaiKhachHangID = item.LoaiKhachHangID;
            mTransit.KaraokeEntities.SaveChanges();
            return item.KhachHangID;
        }

        public static void Luu(List<KHACHHANG> lsArray, List<KHACHHANG> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (KHACHHANG item in lsArray)
                {
                    if (item.KhachHangID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (KHACHHANG item in lsArrayDeleted)
                {
                    Xoa(item.KhachHangID, mTransit);
                }
        }
    }
}
