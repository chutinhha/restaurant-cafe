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
        public BOQuyenNhanVien()
        {

        }
        public static List<BOQuyenNhanVien> GetAll(int MaQuyen, Transit mTransit)
        {
            FrameworkRepository<NHANVIEN> frmNhanVien = new FrameworkRepository<NHANVIEN>(mTransit.KaraokeEntities);
            FrameworkRepository<QUYEN> frmQuyen = new FrameworkRepository<QUYEN>(mTransit.KaraokeEntities);
            FrameworkRepository<QUYENNHANVIEN> frmQuyenNhanVien = new FrameworkRepository<QUYENNHANVIEN>(mTransit.KaraokeEntities);

            return (from qnv in frmQuyenNhanVien.Query()
                    join q in frmQuyen.Query() on qnv.QuyenID equals q.MaQuyen
                    join nv in frmNhanVien.Query() on qnv.NhanVienID equals nv.NhanVienID
                    where qnv.Deleted == false && qnv.Deleted == false && qnv.QuyenID == MaQuyen
                    select new BOQuyenNhanVien
                    {
                        QuyenNhanVien = qnv,
                        Quyen = q,
                        NhanVien = nv
                    }).ToList();

        }

        private static int Them(BOQuyenNhanVien item, Transit mTransit, FrameworkRepository<QUYENNHANVIEN> frm)
        {
            frm.AddObject(item.QuyenNhanVien);
            return item.QuyenNhanVien.ID;
        }

        private static int Xoa(BOQuyenNhanVien item, Transit mTransit, FrameworkRepository<QUYENNHANVIEN> frm)
        {
            frm.DeleteObject(item.QuyenNhanVien);
            return item.QuyenNhanVien.ID;
        }

        private static int Sua(BOQuyenNhanVien item, Transit mTransit, FrameworkRepository<QUYENNHANVIEN> frm)
        {
            frm.Update(item.QuyenNhanVien);
            return item.QuyenNhanVien.ID;
        }

        public static void Luu(List<BOQuyenNhanVien> lsArray, List<BOQuyenNhanVien> lsArrayDeleted, Transit mTransit)
        {
            FrameworkRepository<QUYENNHANVIEN> frm = new FrameworkRepository<QUYENNHANVIEN>(mTransit.KaraokeEntities);
            if (lsArray != null)
                foreach (BOQuyenNhanVien item in lsArray)
                {
                    if (item.QuyenNhanVien.ID > 0)
                        Sua(item, mTransit, frm);
                    else
                        Them(item, mTransit, frm);
                }
            if (lsArrayDeleted != null)
                foreach (BOQuyenNhanVien item in lsArrayDeleted)
                {
                    if (item.QuyenNhanVien.ID > 0)
                        Xoa(item, mTransit, frm);
                }
            frm.Commit();
        }
    }
}
