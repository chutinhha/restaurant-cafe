using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChucNang
    {
        public static IQueryable<CHUCNANG> GetAllNoTracking(Transit mTransit)
        {
            return FrameworkRepository<CHUCNANG>.QueryNoTracking(mTransit.KaraokeEntities.CHUCNANGs).Where(s => s.Deleted == false);
        }

        public static List<CHUCNANG> GetAll(Transit mTransit)
        {
            return mTransit.KaraokeEntities.CHUCNANGs.Where(s => s.Deleted == false).ToList();
        }

        public static int Them(CHUCNANG item, Transit mTransit)
        {
            mTransit.KaraokeEntities.CHUCNANGs.AddObject(item);
            mTransit.KaraokeEntities.SaveChanges();
            return item.ChucNangID;
        }
    }

    public class TypeChucNang
    {
        public enum BanHang
        {
            TinhTien = 101,
            LuuHoaDon = 102,
            ThayDoiGia = 103,
            ChuyenBan = 104,
            TachBan = 104,
            XoaMon = 106,
            XoaToanBoMon = 107
        }

        public enum QuanLyNhanVien
        {
            QuanLyNhanVien = 201
        }

        public enum QuanLyMayIn
        {
            CaiDatMayIn = 301,
            CaiDatThucDonMayIn = 302
        }

        public enum QuanLyThucDon
        {
            QuanLyThucDon = 401
        }

        public enum QuanLyKhachHang
        {
            KhachHang = 501,
            LoaiKhachHang = 502
        }

        public enum QuanLyThuChi
        {
            QuanLyThuChi = 601
        }

        public enum QuanLyGia
        {
            LoaiGia = 701,
            LichBieuDinhKy = 702,
            LichBieuKhongDinhKy = 703,
            DanhSachBan = 705,
            DanhSachGia = 705,
            KhuyenMai = 706
        }

        public enum QuanLyPhanQuyen
        {
            Quyen = 801,
            CaiDatQuyenNhanVienThuocNhomQuyen = 802,
            CaiDatQuyenCuaNhomQuyen = 803
        }

        public enum QuanLyKho
        {
            TonKho = 901,
            NhaKho = 902,
            NhapKho = 903,
            HuKho = 904,
            MatKho = 905,
            ChuyenKho = 906,
            ChinhKho = 907
        }

        public enum DinhLuong
        {
            DinhLuong = 1000
        }

        public enum QuanLySoDoBan
        {
            QuanLyKhu = 1101,
            QuanLyBan = 1102
        }

        public enum Baocao
        {

        }

        public enum QuanLyThe
        {
            QuanLyThe = 1301
        }

        public enum CaiDatChuongTrinh
        {
            CaiDatChuongTrinh = 1401
        }

        public enum ThongTinChuongTrinh
        {
            ThongTinChuongTrinh = 1501
        }
    }
}
