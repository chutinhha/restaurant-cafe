using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class BOKhachHang
    {
        private FrameworkRepository<KHACHHANG> frmKhachHang = null;

        private FrameworkRepository<LOAIKHACHHANG> frmLoaiKhachHang = null;

        public BOKhachHang(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmKhachHang = new FrameworkRepository<KHACHHANG>(transit.KaraokeEntities, transit.KaraokeEntities.KHACHHANGs);
            frmLoaiKhachHang = new FrameworkRepository<LOAIKHACHHANG>(transit.KaraokeEntities, transit.KaraokeEntities.LOAIKHACHHANGs);
        }

        public BOKhachHang()
        {
            KhachHang = new KHACHHANG();
            LoaiKhachHang = new LOAIKHACHHANG();
        }

        public KHACHHANG KhachHang { get; set; }

        public LOAIKHACHHANG LoaiKhachHang { get; set; }

        public IQueryable<BOKhachHang> GetAll(Transit mTransit)
        {
            return (from k in frmKhachHang.Query()
                    join l in frmLoaiKhachHang.Query() on k.LoaiKhachHangID equals l.LoaiKhachHangID
                    where k.Deleted == false
                    select new BOKhachHang
                    {
                        KhachHang = k,
                        LoaiKhachHang = l
                    });
        }

        public void Luu(List<BOKhachHang> lsArray, List<BOKhachHang> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOKhachHang item in lsArray)
                {
                    if (item.KhachHang.KhachHangID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOKhachHang item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmKhachHang.Commit();
        }

        private int Sua(BOKhachHang item, Transit mTransit)
        {
            frmKhachHang.Update(item.KhachHang);
            return item.KhachHang.KhachHangID;
        }

        private int Them(BOKhachHang item, Transit mTransit)
        {
            frmKhachHang.AddObject(item.KhachHang);
            return item.KhachHang.KhachHangID;
        }

        private int Xoa(BOKhachHang item, Transit mTransit)
        {
            frmKhachHang.DeleteObject(item.KhachHang);
            return item.KhachHang.KhachHangID;
        }
    }
}