using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class BOKhachHang
    {
        public KHACHHANG KhachHang { get; set; }
        public LOAIKHACHHANG LoaiKhachHang { get; set; }
        private FrameworkRepository<KHACHHANG> frmKhachHang = null;
        private Transit mTransit;
        private FrameworkRepository<LOAIKHACHHANG> frmLoaiKhachHang = null;
        KaraokeEntities mKaraokeEntities = null;

        public BOKhachHang(Data.Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
            frmKhachHang = new FrameworkRepository<KHACHHANG>(transit.KaraokeEntities, transit.KaraokeEntities.KHACHHANGs);
            frmLoaiKhachHang = new FrameworkRepository<LOAIKHACHHANG>(transit.KaraokeEntities, transit.KaraokeEntities.LOAIKHACHHANGs);
        }

        public BOKhachHang()
        {
            KhachHang = new KHACHHANG();
            LoaiKhachHang = new LOAIKHACHHANG();
        }
        public IQueryable<BOKhachHang> TimKhachHang(string ten, string dienthoai)
        {
            if (ten != "" && dienthoai == "")
            {
                return from k in frmKhachHang.Query()
                       join l in frmLoaiKhachHang.Query() on k.LoaiKhachHangID equals l.LoaiKhachHangID
                       where k.TenKhachHang.Contains(ten)
                       select new BOKhachHang
                       {
                           KhachHang = k,
                           LoaiKhachHang = l
                       };
            }
            if (ten == "" && dienthoai != "")
            {
                return from k in frmKhachHang.Query()
                       join l in frmLoaiKhachHang.Query() on k.LoaiKhachHangID equals l.LoaiKhachHangID
                       where k.Mobile.Contains(dienthoai)
                       select new BOKhachHang
                       {
                           KhachHang = k,
                           LoaiKhachHang = l
                       };
            }
            return from k in frmKhachHang.Query()
                   join l in frmLoaiKhachHang.Query() on k.LoaiKhachHangID equals l.LoaiKhachHangID
                   where k.TenKhachHang.Contains(ten) || k.Mobile.Contains(dienthoai)
                   select new BOKhachHang
                    {
                        KhachHang = k,
                        LoaiKhachHang = l
                    };
        }

        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.KHACHHANGs);
        }

        public IQueryable<BOKhachHang> GetAll()
        {
            return from kh in mKaraokeEntities.KHACHHANGs
                   join lkh in mKaraokeEntities.LOAIKHACHHANGs on kh.LoaiKhachHangID equals lkh.LoaiKhachHangID
                   where kh.Deleted == false
                   select new BOKhachHang
                   {
                       KhachHang = kh,
                       LoaiKhachHang = lkh
                   };
        }

        public void Luu(List<BOKhachHang> lsArray)
        {
            foreach (BOKhachHang item in lsArray)
            {
                if (item.KhachHang.KhachHangID == 0)
                {
                    mKaraokeEntities.KHACHHANGs.AddObject(item.KhachHang);
                }

            }
            mKaraokeEntities.SaveChanges();
        }

        public void Commit()
        {
            frmKhachHang.Commit();
        }

        public IQueryable<LOAIKHACHHANG> GetLoaiKhachHang()
        {
            return BOLoaiKhachHang.GetQueryNoTracking(mKaraokeEntities);
        }
    }
}