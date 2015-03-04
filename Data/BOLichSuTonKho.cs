using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOLichSuTonKho
    {
        KaraokeEntities mKaraokeEntities = null;
        public BOLichSuTonKho()
        {
            mKaraokeEntities = new KaraokeEntities();
        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }

        public static System.Data.Objects.ObjectResult<BAOCAOLICHSUTONKHO> GetLichSuTonKho(KaraokeEntities kara, int KhoID, DateTime dtFrom, DateTime dtTo)
        {
            var Parameter_KhoID = new System.Data.SqlClient.SqlParameter("@KhoID", System.Data.SqlDbType.Int);
            Parameter_KhoID.Value = KhoID;
            var Parameter_DateFrom = new System.Data.SqlClient.SqlParameter("@DateFrom", System.Data.SqlDbType.DateTime);
            Parameter_DateFrom.Value = dtFrom;
            var Parameter_DateTo = new System.Data.SqlClient.SqlParameter("@DateTo", System.Data.SqlDbType.DateTime);
            Parameter_DateTo.Value = dtTo;
            return kara.ExecuteStoreQuery<BAOCAOLICHSUTONKHO>("SP_BAOCAOLICHSUTONKHO @KhoID, @DateFrom, @DateTo", Parameter_KhoID, Parameter_DateFrom, Parameter_DateTo);
        }
        public System.Data.Objects.ObjectResult<BAOCAOLICHSUTONKHO> GetLichSuTonKho(int KhoID, DateTime dtFrom, DateTime dtTo)
        {
            return GetLichSuTonKho(mKaraokeEntities, KhoID, dtFrom, dtTo);
        }
        
        public static void NhapKho(KaraokeEntities kara, BOChiTietNhapKho chitiet,BONhapKho nhapKho)
        {
            LICHSUTONKHO lichSuDauKy = (from a in kara.LICHSUTONKHOes
                                        where a.MonID == chitiet.MenuKichThuocMon.MenuKichThuocMon.MonID && a.KhoID==nhapKho.NhapKho.KhoID
                                        orderby a.ID descending
                                        select a).FirstOrDefault();
            LICHSUTONKHO lichSu = new LICHSUTONKHO();
            if (lichSuDauKy == null)
            {
                lichSu.DauKySoLuong = 0;
                lichSu.DauKyDonGia = 0;
            }
            else
            {
                lichSu.DauKySoLuong = lichSuDauKy.CuoiKySoLuong;
                lichSu.DauKyDonGia = lichSuDauKy.CuoiKyDonGia;
            }
            int soluong = chitiet.ChiTietNhapKho.SoLuongNhap * chitiet.MenuKichThuocMon.MenuKichThuocMon.KichThuocLoaiBan;
            lichSu.NhapSoLuong = soluong;
            lichSu.NhapDonGia = chitiet.ChiTietNhapKho.GiaNhap / chitiet.MenuKichThuocMon.MenuKichThuocMon.KichThuocLoaiBan;
            lichSu.CuoiKySoLuong = lichSu.DauKySoLuong + lichSu.NhapSoLuong;
            lichSu.CuoiKyDonGia = lichSu.CuoiKyDonGia;
            lichSu.NgayGhiNhan = DateTime.Now;
            lichSu.MonID = chitiet.MenuKichThuocMon.MenuKichThuocMon.MonID;
            lichSu.KhoID = nhapKho.NhapKho.KhoID;
            lichSu.LoaiPhatSinhID = 1;
            kara.LICHSUTONKHOes.AddObject(lichSu);
        }
        public static void ChuyenKho(KaraokeEntities kara, BOChuyenKho chuyenKho)
        {
            var ktm = (from a in kara.MENUKICHTHUOCMONs
                       where a.MonID == chuyenKho.TonKho.MonID
                       orderby a.KichThuocLoaiBan ascending
                       select a).FirstOrDefault();
            if (ktm!=null)
            {
                int soluong = chuyenKho.SoLuong;
                //if (ktm.KichThuocLoaiBan>0)
                //{
                //    soluong = soluong / ktm.KichThuocLoaiBan;
                //}
                //kho di
                LICHSUTONKHO lichSuDauKyDi = (from a in kara.LICHSUTONKHOes
                                        where a.MonID == ktm.MonID && a.KhoID == chuyenKho.TonKho.KhoID
                                        orderby a.ID descending
                                        select a).FirstOrDefault();
                LICHSUTONKHO lichSuDi = new LICHSUTONKHO();
                if (lichSuDauKyDi == null)
                {
                    lichSuDi.DauKySoLuong = 0;
                    lichSuDi.DauKyDonGia = 0;
                }
                else
                {
                    lichSuDi.DauKySoLuong = lichSuDauKyDi.CuoiKySoLuong;
                    lichSuDi.DauKyDonGia = lichSuDauKyDi.CuoiKyDonGia;
                }
                lichSuDi.XuatSoLuong = soluong;
                lichSuDi.XuatDonGia = chuyenKho.TonKho.GiaNhap;
                lichSuDi.CuoiKySoLuong = lichSuDi.DauKySoLuong - lichSuDi.XuatSoLuong;
                lichSuDi.CuoiKyDonGia = lichSuDi.CuoiKyDonGia;
                lichSuDi.NgayGhiNhan = DateTime.Now;
                lichSuDi.MonID = ktm.MonID;
                lichSuDi.KhoID = chuyenKho.TonKho.KhoID;
                lichSuDi.LoaiPhatSinhID = 2;
                kara.LICHSUTONKHOes.AddObject(lichSuDi);

                //kho den
                LICHSUTONKHO lichSuDauKyDen = (from a in kara.LICHSUTONKHOes
                                            where a.MonID == ktm.MonID && a.KhoID == chuyenKho.KhoDenID
                                            orderby a.ID descending
                                            select a).FirstOrDefault();
                LICHSUTONKHO lichSuDen = new LICHSUTONKHO();
                if (lichSuDauKyDen == null)
                {
                    lichSuDen.DauKySoLuong = 0;
                    lichSuDen.DauKyDonGia = 0;
                }
                else
                {
                    lichSuDen.DauKySoLuong = lichSuDauKyDen.CuoiKySoLuong;
                    lichSuDen.DauKyDonGia = lichSuDauKyDen.CuoiKyDonGia;
                }
                lichSuDen.NhapSoLuong = soluong;
                lichSuDen.XuatDonGia = chuyenKho.TonKho.GiaNhap;
                lichSuDen.CuoiKySoLuong = lichSuDen.DauKySoLuong + lichSuDen.NhapSoLuong;
                lichSuDen.CuoiKyDonGia = lichSuDen.CuoiKyDonGia;
                lichSuDen.NgayGhiNhan = DateTime.Now;
                lichSuDen.MonID = ktm.MonID;
                lichSuDen.KhoID = chuyenKho.KhoDenID;
                lichSuDen.LoaiPhatSinhID = 2;
                kara.LICHSUTONKHOes.AddObject(lichSuDen);
            }
        }
        public class test
        {
            public NHANVIEN nv { get; set; }
            public BANHANG bh { get; set; }
        }
        public static void BanHang(KaraokeEntities kara, BOBanHang banhang)
        {
            foreach (var chitiet in banhang._ListChiTietBanHang)
            {                
                int soluong = chitiet.ChiTietBanHang.SoLuongBan * chitiet.ChiTietBanHang.KichThuocLoaiBan;
                LICHSUTONKHO lichSuDauKy = (from a in kara.LICHSUTONKHOes
                                            where a.MonID == chitiet.MenuKichThuocMon.MonID
                                            orderby a.ID descending
                                            select a).FirstOrDefault();
                LICHSUTONKHO lichSu = new LICHSUTONKHO();
                if (lichSuDauKy == null)
                {
                    lichSu.DauKySoLuong = 0;
                    lichSu.DauKyDonGia = 0;
                }
                else
                {
                    lichSu.DauKySoLuong = lichSuDauKy.CuoiKySoLuong;
                    lichSu.DauKyDonGia = lichSuDauKy.CuoiKyDonGia;
                }
                lichSu.XuatSoLuong = soluong;
                lichSu.XuatDonGia = chitiet.ChiTietBanHang.GiaBan;
                lichSu.CuoiKySoLuong = lichSu.DauKySoLuong - lichSu.XuatSoLuong;
                lichSu.CuoiKyDonGia = lichSu.CuoiKyDonGia;
                lichSu.NgayGhiNhan = DateTime.Now;
                lichSu.MonID = chitiet.MenuKichThuocMon.MonID;
                lichSu.KhoID = banhang.KhoID;
                lichSu.LoaiPhatSinhID = 5;
                kara.LICHSUTONKHOes.AddObject(lichSu);
            }
        }
    }
}
