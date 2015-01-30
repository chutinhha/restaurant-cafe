using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BONhanVien
    {
        public NHANVIEN NhanVien { get; set; }
        public LOAINHANVIEN LoaiNhanVien { get; set; }
        KaraokeEntities mKaraokeEntities = null;

        public BONhanVien(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
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
            return (from nv in mKaraokeEntities.NHANVIENs
                    join lnv in mKaraokeEntities.LOAINHANVIENs on nv.LoaiNhanVienID equals lnv.LoaiNhanVienID
                    where nv.Deleted == false && nv.CapDo > mTransit.NhanVien.CapDo || nv.NhanVienID == mTransit.NhanVien.NhanVienID
                    select new BONhanVien
                    {
                        NhanVien = nv,
                        LoaiNhanVien = lnv
                    });
        }

        public void Luu(List<BONhanVien> lsArray)
        {
            foreach (BONhanVien item in lsArray)
            {
                if (item.NhanVien.NhanVienID == 0)
                {
                    mKaraokeEntities.NHANVIENs.AddObject(item.NhanVien);
                }

            }
            mKaraokeEntities.SaveChanges();
        }
        public void Refresh()
        {
            mKaraokeEntities.Refresh(System.Data.Objects.RefreshMode.StoreWins, mKaraokeEntities.NHANVIENs);
        }
        public static NHANVIEN CheckLogin(KaraokeEntities kara,string user, string pass)
        {
            if (!String.IsNullOrEmpty(user) && !String.IsNullOrEmpty(pass))
            {
                var Parameter_TenDangNhap = new System.Data.SqlClient.SqlParameter("@TenDangNhap", System.Data.SqlDbType.VarChar, 50);
                Parameter_TenDangNhap.Value = user;
                var Parameter_MatKhau = new System.Data.SqlClient.SqlParameter("@MatKhau", System.Data.SqlDbType.VarChar, 255);
                Parameter_MatKhau.Value = pass;
                NHANVIEN nv = kara.ExecuteStoreQuery<NHANVIEN>("SP_Login_NhanVien @TenDangNhap, @MatKhau", Parameter_TenDangNhap, Parameter_MatKhau).FirstOrDefault();
                return nv;
            }
            return null;
        }

        public static NHANVIEN CheckLogin(string user, string pass,Transit transit)
        {
            //if (!String.IsNullOrEmpty(user) && !String.IsNullOrEmpty(pass))
            //{
            //    var Parameter_TenDangNhap = new System.Data.SqlClient.SqlParameter("@TenDangNhap", System.Data.SqlDbType.VarChar, 50);
            //    Parameter_TenDangNhap.Value = user;
            //    var Parameter_MatKhau = new System.Data.SqlClient.SqlParameter("@MatKhau", System.Data.SqlDbType.VarChar, 255);
            //    Parameter_MatKhau.Value = pass;
            //    NHANVIEN nv = transit.KaraokeEntities.ExecuteStoreQuery<NHANVIEN>("SP_Login_NhanVien @TenDangNhap, @MatKhau", Parameter_TenDangNhap, Parameter_MatKhau).FirstOrDefault();
            //    return nv;
            //}
            //return null;
            return CheckLogin(transit.KaraokeEntities, user, pass);
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
            mKaraokeEntities.LICHSUDANGNHAPs.AddObject(new LICHSUDANGNHAP() { NhanVienID = NhanVienID, ThoiGian = DateTime.Now, Deleted = false, Edit = false, Visual = true });
            mKaraokeEntities.SaveChanges();
        }

        public IQueryable<LOAINHANVIEN> GetLoaiNhanVien(int CapDo)
        {
            return BOLoaiNhanVien.GetAllNoTracking(mKaraokeEntities, CapDo);
        }
    }
}
