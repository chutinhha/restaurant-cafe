using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOMatKho
    {
        public Data.MATKHO MatKho { get; set; }
        public Data.KHO Kho { get; set; }
        public Data.NHANVIEN NhanVien { get; set; }
        public List<Data.CHITIETHUKHO> ListChiTietHuKho { get; set; }

        FrameworkRepository<Data.KHO> frmKho = null;
        FrameworkRepository<Data.NHANVIEN> frmNhanVien = null;
        FrameworkRepository<Data.MATKHO> frmMatKho = null;

        public BOMatKho(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmKho = new FrameworkRepository<KHO>(transit.KaraokeEntities, transit.KaraokeEntities.KHOes);
            frmNhanVien = new FrameworkRepository<NHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.NHANVIENs);
            frmMatKho = new FrameworkRepository<MATKHO>(transit.KaraokeEntities, transit.KaraokeEntities.MATKHOes);
        }
        public BOMatKho()
        {
            Kho = new KHO();
            MatKho = new MATKHO();
            NhanVien = new NHANVIEN();
            ListChiTietHuKho = new List<CHITIETHUKHO>();
        }

        public IQueryable<BOMatKho> GetAll(Transit mTransit, DateTime dt)
        {
            frmMatKho.Refresh();
            return (from hk in frmMatKho.Query()
                    join k in frmKho.Query() on hk.KhoID equals k.KhoID
                    join nv in frmNhanVien.Query() on hk.NhanVienID equals nv.NhanVienID
                    select new BOMatKho
                    {
                        MatKho = hk,
                        Kho = k,
                        NhanVien = nv
                    }
                        );

        }

        public int Them(BOMatKho item, List<BOChiTietHuKho> lsArray, Transit mTransit)
        {
            ThemMoi(item, lsArray, mTransit);
            frmMatKho.AddObject(item.MatKho);
            frmMatKho.Commit();
            return item.MatKho.MatKhoID;
        }

        private int ThemMoi(BOMatKho item, List<BOChiTietHuKho> lsArray, Transit mTransit)
        {
            if (lsArray != null)
            {
                foreach (BOChiTietHuKho line in lsArray)
                {

                }
            }
            return item.MatKho.MatKhoID;
        }
        private int Them(BOMatKho item, Transit mTransit)
        {
            frmMatKho.AddObject(item.MatKho);
            return item.MatKho.MatKhoID;
        }

        private int Xoa(BOMatKho item, Transit mTransit)
        {
            item.MatKho.Deleted = true;
            frmMatKho.Update(item.MatKho);
            return item.MatKho.MatKhoID;
        }

        private int Sua(BOMatKho item, Transit mTransit)
        {
            item.MatKho.Edit = false;
            frmMatKho.Update(item.MatKho);
            return item.MatKho.MatKhoID;
        }

        public void Luu(List<BOMatKho> lsArray, List<BOMatKho> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOMatKho item in lsArray)
                {
                    if (item.MatKho.MatKhoID > 0)
                        Sua(item, mTransit);
                    else
                        ThemMoi(item, null, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOMatKho item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            mTransit.KaraokeEntities.SaveChanges();
        }
    }
}
