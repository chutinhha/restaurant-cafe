using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChuyenKho
    {
        public Data.CHUYENKHO ChuyenKho { get; set; }
        public Data.KHO KhoDen { get; set; }
        public Data.KHO KhoDi { get; set; }
        public Data.NHANVIEN NhanVien { get; set; }
        public List<BOChiTietChuyenKho> ListChiTietHuKho { get; set; }

        FrameworkRepository<Data.KHO> frmKho = null;
        FrameworkRepository<Data.NHANVIEN> frmNhanVien = null;
        FrameworkRepository<Data.CHUYENKHO> frmChuyenKho = null;

        public BOChuyenKho(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmKho = new FrameworkRepository<KHO>(transit.KaraokeEntities, transit.KaraokeEntities.KHOes);
            frmNhanVien = new FrameworkRepository<NHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.NHANVIENs);
            frmChuyenKho = new FrameworkRepository<CHUYENKHO>(transit.KaraokeEntities, transit.KaraokeEntities.CHUYENKHOes);
        }
        public BOChuyenKho()
        {
            KhoDen = new KHO();
            KhoDi = new KHO();
            ChuyenKho = new CHUYENKHO();
            NhanVien = new NHANVIEN();
            ListChiTietHuKho = new List<BOChiTietChuyenKho>();
        }

        public IQueryable<BOChuyenKho> GetAll(Transit mTransit, DateTime dt)
        {
            frmChuyenKho.Refresh();
            return (from ck in frmChuyenKho.Query()
                    join ke in frmKho.Query() on ck.KhoDenID equals ke.KhoID
                    join ki in frmKho.Query() on ck.KhoDiID equals ki.KhoID
                    join nv in frmNhanVien.Query() on ck.NhanVienID equals nv.NhanVienID
                    where ck.NgayChuyen.Value.Year == dt.Year && ck.NgayChuyen.Value.Month == dt.Month && ck.NgayChuyen.Value.Day == dt.Day
                    select new BOChuyenKho
                    {
                        ChuyenKho = ck,
                        KhoDen = ke,
                        KhoDi = ki,
                        NhanVien = nv
                    }
                        );

        }

        public int Them(BOChuyenKho item, List<BOChiTietChuyenKho> lsArray, Transit mTransit)
        {            
            frmChuyenKho.AddObject(item.ChuyenKho);
            frmChuyenKho.Commit();
            return item.ChuyenKho.ChuyenKhoID;
        }

        

        private int Them(BOChuyenKho item, Transit mTransit)
        {
            frmChuyenKho.AddObject(item.ChuyenKho);
            return item.ChuyenKho.ChuyenKhoID;
        }

        private int Xoa(BOChuyenKho item, Transit mTransit)
        {
            item.ChuyenKho.Deleted = true;
            frmChuyenKho.Update(item.ChuyenKho);
            return item.ChuyenKho.ChuyenKhoID;
        }

        private int Sua(BOChuyenKho item, Transit mTransit)
        {
            item.ChuyenKho.Edit = false;
            frmChuyenKho.Update(item.ChuyenKho);
            return item.ChuyenKho.ChuyenKhoID;
        }

        public void Luu(List<BOChuyenKho> lsArray, List<BOChuyenKho> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOChuyenKho item in lsArray)
                {
                    if (item.ChuyenKho.ChuyenKhoID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, null, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOChuyenKho item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            mTransit.KaraokeEntities.SaveChanges();
        }
    }
}
