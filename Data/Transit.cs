﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class Transit
    {
        public Data.NHANVIEN NhanVien { get; set; }
        public Data.NHANVIEN Admin { get; set; }
        public Data.MenuGiaoDien MenuGiaoDien { get; set; }
        public BAN Ban { get; set; }
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
        public Data.BOChiTietQuyen BOChiTietQuyen = null;
        public int KhoID { get; set; }
        public Transit()
        {
            StringButton = new ClassStringButton();
            MenuGiaoDien = new Data.MenuGiaoDien();
            HashMD5 = "KTr";
            Admin = new NHANVIEN();
            Admin.NhanVienID = 0;
            Admin.LoaiNhanVienID = (int)Data.EnumLoaiNhanVien.QuanLy;
            Admin.TenNhanVien = "Admin";
            Admin.TenDangNhap = "0000";
            Admin.MatKhau = Utilities.SecurityKaraoke.GetMd5Hash("0000", HashMD5);
            KhoID = 1;

            KaraokeEntities = new KaraokeEntities();
            KaraokeEntities.ContextOptions.LazyLoadingEnabled = false;
            KaraokeEntities.MENUKICHTHUOCMONs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            KaraokeEntities.MENUMONs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            ThamSo = KaraokeEntities.THAMSOes.Where(o => o.SoMay == 1).FirstOrDefault();
            KhachHang = KaraokeEntities.KHACHHANGs.FirstOrDefault();
            The = KaraokeEntities.THEs.FirstOrDefault();
            ListDonVi = BODonVi.GetAll(this);
            BOChiTietQuyen = new BOChiTietQuyen(this);
            //Data.BONhomChucNang BONhomChucNang = new BONhomChucNang(this);
        }

        public void LayDanhSachQuyen()
        {
            if (NhanVien.NhanVienID != 0)
                DanhSachQuyen = BOChiTietQuyen.LayDanhSachQuyen(NhanVien);
            else
                DanhSachQuyen = null;
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
        }
    }
}
