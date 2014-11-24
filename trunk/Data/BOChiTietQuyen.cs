using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Data
{
    public class BOChiTietQuyen
    {
        public CHITIETQUYEN ChiTietQuyen { get; set; }
        public QUYEN Quyen { get; set; }
        public CHUCNANG ChucNang { get; set; }
        public QUYENNHANVIEN QuyenNhanVien { get; set; }

        public Visibility IsChoPhep { get; set; }
        public Visibility IsDangNhap { get; set; }
        public Visibility IsThem { get; set; }
        public Visibility IsXoa { get; set; }
        public Visibility IsSua { get; set; }
        private Data.Transit mTransit = null;

        FrameworkRepository<CHITIETQUYEN> frmChiTietQuyen = null;
        FrameworkRepository<QUYEN> frmQuyen = null;
        FrameworkRepository<CHUCNANG> frmChucNang = null;
        FrameworkRepository<QUYENNHANVIEN> frmQuyenNhanVien = null;
        public BOChiTietQuyen(Transit transit)
        {
            mTransit = transit;
            frmChiTietQuyen = new FrameworkRepository<CHITIETQUYEN>(transit.KaraokeEntities, transit.KaraokeEntities.CHITIETQUYENs);
            frmQuyen = new FrameworkRepository<QUYEN>(transit.KaraokeEntities, transit.KaraokeEntities.QUYENs);
            frmChucNang = new FrameworkRepository<CHUCNANG>(transit.KaraokeEntities, transit.KaraokeEntities.CHUCNANGs);
            frmQuyenNhanVien = new FrameworkRepository<QUYENNHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.QUYENNHANVIENs);
        }
        public BOChiTietQuyen()
        {
            ChiTietQuyen = new CHITIETQUYEN();
            Quyen = new QUYEN();
            ChucNang = new CHUCNANG();
            QuyenNhanVien = new QUYENNHANVIEN();
        }

        public IQueryable<BOChiTietQuyen> GetAll(int MaQuyen, Transit mTransit)
        {
            var res = (from ctq in frmChiTietQuyen.Query()
                       join q in frmQuyen.Query() on ctq.QuyenID equals q.MaQuyen
                       join cn in frmChucNang.Query() on ctq.ChucNangID equals cn.ChucNangID
                       where ctq.Deleted == false && ctq.Deleted == false && ctq.QuyenID == MaQuyen
                       select new BOChiTietQuyen
                       {
                           ChiTietQuyen = ctq,
                           Quyen = q,
                           ChucNang = cn
                       });
            return res;
        }
        private int Them(BOChiTietQuyen item, Transit mTransit)
        {
            frmChiTietQuyen.AddObject(item.ChiTietQuyen);
            return item.ChiTietQuyen.ChiTietQuyenID;
        }

        private int Xoa(BOChiTietQuyen item, Transit mTransit)
        {
            frmChiTietQuyen.DeleteObject(item.ChiTietQuyen);
            return item.ChiTietQuyen.ChiTietQuyenID;
        }

        private int Sua(BOChiTietQuyen item, Transit mTransit)
        {
            frmChiTietQuyen.Update(item.ChiTietQuyen);
            return item.ChiTietQuyen.ChiTietQuyenID;
        }

        public void Luu(List<BOChiTietQuyen> lsArray, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOChiTietQuyen item in lsArray)
                {
                    if (item.ChiTietQuyen.ChiTietQuyenID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            frmChiTietQuyen.Commit();
        }

        public IQueryable<BOChiTietQuyen> LayDanhSachQuyen(Data.NHANVIEN NhanVien)
        {
            return from qnv in frmQuyenNhanVien.Query()
                   join ctq in frmChiTietQuyen.Query() on qnv.QuyenID equals ctq.QuyenID
                   where qnv.NhanVienID == NhanVien.NhanVienID
                   select new BOChiTietQuyen
                   {
                       QuyenNhanVien = qnv,
                       ChiTietQuyen = ctq
                   };
        }

        public BOChiTietQuyen KiemTraNhomChucNang(int NhomChucNangID)
        {
            BOChiTietQuyen item = new BOChiTietQuyen();
            if (mTransit.DanhSachQuyen != null && mTransit.NhanVien.LoaiNhanVienID > (int)Data.EnumLoaiNhanVien.Admin)
            {
                IQueryable<BOChiTietQuyen> list = mTransit.DanhSachQuyen.Where(s => s.ChiTietQuyen.NhomChucNangID == NhomChucNangID);
                if (list.Count() > 0)
                {
                    foreach (BOChiTietQuyen line in list)
                    {
                        item.ChiTietQuyen.ChoPhep = ((bool)item.ChiTietQuyen.ChoPhep || (bool)line.ChiTietQuyen.ChoPhep) ? true : false;
                        item.ChiTietQuyen.DangNhap = ((bool)item.ChiTietQuyen.DangNhap || (bool)line.ChiTietQuyen.DangNhap) ? true : false;
                        item.ChiTietQuyen.Them = ((bool)item.ChiTietQuyen.Them || (bool)line.ChiTietQuyen.Them) ? true : false;
                        item.ChiTietQuyen.Sua = ((bool)item.ChiTietQuyen.Sua || (bool)line.ChiTietQuyen.Sua) ? true : false;
                        item.ChiTietQuyen.Xoa = ((bool)item.ChiTietQuyen.Xoa || (bool)line.ChiTietQuyen.Xoa) ? true : false;
                    }
                }
            }
            else
            {
                item.ChiTietQuyen.ChoPhep = true;
                item.ChiTietQuyen.DangNhap = false;
                item.ChiTietQuyen.Them = true;
                item.ChiTietQuyen.Xoa = true;
                item.ChiTietQuyen.Sua = true;
            }
            return item;
        }

        public BOChiTietQuyen KiemTraQuyen(int MaChucNang)
        {
            BOChiTietQuyen item = new BOChiTietQuyen();
            if (mTransit.DanhSachQuyen != null && mTransit.NhanVien.LoaiNhanVienID > (int)Data.EnumLoaiNhanVien.Admin)
            {
                IQueryable<BOChiTietQuyen> list = mTransit.DanhSachQuyen.Where(s => s.ChiTietQuyen.ChucNangID == MaChucNang);
                if (list.Count() > 0)
                {
                    foreach (BOChiTietQuyen line in list)
                    {
                        item.ChiTietQuyen.ChoPhep = ((bool)item.ChiTietQuyen.ChoPhep || (bool)line.ChiTietQuyen.ChoPhep) ? true : false;
                        item.ChiTietQuyen.DangNhap = ((bool)item.ChiTietQuyen.DangNhap || (bool)line.ChiTietQuyen.DangNhap) ? true : false;
                        item.ChiTietQuyen.Them = ((bool)item.ChiTietQuyen.Them || (bool)line.ChiTietQuyen.Them) ? true : false;
                        item.ChiTietQuyen.Sua = ((bool)item.ChiTietQuyen.Sua || (bool)line.ChiTietQuyen.Sua) ? true : false;
                        item.ChiTietQuyen.Xoa = ((bool)item.ChiTietQuyen.Xoa || (bool)line.ChiTietQuyen.Xoa) ? true : false;
                    }
                }
            }
            else
            {
                item.ChiTietQuyen.ChoPhep = true;
                item.ChiTietQuyen.DangNhap = false;
                item.ChiTietQuyen.Them = true;
                item.ChiTietQuyen.Xoa = true;
                item.ChiTietQuyen.Sua = true;
            }
            return item;
        }
    }
}
