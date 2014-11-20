using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BoChinhKho
    {
        public Data.CHINHKHO ChinhKho { get; set; }
        public Data.KHO Kho { get; set; }
        public Data.NHANVIEN NhanVien { get; set; }
        public List<BOChiTietChinhKho> ListChiTietChinhKho { get; set; }

        FrameworkRepository<Data.KHO> frmKho = null;
        FrameworkRepository<Data.NHANVIEN> frmNhanVien = null;
        FrameworkRepository<Data.CHINHKHO> frmChinhKho = null;

        public BoChinhKho(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmKho = new FrameworkRepository<KHO>(transit.KaraokeEntities, transit.KaraokeEntities.KHOes);
            frmNhanVien = new FrameworkRepository<NHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.NHANVIENs);
            frmChinhKho = new FrameworkRepository<CHINHKHO>(transit.KaraokeEntities, transit.KaraokeEntities.CHINHKHOes);
        }
        public BoChinhKho()
        {
            Kho = new KHO();
            ChinhKho = new CHINHKHO();
            NhanVien = new NHANVIEN();
            ListChiTietChinhKho = new List<BOChiTietChinhKho>();
        }

        public IQueryable<BoChinhKho> GetAll(Transit mTransit, DateTime dt)
        {
            frmChinhKho.Refresh();
            return (from ck in frmChinhKho.Query()
                    join k in frmKho.Query() on ck.KhoID equals k.KhoID
                    join nv in frmNhanVien.Query() on ck.NhanVienID equals nv.NhanVienID
                    select new BoChinhKho
                    {
                        ChinhKho = ck,
                        Kho = k,
                        NhanVien = nv
                    }
                        );

        }

        public int Them(BoChinhKho item, List<BOChiTietChinhKho> lsArray, Transit mTransit)
        {
            ThemMoi(item, lsArray, mTransit);
            frmChinhKho.AddObject(item.ChinhKho);
            frmChinhKho.Commit();
            return item.ChinhKho.ChinhKhoID;
        }

        private int ThemMoi(BoChinhKho item, List<BOChiTietChinhKho> lsArray, Transit mTransit)
        {
            if (lsArray != null)
            {
                foreach (BOChiTietChinhKho line in lsArray)
                {

                }
            }
            return item.ChinhKho.ChinhKhoID;
        }
        private int Them(BoChinhKho item, Transit mTransit)
        {
            frmChinhKho.AddObject(item.ChinhKho);
            return item.ChinhKho.ChinhKhoID;
        }

        private int Xoa(BoChinhKho item, Transit mTransit)
        {
            item.ChinhKho.Deleted = true;
            frmChinhKho.Update(item.ChinhKho);
            return item.ChinhKho.ChinhKhoID;
        }

        private int Sua(BoChinhKho item, Transit mTransit)
        {
            item.ChinhKho.Edit = false;
            frmChinhKho.Update(item.ChinhKho);
            return item.ChinhKho.ChinhKhoID;
        }

        public void Luu(List<BoChinhKho> lsArray, List<BoChinhKho> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BoChinhKho item in lsArray)
                {
                    if (item.ChinhKho.ChinhKhoID > 0)
                        Sua(item, mTransit);
                    else
                        ThemMoi(item, null, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BoChinhKho item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            mTransit.KaraokeEntities.SaveChanges();
        }
    }
}
