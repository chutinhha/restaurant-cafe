using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOKhachHang
    {
        public static List<KHACHHANG> GetAll()
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                var res = (from k in ke.KHACHHANGs
                           join l in ke.LOAIKHACHHANGs on k.LoaiKhachHangID equals l.LoaiKhachHangID
                           where k.Deleted == false && l.Deleted == false
                           orderby k.TenKhachHang ascending
                           select new
                           {
                               KHACHHANGs = k,
                               LOAIKHACHHANGs = l
                           }).ToList().Select(s => s.KHACHHANGs);
                return res.ToList();
            }
        }

        public static int Them(KHACHHANG item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                ke.KHACHHANGs.AddObject(item);
                ke.SaveChanges();
                return item.KhachHangID;
            }
        }

        public static int Xoa(int KhachHangID)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                KHACHHANG item = (from x in ke.KHACHHANGs where x.KhachHangID == KhachHangID select x).First();
                item.Deleted = true;
                ke.SaveChanges();
                return item.KhachHangID;
            }
        }

        public static int Sua(KHACHHANG item)
        {
            using (KaraokeEntities ke = new KaraokeEntities())
            {
                KHACHHANG m = (from x in ke.KHACHHANGs where x.LoaiKhachHangID == item.LoaiKhachHangID select x).First();
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
                ke.SaveChanges();
                return item.KhachHangID;
            }
        }
    }
}
