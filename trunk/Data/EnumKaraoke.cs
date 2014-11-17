using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public enum EnumChucNang
    {
        XoaMon = 1,
        XoaToanBoMon,
        TinhTien,
        LuuHoaDon,
        ThayDoiGia,
        ChuyenBan,
        TachBan
    }

    public enum EnumLoaiBan
    {
        Cai = 1,
        Gram = 2,
        Kg = 3,
        Millilit = 4,
        Lit = 5,
        Gio = 6,
        Phut = 7,
        Giay = 8,
        DinhLuong = 9

    }

    public enum EnumDonVi
    {
        SoLuong,
        TrongLuong,
        TheTich,
        ThoiGian,
        DinhLuong
    }

    public enum EnumLoaiNhanVien
    {
        QuanLy = 1,
        ThuKho = 2,
        NhanVien = 3
    }
}
