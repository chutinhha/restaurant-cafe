using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOTonKho
    {
        public TONKHO Kho { get; set; }
        public string TenMon { get; set; }
        public static void NhapKhoTong(KaraokeEntities kara, BOChiTietNhapKho chitiet, BONhapKho nhapkho)
        {
            TONKHO tonkho = new TONKHO();
            tonkho.KhoID = nhapkho.NhapKho.KhoID;
            tonkho.MonID = chitiet.MenuKichThuocMon.MenuKichThuocMon.MonID;
            tonkho.SoLuongNhap = chitiet.ChiTietNhapKho.SoLuongNhap;
            tonkho.SoLuongTon = chitiet.ChiTietNhapKho.SoLuongNhap;
            tonkho.NgayNhap = DateTime.Now;
            tonkho.NgaySanXuat = chitiet.ChiTietNhapKho.NgaySanXuat;
            tonkho.NgayHetHan = chitiet.ChiTietNhapKho.NgayHetHan;
            tonkho.GiaNhap = chitiet.ChiTietNhapKho.GiaNhap;
            tonkho.GiaBan = chitiet.ChiTietNhapKho.GiaBan;
            tonkho.LoaiPhatSinhID = 1;            
            tonkho.Visual = true;
            kara.TONKHOes.AddObject(tonkho);
        }
        public static IQueryable<BOTonKho> GetTonKhoByKho(KaraokeEntities kara,KHO kho)
        {            
            return from a in kara.TONKHOes
                   join b in kara.MENUMONs on a.MonID equals b.MonID
                   where a.KhoID == kho.KhoID && a.SoLuongTon > 0
                   select new BOTonKho
                   {
                       Kho = a,
                       TenMon=b.TenDai
                   };
        }
        public void asasa()
        {
            //Kho.            
        }
    }
}
