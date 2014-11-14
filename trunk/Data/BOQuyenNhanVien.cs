using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOQuyenNhanVien
    {
        public QUYEN Quyen { get; set; }
        public QUYENNHANVIEN QuyenNhanVien { get; set; }
        public NHANVIEN NhanVien { get; set; }
        FrameworkRepository<NHANVIEN> frmNhanVien = null;
        FrameworkRepository<QUYEN> frmQuyen = null;
        FrameworkRepository<QUYENNHANVIEN> frmQuyenNhanVien = null;

        public BOQuyenNhanVien(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmNhanVien = new FrameworkRepository<NHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.NHANVIENs);
            frmQuyen = new FrameworkRepository<QUYEN>(transit.KaraokeEntities, transit.KaraokeEntities.QUYENs);
            frmQuyenNhanVien = new FrameworkRepository<QUYENNHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.QUYENNHANVIENs);
        }
        public BOQuyenNhanVien()
        {
            Quyen = new QUYEN();
            QuyenNhanVien = new QUYENNHANVIEN();
            NhanVien = new NHANVIEN();
        }
        public IQueryable<BOQuyenNhanVien> GetAll(int MaQuyen, Transit mTransit)
        {
            return (from qnv in frmQuyenNhanVien.Query()
                    join q in frmQuyen.Query() on qnv.QuyenID equals q.MaQuyen
                    join nv in frmNhanVien.Query() on qnv.NhanVienID equals nv.NhanVienID
                    where qnv.Deleted == false && qnv.Deleted == false && qnv.QuyenID == MaQuyen
                    select new BOQuyenNhanVien
                    {
                        QuyenNhanVien = qnv,
                        Quyen = q,
                        NhanVien = nv
                    });

        }

        private int Them(BOQuyenNhanVien item, Transit mTransit)
        {
            frmQuyenNhanVien.AddObject(item.QuyenNhanVien);
            return item.QuyenNhanVien.ID;
        }

        private int Xoa(BOQuyenNhanVien item, Transit mTransit)
        {
            frmQuyenNhanVien.DeleteObject(item.QuyenNhanVien);
            return item.QuyenNhanVien.ID;
        }

        private int Sua(BOQuyenNhanVien item, Transit mTransit)
        {
            frmQuyenNhanVien.Update(item.QuyenNhanVien);
            return item.QuyenNhanVien.ID;
        }

        public void Luu(List<BOQuyenNhanVien> lsArray, List<BOQuyenNhanVien> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOQuyenNhanVien item in lsArray)
                {
                    if (item.QuyenNhanVien.ID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOQuyenNhanVien item in lsArrayDeleted)
                {
                    if (item.QuyenNhanVien.ID > 0)
                        Xoa(item, mTransit);
                }
            frmQuyenNhanVien.Commit();
        }
    }
}
