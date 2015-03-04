using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOTonKho
    {
        public TONKHO TonKho { get; set; }
        public string TenMon { get; set; }
        public static void ChuyenKhoTong(KaraokeEntities kara, TONKHO tonkho, int? khoDen, int soluong)
        {
            if (soluong <= tonkho.SoLuongTon)
            {
                tonkho.SoLuongTon -= soluong;
                TONKHO tonkhoDen = new TONKHO();
                tonkhoDen.KhoID = khoDen;
                tonkhoDen.MonID = tonkho.MonID;
                tonkhoDen.LoaiBanID = tonkho.LoaiBanID;
                tonkhoDen.DonViID = tonkho.DonViID;
                tonkhoDen.DonViTinh = tonkho.DonViTinh;
                tonkhoDen.PhatSinhTuTonKhoID = tonkho.TonKhoID;
                tonkhoDen.SoLuongNhap = soluong;
                tonkhoDen.SoLuongTon = soluong;
                tonkhoDen.NgayNhap = tonkho.NgayNhap;
                tonkhoDen.NgaySanXuat = tonkho.NgaySanXuat;
                tonkhoDen.NgayHetHan = tonkho.NgayHetHan;
                tonkhoDen.GiaBan = tonkho.GiaBan;
                tonkhoDen.GiaNhap = tonkho.GiaNhap;
                tonkhoDen.LoaiPhatSinhID = 2;
                tonkhoDen.SoLuongPhatSinh = soluong;
                tonkhoDen.Visual = true;
                kara.TONKHOes.AddObject(tonkhoDen);
                kara.SaveChanges();
            }
        }
        public static void NhapKhoTong(KaraokeEntities kara, BOChiTietNhapKho chitiet, BONhapKho nhapkho)
        {
            int soluongNhap = chitiet.ChiTietNhapKho.SoLuongNhap * chitiet.MenuKichThuocMon.KichThuocLoaiBan;
            TONKHO tonkho = new TONKHO();
            tonkho.KhoID = nhapkho.NhapKho.KhoID;
            tonkho.MonID = chitiet.MenuKichThuocMon.MenuKichThuocMon.MonID;
            tonkho.SoLuongNhap = soluongNhap;
            tonkho.SoLuongTon = soluongNhap;
            tonkho.NgayNhap = DateTime.Now;
            tonkho.NgaySanXuat = chitiet.ChiTietNhapKho.NgaySanXuat;
            tonkho.NgayHetHan = chitiet.ChiTietNhapKho.NgayHetHan;
            tonkho.GiaNhap = chitiet.ChiTietNhapKho.GiaNhap;
            tonkho.GiaBan = chitiet.ChiTietNhapKho.GiaBan;
            tonkho.LoaiPhatSinhID = 1;            
            tonkho.Visual = true;
            kara.TONKHOes.AddObject(tonkho);
        }
        public static void BanHang(KaraokeEntities kara, BOBanHang banhang)
        {
            foreach (var chitiet in banhang._ListChiTietBanHang)
            {
                int soluongBan = chitiet.ChiTietBanHang.SoLuongBan * chitiet.ChiTietBanHang.KichThuocLoaiBan;
                var dsTonKho = GetTonKhoByMonID(kara, chitiet.MenuKichThuocMon.MonID,banhang.KhoID);
                foreach (var item in dsTonKho)
                {
                    if (soluongBan>0)
                    {
                        if (soluongBan>=item.SoLuongTon)
                        {
                            soluongBan -= item.SoLuongTon;
                            item.SoLuongTon = 0;
                        }
                        else
                        {
                            item.SoLuongTon -= soluongBan;
                            soluongBan = 0;
                        }
                    }
                }
                if (soluongBan>0)
                {
                    //xy ly sau KHOATRAN
                }
            }
        }
        public static IQueryable<BOTonKho> GetTonKhoByKho(KaraokeEntities kara,KHO kho)
        {            
            return from a in kara.TONKHOes
                   join b in kara.MENUMONs on a.MonID equals b.MonID
                   where a.KhoID == kho.KhoID && a.SoLuongTon > 0
                   select new BOTonKho
                   {
                       TonKho = a,
                       TenMon=b.TenDai
                   };
        }
        public static IQueryable<TONKHO> GetTonKhoByMonID(KaraokeEntities kara, int? monID,int? khoID)
        {
            return from a in kara.TONKHOes                   
                   where a.SoLuongTon > 0 && a.MonID==monID && a.KhoID==khoID
                   orderby a.NgayHetHan ascending
                   select a;
        }
        public void asasa()
        {
            //Kho.            
        }
    }
}
