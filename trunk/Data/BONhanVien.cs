using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BONhanVien
    {
        public FrameworkRepository<NHANVIEN> frmNhanVien = null;
        public FrameworkRepository<LOAINHANVIEN> frmLoaiNhanVien = null;
        FrameworkRepository<Data.LICHSUDANGNHAP> frmLichSuDangNhap = null;

        public NHANVIEN NhanVien { get; set; }
        public LOAINHANVIEN LoaiNhanVien { get; set; }
        public BONhanVien(Data.Transit transit)
        {
            frmNhanVien = new FrameworkRepository<NHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.NHANVIENs);
            frmLoaiNhanVien = new FrameworkRepository<LOAINHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.LOAINHANVIENs);
            frmLichSuDangNhap = new FrameworkRepository<LICHSUDANGNHAP>(transit.KaraokeEntities, transit.KaraokeEntities.LICHSUDANGNHAPs);
        }

        public BONhanVien()
        {
            NhanVien = new NHANVIEN();
            LoaiNhanVien = new LOAINHANVIEN();
        }

        public static IQueryable<NHANVIEN> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<NHANVIEN>.QueryNoTracking(mTransit.KaraokeEntities.NHANVIENs).Where(s => s.Deleted == false);
        }
        public IQueryable<BONhanVien> GetAll(Transit mTransit)
        {
            return (from n in frmNhanVien.Query()
                    join l in frmLoaiNhanVien.Query() on n.LoaiNhanVienID equals l.LoaiNhanVienID
                    where n.Deleted == false
                    select new BONhanVien
                    {
                        NhanVien = n,
                        LoaiNhanVien = l
                    });
        }

        private int Them(BONhanVien item, Transit mTransit)
        {
            frmNhanVien.AddObject(item.NhanVien);
            return item.NhanVien.NhanVienID;
        }

        private int Xoa(BONhanVien item, Transit mTransit)
        {
            item.NhanVien.Deleted = true;
            frmNhanVien.Update(item.NhanVien);
            return item.NhanVien.NhanVienID;
        }

        private int Sua(BONhanVien item, Transit mTransit)
        {
            item.NhanVien.Edit = false;
            frmNhanVien.Update(item.NhanVien);
            return item.NhanVien.NhanVienID;
        }

        public void Luu(List<BONhanVien> lsArray, List<BONhanVien> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BONhanVien item in lsArray)
                {
                    if (item.NhanVien.NhanVienID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BONhanVien item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmNhanVien.Commit();
        }

        public static NHANVIEN Login(string TenDangNhap, string MatKhau, Data.Transit mTransit)
        {
            if (TenDangNhap != null && MatKhau != null)
            {
                var Parameter_TenDangNhap = new System.Data.SqlClient.SqlParameter("@TenDangNhap", System.Data.SqlDbType.VarChar, 50);
                Parameter_TenDangNhap.Value = TenDangNhap;
                var Parameter_MatKhau = new System.Data.SqlClient.SqlParameter("@MatKhau", System.Data.SqlDbType.VarChar, 255);
                Parameter_MatKhau.Value = MatKhau;
                List<NHANVIEN> lsArray = mTransit.KaraokeEntities.ExecuteStoreQuery<NHANVIEN>("SP_Login_NhanVien @TenDangNhap, @MatKhau", Parameter_TenDangNhap, Parameter_MatKhau).ToList();
                if (lsArray.Count > 0)
                    return lsArray[0];
            }
            return null;
        }

        public void ThemLichSuDangNhap(int NhanVienID)
        {
            Data.LICHSUDANGNHAP item = new LICHSUDANGNHAP() { NhanVienID = NhanVienID, ThoiGian = DateTime.Now, Deleted = false, Edit = false, Visual = true };
            frmLichSuDangNhap.AddObject(item);
            frmLichSuDangNhap.Commit();
        }
    }
}
