using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BONhomChucNang
    {
        public Data.NHOMCHUCNANG BanHang { get; set; }
        public Data.NHOMCHUCNANG QuanLyNhanVien { get; set; }
        public Data.NHOMCHUCNANG QuanLyMayIn { get; set; }
        public Data.NHOMCHUCNANG QuanLyThucDon { get; set; }
        public Data.NHOMCHUCNANG QuanLyKhachHang { get; set; }
        public Data.NHOMCHUCNANG QuanLyThuChi { get; set; }
        public Data.NHOMCHUCNANG QuanLyGia { get; set; }
        public Data.NHOMCHUCNANG QuanLyPhanQuyen { get; set; }
        public Data.NHOMCHUCNANG QuanLyKho { get; set; }
        public Data.NHOMCHUCNANG QuanLyDinhLuong { get; set; }
        public Data.NHOMCHUCNANG QuanLySoDoBan { get; set; }
        public Data.NHOMCHUCNANG BaoCao { get; set; }
        public Data.NHOMCHUCNANG QuanLyThe { get; set; }
        public Data.NHOMCHUCNANG CaiDatChuongTrinh { get; set; }
        public Data.NHOMCHUCNANG ThongTinPhanMem { get; set; }        

        public BONhomChucNang(Transit transit)
        {
            BanHang = new NHOMCHUCNANG() { NhomChucNangID = 1, TenNhomChucNang = "Bán hàng", Deleted = false, Visual = true, Edit = false };
            BanHang.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = BanHang.NhomChucNangID * 100 + 1, TenChucNang = "Tính tiền", NhomChucNangID = BanHang.NhomChucNangID, ChoPhep = true, DangNhap = true, Xem = false, Them = false, Xoa = false, Sua = false, Visual = true, Deleted = false, Edit = false });
            BanHang.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = BanHang.NhomChucNangID * 100 + 2, TenChucNang = "Xóa món", NhomChucNangID = BanHang.NhomChucNangID, ChoPhep = true, DangNhap = true, Xem = false, Them = false, Xoa = false, Sua = false, Visual = true, Deleted = false, Edit = false });

            QuanLyNhanVien = new NHOMCHUCNANG() { NhomChucNangID = 2, TenNhomChucNang = "Quản lý nhân viến", Deleted = false, Visual = true, Edit = false };
            QuanLyNhanVien.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyNhanVien.NhomChucNangID * 100 + 1, TenChucNang = "Quản lý nhân viên", NhomChucNangID = QuanLyNhanVien.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            QuanLyMayIn = new NHOMCHUCNANG() { NhomChucNangID = 3, TenNhomChucNang = "Quản lý máy in", Deleted = false, Visual = true, Edit = false };
            QuanLyMayIn.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyMayIn.NhomChucNangID * 100 + 1, TenChucNang = "Cài đặt máy in", NhomChucNangID = QuanLyMayIn.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyMayIn.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyMayIn.NhomChucNangID * 100 + 2, TenChucNang = "Cài đặt thực đơn máy in", NhomChucNangID = QuanLyMayIn.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            QuanLyThucDon = new NHOMCHUCNANG() { NhomChucNangID = 4, TenNhomChucNang = "Quản lý thực đơn", Deleted = false, Visual = true, Edit = false };
            QuanLyThucDon.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyThucDon.NhomChucNangID * 100 + 1, TenChucNang = "Quản lý thực đơn", NhomChucNangID = QuanLyThucDon.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            QuanLyKhachHang = new NHOMCHUCNANG() { NhomChucNangID = 5, TenNhomChucNang = "Quản lý khách hàng", Deleted = false, Visual = true, Edit = false };
            QuanLyKhachHang.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyKhachHang.NhomChucNangID * 100 + 1, TenChucNang = "Khách hàng", NhomChucNangID = QuanLyKhachHang.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyKhachHang.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyKhachHang.NhomChucNangID * 100 + 2, TenChucNang = "Loại khách hàng", NhomChucNangID = QuanLyKhachHang.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            QuanLyThuChi = new NHOMCHUCNANG() { NhomChucNangID = 6, TenNhomChucNang = "Quản lý thu chi", Deleted = false, Visual = true, Edit = false };
            QuanLyThuChi.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyThuChi.NhomChucNangID * 100 + 1, TenChucNang = "Quản lý thu chi", NhomChucNangID = QuanLyThuChi.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            QuanLyGia = new NHOMCHUCNANG() { NhomChucNangID = 7, TenNhomChucNang = "Quản lý giá", Deleted = false, Visual = true, Edit = false };
            QuanLyGia.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyGia.NhomChucNangID * 100 + 1, TenChucNang = "Loại giá", NhomChucNangID = QuanLyGia.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyGia.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyGia.NhomChucNangID * 100 + 2, TenChucNang = "Lịch biểu định kỳ", NhomChucNangID = QuanLyGia.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyGia.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyGia.NhomChucNangID * 100 + 3, TenChucNang = "Lịch biểu không định kỳ", NhomChucNangID = QuanLyGia.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyGia.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyGia.NhomChucNangID * 100 + 4, TenChucNang = "Danh sách giá", NhomChucNangID = QuanLyGia.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyGia.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyGia.NhomChucNangID * 100 + 5, TenChucNang = "Khuyễn mãi", NhomChucNangID = QuanLyGia.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            QuanLyPhanQuyen = new NHOMCHUCNANG() { NhomChucNangID = 8, TenNhomChucNang = "Quản lý phần quyền", Deleted = false, Visual = true, Edit = false };
            QuanLyPhanQuyen.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyPhanQuyen.NhomChucNangID * 100 + 1, TenChucNang = "Quyền", NhomChucNangID = QuanLyPhanQuyen.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyPhanQuyen.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyPhanQuyen.NhomChucNangID * 100 + 2, TenChucNang = "Cài đặt nhân viên thuộc nhóm quyền", NhomChucNangID = QuanLyPhanQuyen.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyPhanQuyen.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyPhanQuyen.NhomChucNangID * 100 + 3, TenChucNang = "Cài đặt quyền của nhóm quyền", NhomChucNangID = QuanLyPhanQuyen.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            QuanLyKho = new NHOMCHUCNANG() { NhomChucNangID = 9, TenNhomChucNang = "Quản lý kho", Deleted = false, Visual = true, Edit = false };
            QuanLyKho.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyKho.NhomChucNangID * 100 + 1, TenChucNang = "Tồn kho", NhomChucNangID = QuanLyKho.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyKho.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyKho.NhomChucNangID * 100 + 2, TenChucNang = "Nhà kho", NhomChucNangID = QuanLyKho.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyKho.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyKho.NhomChucNangID * 100 + 3, TenChucNang = "Nhập kho", NhomChucNangID = QuanLyKho.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyKho.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyKho.NhomChucNangID * 100 + 4, TenChucNang = "Hư kho", NhomChucNangID = QuanLyKho.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyKho.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyKho.NhomChucNangID * 100 + 5, TenChucNang = "Mất kho", NhomChucNangID = QuanLyKho.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyKho.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyKho.NhomChucNangID * 100 + 6, TenChucNang = "Chuyển kho", NhomChucNangID = QuanLyKho.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLyKho.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyKho.NhomChucNangID * 100 + 7, TenChucNang = "Chỉnh kho", NhomChucNangID = QuanLyKho.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            QuanLyDinhLuong = new NHOMCHUCNANG() { NhomChucNangID = 10, TenNhomChucNang = "Định lượng", Deleted = false, Visual = true, Edit = false };
            QuanLyDinhLuong.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyDinhLuong.NhomChucNangID * 100 + 1, TenChucNang = "Định lượng", NhomChucNangID = QuanLyDinhLuong.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            QuanLySoDoBan = new NHOMCHUCNANG() { NhomChucNangID = 11, TenNhomChucNang = "Quản lý sơ đồ bàn", Deleted = false, Visual = true, Edit = false };
            QuanLySoDoBan.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLySoDoBan.NhomChucNangID * 100 + 1, TenChucNang = "Quản lý khu", NhomChucNangID = QuanLySoDoBan.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });
            QuanLySoDoBan.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLySoDoBan.NhomChucNangID * 100 + 2, TenChucNang = "Quản lý bàn", NhomChucNangID = QuanLySoDoBan.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            BaoCao = new NHOMCHUCNANG() { NhomChucNangID = 12, TenNhomChucNang = "Báo cáo", Deleted = false, Visual = true, Edit = false };


            QuanLyThe = new NHOMCHUCNANG() { NhomChucNangID = 13, TenNhomChucNang = "Quản lý thẻ", Deleted = false, Visual = true, Edit = false };
            QuanLyThe.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = QuanLyThe.NhomChucNangID * 100 + 1, TenChucNang = "Quản lý thẻ", NhomChucNangID = QuanLyThe.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            CaiDatChuongTrinh = new NHOMCHUCNANG() { NhomChucNangID = 14, TenNhomChucNang = "Cài đặt chương trình", Deleted = false, Visual = true, Edit = false };
            CaiDatChuongTrinh.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = CaiDatChuongTrinh.NhomChucNangID * 100 + 1, TenChucNang = "Nhân viên", NhomChucNangID = CaiDatChuongTrinh.NhomChucNangID, ChoPhep = false, DangNhap = true, Xem = true, Them = true, Xoa = true, Sua = true, Visual = true, Deleted = false, Edit = false });

            ThongTinPhanMem = new NHOMCHUCNANG() { NhomChucNangID = 15, TenNhomChucNang = "Thông tin phần mềm", Deleted = false, Visual = true, Edit = false };
            ThongTinPhanMem.CHUCNANGs.Add(new CHUCNANG() { ChucNangID = ThongTinPhanMem.NhomChucNangID * 100 + 1, TenChucNang = "Thông tin phần mềm", NhomChucNangID = ThongTinPhanMem.NhomChucNangID, ChoPhep = true, DangNhap = true, Xem = false, Them = false, Xoa = false, Sua = false, Visual = true, Deleted = false, Edit = false });

            transit.KaraokeEntities = new KaraokeEntities();
            FrameworkRepository<NHOMCHUCNANG> frmNhomChucNang = new FrameworkRepository<NHOMCHUCNANG>(transit.KaraokeEntities, transit.KaraokeEntities.NHOMCHUCNANGs);
            frmNhomChucNang.AddObject(BanHang);
            frmNhomChucNang.AddObject(QuanLyNhanVien);
            frmNhomChucNang.AddObject(QuanLyMayIn);
            frmNhomChucNang.AddObject(QuanLyThucDon);
            frmNhomChucNang.AddObject(QuanLyKhachHang);
            frmNhomChucNang.AddObject(QuanLyThuChi);
            frmNhomChucNang.AddObject(QuanLyGia);
            frmNhomChucNang.AddObject(QuanLyPhanQuyen);
            frmNhomChucNang.AddObject(QuanLyKho);
            frmNhomChucNang.AddObject(QuanLyDinhLuong);
            frmNhomChucNang.AddObject(QuanLySoDoBan);
            frmNhomChucNang.AddObject(BaoCao);
            frmNhomChucNang.AddObject(QuanLyThe);
            frmNhomChucNang.AddObject(CaiDatChuongTrinh);
            frmNhomChucNang.AddObject(ThongTinPhanMem);
            frmNhomChucNang.Commit();
        }

    }
}
