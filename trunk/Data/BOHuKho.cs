using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOHuKho
    {

        public Data.HUKHO HuKho { get; set; }
        public Data.KHO Kho { get; set; }
        public Data.NHANVIEN NhanVien { get; set; }
        public List<Data.CHITIETHUKHO> ListChiTietHuKho { get; set; }

        FrameworkRepository<Data.KHO> frmKho = null;
        FrameworkRepository<Data.NHANVIEN> frmNhanVien = null;
        FrameworkRepository<Data.HUKHO> frmHuKho = null;

        public BOHuKho(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmKho = new FrameworkRepository<KHO>(transit.KaraokeEntities, transit.KaraokeEntities.KHOes);
            frmNhanVien = new FrameworkRepository<NHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.NHANVIENs);
            frmHuKho = new FrameworkRepository<HUKHO>(transit.KaraokeEntities, transit.KaraokeEntities.HUKHOes);
        }
        public BOHuKho()
        {
            Kho = new KHO();
            HuKho = new HUKHO();
            NhanVien = new NHANVIEN();
            ListChiTietHuKho = new List<CHITIETHUKHO>();
        }

        public IQueryable<BOHuKho> GetAll(Transit mTransit, DateTime dt)
        {
            frmHuKho.Refresh();
            return (from hk in frmHuKho.Query()
                    join k in frmKho.Query() on hk.KhoID equals k.KhoID
                    join nv in frmNhanVien.Query() on hk.NhanVienID equals nv.NhanVienID
                    select new BOHuKho
                    {
                        HuKho = hk,
                        Kho = k,
                        NhanVien = nv
                    }
                        );

        }

        public int Them(BOHuKho item, List<BOChiTietHuKho> lsArray, Transit mTransit)
        {
            ThemMoi(item, lsArray, mTransit);
            frmHuKho.AddObject(item.HuKho);
            frmHuKho.Commit();
            return item.HuKho.HuKhoID;
        }

        private int ThemMoi(BOHuKho item, List<BOChiTietHuKho> lsArray, Transit mTransit)
        {
            if (lsArray != null)
            {
                foreach (BOChiTietHuKho line in lsArray)
                {

                }
            }
            return item.HuKho.HuKhoID;
        }
        private int Them(BOHuKho item, Transit mTransit)
        {
            frmHuKho.AddObject(item.HuKho);
            return item.HuKho.HuKhoID;
        }

        private int Xoa(BOHuKho item, Transit mTransit)
        {
            item.HuKho.Deleted = true;
            frmHuKho.Update(item.HuKho);
            return item.HuKho.HuKhoID;
        }

        private int Sua(BOHuKho item, Transit mTransit)
        {
            item.HuKho.Edit = false;
            frmHuKho.Update(item.HuKho);
            return item.HuKho.HuKhoID;
        }

        public void Luu(List<BOHuKho> lsArray, List<BOHuKho> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOHuKho item in lsArray)
                {
                    if (item.HuKho.HuKhoID > 0)
                        Sua(item, mTransit);
                    else
                        ThemMoi(item, null, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOHuKho item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            mTransit.KaraokeEntities.SaveChanges();
        }
    }
}
