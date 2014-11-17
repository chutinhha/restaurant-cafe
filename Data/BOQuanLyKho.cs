using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOQuanLyKho
    {
        FrameworkRepository<TONKHOTONG> frmTonKhoTong = null;
        FrameworkRepository<TONKHO> frmTonKho = null;
        public BOQuanLyKho(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmTonKhoTong = new FrameworkRepository<TONKHOTONG>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOTONGs);
            frmTonKho = new FrameworkRepository<TONKHO>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOes);
        }

        public void TruKho(List<BOChiTietBanHang> lsArray, Data.Transit transit)
        {
            foreach (BOChiTietBanHang item in lsArray)
            {

            }
        }

        public void NhapKho(List<BOChiTietNhapKho> lsArray, Data.Transit transit)
        {
            foreach (BOChiTietNhapKho item in lsArray)
            {
                Data.TONKHOTONG tonKhoTong = KiemTraTonKhoTong((int)item.NhapKho.KhoID, (int)item.MenuMon.MonID, (int)item.LoaiBan.DonViID);
                switch (item.LoaiBan.DonViID)
                {
                    case (int)EnumDonVi.SoLuong:
                    case (int)EnumDonVi.TrongLuong:
                    case (int)EnumDonVi.TheTich:
                        tonKhoTong.SoLuongNhap += (item.ChiTietNhapKho.SoLuong * item.ChiTietNhapKho.KichThuocBan);
                        break;
                    case (int)EnumDonVi.ThoiGian:
                        break;
                    case (int)EnumDonVi.DinhLuong:
                        break;
                    default:
                        break;
                }
                frmTonKhoTong.Update(tonKhoTong);
            }
            frmTonKhoTong.Commit();
        }

        private Data.TONKHOTONG KiemTraTonKhoTong(int KhoID, int MonID, int DonVi)
        {
            if (frmTonKhoTong.Query().Where(s => s.KhoID == KhoID && s.MonID == MonID && s.DonViBan == DonVi).Count() == 0)
            {
                Data.TONKHOTONG item = new TONKHOTONG();
                item.DonViBan = DonVi;
                item.KhoID = KhoID;
                item.MonID = MonID;
                item.SoLuongBan = 0;
                item.SoLuongDieuChinh = 0;
                item.SoLuongHu = 0;
                item.SoLuongNhap = 0;
                item.SoLuongTon = 0;
                frmTonKhoTong.AddObject(item);
                frmTonKhoTong.Commit();
            }
            return frmTonKhoTong.Query().Where(s => s.KhoID == KhoID && s.MonID == MonID && s.DonViBan == DonVi).FirstOrDefault();
        }

    }
}
