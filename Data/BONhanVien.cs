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
        public BONhanVien()
        {

        }
        public static List<NHANVIEN> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<NHANVIEN>.QueryNoTracking(mTransit.KaraokeEntities.NHANVIENs).ToList();
        }
        public static List<BONhanVien> GetAll(Transit mTransit)
        {
            FrameworkRepository<NHANVIEN> frmNhanVien = new FrameworkRepository<NHANVIEN>(mTransit.KaraokeEntities);
            FrameworkRepository<LOAINHANVIEN> frmLoaiNhanVien = new FrameworkRepository<LOAINHANVIEN>(mTransit.KaraokeEntities);

            return (from n in frmNhanVien.Query()
                    join l in frmLoaiNhanVien.Query() on n.LoaiNhanVienID equals l.LoaiNhanVienID
                    where n.Deleted == false
                    select new BONhanVien
                    {
                        NhanVien = n,
                        LoaiNhanVien = l
                    }).ToList();
        }

        private static int Them(BONhanVien item, Transit mTransit, FrameworkRepository<NHANVIEN> frm)
        {
            frm.AddObject(item.NhanVien);
            return item.NhanVien.NhanVienID;
        }

        private static int Xoa(BONhanVien item, Transit mTransit, FrameworkRepository<NHANVIEN> frm)
        {
            frm.DeleteObject(item.NhanVien);
            return item.NhanVien.NhanVienID;
        }

        private static int Sua(BONhanVien item, Transit mTransit, FrameworkRepository<NHANVIEN> frm)
        {
            frm.Update(item.NhanVien);
            return item.NhanVien.NhanVienID;
        }

        public static void Luu(List<BONhanVien> lsArray, List<BONhanVien> lsArrayDeleted, Transit mTransit)
        {
            FrameworkRepository<NHANVIEN> frm = new FrameworkRepository<NHANVIEN>(mTransit.KaraokeEntities);
            if (lsArray != null)
                foreach (BONhanVien item in lsArray)
                {
                    if (item.NhanVien.NhanVienID > 0)
                        Sua(item, mTransit, frm);
                    else
                        Them(item, mTransit, frm);
                }
            if (lsArrayDeleted != null)
                foreach (BONhanVien item in lsArrayDeleted)
                {
                    Xoa(item, mTransit, frm);
                }
            frm.Commit();
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
    }
}
