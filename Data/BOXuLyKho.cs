using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOXuLyKho
    {
        private FrameworkRepository<KHO> frmKho = null;

        private FrameworkRepository<XULYKHOLOAI> frmXuLyKhoLoai = null;

        private FrameworkRepository<NHANVIEN> frmNhanVien = null;

        private FrameworkRepository<XULYKHO> frmNhapKho = null;

        public BOXuLyKho(Data.Transit transit)
        {
            frmNhapKho = new FrameworkRepository<XULYKHO>(transit.KaraokeEntities, transit.KaraokeEntities.XULYKHOes);
            frmKho = new FrameworkRepository<KHO>(transit.KaraokeEntities, transit.KaraokeEntities.KHOes);
            frmNhanVien = new FrameworkRepository<NHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.NHANVIENs);
            frmXuLyKhoLoai = new FrameworkRepository<XULYKHOLOAI>(transit.KaraokeEntities, transit.KaraokeEntities.XULYKHOLOAIs);
        }

        public BOXuLyKho()
        {
            NhapKho = new XULYKHO();
            Kho = new KHO();
            NhanVien = new NHANVIEN();
        }

        public Data.KHO Kho { get; set; }

        public Data.NHANVIEN NhanVien { get; set; }

        public Data.XULYKHO NhapKho { get; set; }

        public Data.XULYKHOLOAI Loai { get; set; }

        public IQueryable<BOXuLyKho> GetAll(Transit mTransit, DateTime dt)
        {
            return (from xlk in frmNhapKho.Query()
                    join k in frmKho.Query() on xlk.KhoID equals k.KhoID
                    join nv in frmNhanVien.Query() on xlk.NhanVienID equals nv.NhanVienID
                    join l in frmXuLyKhoLoai.Query() on xlk.LoaiID equals l.ID
                    where xlk.ThoiGian.Value.Year == dt.Year && xlk.ThoiGian.Value.Month == dt.Month && xlk.ThoiGian.Value.Day == dt.Day
                    select new BOXuLyKho
                    {
                        NhapKho = xlk,
                        Kho = k,
                        NhanVien = nv,
                        Loai = l
                    }
                        );
        }

        public void Luu(List<BOXuLyKho> lsArray, List<BOXuLyKho> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOXuLyKho item in lsArray)
                {
                    if (item.NhapKho.ChinhKhoID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, null, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOXuLyKho item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            mTransit.KaraokeEntities.SaveChanges();
        }

        public int Them(BOXuLyKho item, List<BOXuLyKhoChiTiet> lsArray, Transit mTransit)
        {
            frmNhapKho.AddObject(item.NhapKho);
            frmNhapKho.Commit();
            return item.NhapKho.ChinhKhoID;
        }

        private int Sua(BOXuLyKho item, Transit mTransit)
        {
            item.NhapKho.Edit = false;
            frmNhapKho.Update(item.NhapKho);
            return item.NhapKho.ChinhKhoID;
        }

        private int Them(BOXuLyKho item, Transit mTransit)
        {
            frmNhapKho.AddObject(item.NhapKho);
            return item.NhapKho.ChinhKhoID;
        }

        private int Xoa(BOXuLyKho item, Transit mTransit)
        {
            item.NhapKho.Deleted = true;
            frmNhapKho.Update(item.NhapKho);
            return item.NhapKho.ChinhKhoID;
        }
    }
}
