using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOKhachHang
    {
        public KHACHHANG KhachHang { get; set; }
        public LOAIKHACHHANG LoaiKhachHang { get; set; }

        public BOKhachHang()
        {
            KhachHang = new KHACHHANG();
            LoaiKhachHang = new LOAIKHACHHANG();
        }

        public static List<BOKhachHang> GetAll(Transit mTransit)
        {
            FrameworkRepository<KHACHHANG> frmKhachHang = new FrameworkRepository<KHACHHANG>(mTransit.KaraokeEntities);
            FrameworkRepository<LOAIKHACHHANG> frmLoaiKhachHang = new FrameworkRepository<LOAIKHACHHANG>(mTransit.KaraokeEntities);

            return (from k in frmKhachHang.Query()
                    join l in frmLoaiKhachHang.Query() on k.LoaiKhachHangID equals l.LoaiKhachHangID
                    where k.Deleted == false
                    select new BOKhachHang
                    {
                        KhachHang = k,
                        LoaiKhachHang = l
                    }).ToList();
        }

        private static int Them(BOKhachHang item, Transit mTransit, FrameworkRepository<KHACHHANG> frm)
        {
            frm.AddObject(item.KhachHang);
            return item.KhachHang.KhachHangID;
        }

        private static int Xoa(BOKhachHang item, Transit mTransit, FrameworkRepository<KHACHHANG> frm)
        {
            frm.DeleteObject(item.KhachHang);
            return item.KhachHang.KhachHangID;
        }

        private static int Sua(BOKhachHang item, Transit mTransit, FrameworkRepository<KHACHHANG> frm)
        {
            frm.Update(item.KhachHang);
            return item.KhachHang.KhachHangID;
        }

        public static void Luu(List<BOKhachHang> lsArray, List<BOKhachHang> lsArrayDeleted, Transit mTransit)
        {
            FrameworkRepository<KHACHHANG> frm = new FrameworkRepository<KHACHHANG>(mTransit.KaraokeEntities);
            if (lsArray != null)
                foreach (BOKhachHang item in lsArray)
                {
                    if (item.KhachHang.KhachHangID > 0)
                        Sua(item, mTransit, frm);
                    else
                        Them(item, mTransit, frm);
                }
            if (lsArrayDeleted != null)
                foreach (BOKhachHang item in lsArrayDeleted)
                {
                    Xoa(item, mTransit, frm);
                }
            frm.Commit();
        }


    }
}
