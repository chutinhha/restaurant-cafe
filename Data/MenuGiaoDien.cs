namespace Data
{
    public class MenuGiaoDien
    {
        public MenuGiaoDien()
        {
            Kho = new MenuKho();
            KhachHang = new MenuKhachHang();
            GiaKhuyenMai = new MenuGiaKhuyenMai();
            Quyen = new MenuQuyen();
            The = new MenuThe();
            NhanVien = new MenuNhanVien();
            DinhLuong = new MenuDinhLuong();
            MayIn = new MenuMayIn();
            QuanLySoDoBan = new MenuQuanLySoDoBan();
        }

        public MenuKho Kho { get; set; }
        public MenuKhachHang KhachHang { get; set; }
        public MenuGiaKhuyenMai GiaKhuyenMai { get; set; }
        public MenuQuyen Quyen { get; set; }
        public MenuThe The { get; set; }
        public MenuNhanVien NhanVien { get; set; }
        public MenuDinhLuong DinhLuong { get; set; }
        public MenuMayIn MayIn { get; set; }
        public MenuQuanLySoDoBan QuanLySoDoBan { get; set; }

        public class MenuKho
        {
            public bool ChinhKho = false;
            public bool ChuyenKho = false;
            public bool HuKho = false;
            public bool MatKho = false;
            public bool NhaCungCap = true;
            public bool NhaKho = true;
            public bool NhapKho = true;
            public bool NhieuNhaNho = true;
            public bool TonKho = false;
        }

        public class MenuKhachHang
        {
            public bool LoaiKhachHang = true;
            public bool KhachHang = true;
        }

        public class MenuGiaKhuyenMai
        {
            public bool LoaiGia = true;
            public bool DanhSachBan = true;
            public bool LichBieuDinhKy = true;
            public bool LichBieuKhongDinKy = true;
            public bool KhuyenMai = true;
        }

        public class MenuQuyen
        {
            public bool DanhSachQuyen = true;
        }

        public class MenuThe
        {
            public bool QuanLyThe = true;
        }

        public class MenuNhanVien
        {
            public bool QuanLyNhanVien = true;
        }

        public class MenuDinhLuong
        {
            public bool DinhLuong = true;
        }

        public class MenuMayIn
        {
            public bool MayIn = true;
            public bool CaiDatThucDonMayIn = true;
        }

        public class MenuQuanLySoDoBan
        {
            public bool SoDoBan = true;
            public bool QuanLyKhu = true;

        }
    }
}
