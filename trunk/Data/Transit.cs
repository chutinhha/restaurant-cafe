using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Data
{
    public class Transit
    {
        public Data.NHANVIEN NhanVien { get; set; }
        public Data.NHANVIEN Admin { get; set; }
        public Data.MenuGiaoDien MenuGiaoDien { get; set; }
        public CAIDATBANHANG CaiDatBanHang { get; set; }
        public BAN Ban { get; set; }
        public KHU Khu { get; set; }
        public BOBanHang BanHang { get; set; }
        //=================
        public KHACHHANG KhachHang { get; set; }
        public THE The { get; set; }
        //================
        public string HashMD5 { get; set; }
        public KaraokeEntities KaraokeEntities { get; set; }
        public THAMSO ThamSo { get; set; }
        public ClassStringButton StringButton { get; set; }
        public List<DONVI> ListDonVi { get; set; }
        //================Quyền=============
        public IQueryable<BOChiTietQuyen> DanhSachQuyen { get; set; }
        public IQueryable<CHUCNANG> DanhSachChucNang { get; set; }
        public Data.BOChiTietQuyen BOChiTietQuyen = null;
        public string DuongDanHinh { get; set; }
        public int KhoID { get; set; }
        public int MayID { get; set; }
        public Transit()
        {
            StringButton = new ClassStringButton();
            MenuGiaoDien = new Data.MenuGiaoDien();
            HashMD5 = "KTr";
            DuongDanHinh = "c:\\";
            Admin = new NHANVIEN();
            Admin.NhanVienID = 0;
            Admin.LoaiNhanVienID = (int)Data.EnumLoaiNhanVien.Admin;
            Admin.CapDo = -1;
            Admin.TenNhanVien = "Admin";
            Admin.TenDangNhap = "0000";
            Admin.MatKhau = Utilities.SecurityKaraoke.GetMd5Hash("0000", HashMD5);
            KhoID = 1;
            MayID = 1;

            KaraokeEntities = new KaraokeEntities();
            KaraokeEntities.ContextOptions.LazyLoadingEnabled = false;
            ThamSo = KaraokeEntities.THAMSOes.Where(o => o.SoMay == 1).FirstOrDefault();
            ListDonVi = BODonVi.GetAll(this).ToList();
            BOChiTietQuyen = new BOChiTietQuyen(this);
            CaiDatBanHang = KaraokeEntities.CAIDATBANHANGs.FirstOrDefault();
            if (CaiDatBanHang == null)
            {
                CaiDatBanHang = new CAIDATBANHANG();
            }
            //==================
            //var nhom = KaraokeEntities.CHUCNANGs.ToList();
            //foreach (var item in nhom)
            //{
            //    KaraokeEntities.Attach(item);
            //    KaraokeEntities.DeleteObject(item);
            //}
            //var list = KaraokeEntities.NHOMCHUCNANGs.ToList();
            //foreach (var item in list)
            //{
            //    KaraokeEntities.Attach(item);
            //    KaraokeEntities.DeleteObject(item);
            //}
            //KaraokeEntities.SaveChanges();
            //new BONhomChucNang(this);            
        }

        public void LayDanhSachQuyen()
        {
            if (NhanVien.NhanVienID != 0)
            {
                DanhSachQuyen = BOChiTietQuyen.LayDanhSachQuyen(NhanVien);
                DanhSachChucNang = BOChucNang.GetAllNoTracking(this);
            }
            else
                DanhSachQuyen = null;
        }

        public bool KiemTraChucNang(int ChucNangID)
        {
            if (DanhSachChucNang != null)
            {
                CHUCNANG item = DanhSachChucNang.Where(s => s.ChucNangID == ChucNangID).FirstOrDefault();
                return item == null ? false : item.Visual;

            }
            return true;
        }

        public bool KiemTraNhomChucNang(int NhomChucNangID)
        {
            if (DanhSachChucNang != null)
            {
                var list = DanhSachChucNang.Where(s => s.NhomChucNangID == NhomChucNangID);
                foreach (var item in list)
                {
                    if (item.Visual == true)
                        return true;
                    return false;
                }

            }
            return true;
        }
        public static void MakeLisence(int keyType, Data.Transit transit)
        {
            DateTime now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day);
            if (keyType == 1)
            {
                transit.ThamSo.NgayBatDau = now;
                transit.ThamSo.XacNhanNgayBatDau = Utilities.SecurityKaraoke.GetHashDay(transit.ThamSo.NgayBatDau.Value, transit.HashMD5);
                transit.ThamSo.NgayKetThuc = transit.ThamSo.NgayBatDau.Value.AddDays(7);
                transit.ThamSo.XacNhanNgayKetThuc = Utilities.SecurityKaraoke.GetHashDay(transit.ThamSo.NgayKetThuc.Value, transit.HashMD5);
            }
            else if (keyType == 2)
            {
                transit.ThamSo.NgayBatDau = now;
                transit.ThamSo.XacNhanNgayBatDau = Utilities.SecurityKaraoke.GetHashDay(transit.ThamSo.NgayBatDau.Value, transit.HashMD5);
                transit.ThamSo.NgayKetThuc = transit.ThamSo.NgayBatDau.Value.AddYears(1);
                transit.ThamSo.XacNhanNgayKetThuc = Utilities.SecurityKaraoke.GetHashDay(transit.ThamSo.NgayKetThuc.Value, transit.HashMD5);
            }
            transit.KaraokeEntities.SaveChanges();
        }
        public class ClassStringButton
        {
            public string ThemMoi = "Thêm mới";
            public string CapNhat = "Cập nhật";
            public string Huy = "Hủy";
            public string Luu = "Lưu";
            public string Them = "Thêm";
            public string LuuThanhCong = "Lưu thành công";
            public string XoaThanhCong = "Xóa thành công";
            public string SuaThanhCong = "Sửa thành công";
            public string ThemThanhCong = "Thêm thành công";
            public string DangNhapKhongDung = "Đăng nhập không đúng";
        }
    }
}
