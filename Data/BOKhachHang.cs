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

        public BOKhachHang(Data.Transit transit)
        {
            mTransit = transit;
            frmKhachHang = new FrameworkRepository<KHACHHANG>(transit.KaraokeEntities, transit.KaraokeEntities.KHACHHANGs);
            frmLoaiKhachHang = new FrameworkRepository<LOAIKHACHHANG>(transit.KaraokeEntities, transit.KaraokeEntities.LOAIKHACHHANGs);
        }

        public BOKhachHang()
        {
            KhachHang = new KHACHHANG();
            LoaiKhachHang = new LOAIKHACHHANG();
        }
        public IQueryable<BOKhachHang> TimKhachHang(string ten,string dienthoai)
        {
            if (ten!="" && dienthoai=="")
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
            if (ten=="" && dienthoai!="")
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
        public IQueryable<BOKhachHang> GetAll()
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

        public void Luu(List<BOKhachHang> lsArray, List<BOKhachHang> lsArrayDeleted)
        {
            if (lsArray != null)
                foreach (BOKhachHang item in lsArray)
                {
                    if (item.KhachHang.KhachHangID > 0)
                        Sua(item);
                    else
                        Them(item);
                }
            if (lsArrayDeleted != null)
                foreach (BOKhachHang item in lsArrayDeleted)
                {
                    Xoa(item);
                }
            frmKhachHang.Commit();
        }

        public int Sua(BOKhachHang item)
        {
            frmKhachHang.Update(item.KhachHang);
            return item.KhachHang.KhachHangID;
        }        
        public int Them(BOKhachHang item)
        {
            frmKhachHang.AddObject(item.KhachHang);
            return item.KhachHang.KhachHangID;
        }

        public int Xoa(BOKhachHang item)
        {
            frmKhachHang.DeleteObject(item.KhachHang);
            return item.KhachHang.KhachHangID;
        }
        public void Commit()
        {
            frmKhachHang.Commit();
        }
    }
}