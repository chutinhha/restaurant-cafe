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
            frmTonKhoTong = new FrameworkRepository<TONKHOTONG>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOTONGs);
            frmTonKho = new FrameworkRepository<TONKHO>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOes);
        }

        /// <summary>
        /// Sẽ trả về một danh danh chi tiết bán hàng gồm những món không đủ trong kho
        /// </summary>
        /// <param name="lsArray"></param>
        /// <param name="transit"></param>
        /// <returns></returns>
        public List<BOChiTietBanHang> TruKho(List<BOChiTietBanHang> lsArray, Data.Transit transit)
        {
            List<BOChiTietBanHang> result = new List<BOChiTietBanHang>();
            foreach (BOChiTietBanHang item in lsArray)
            {
                frmTonKho.Refresh();
                IQueryable<Data.TONKHO> lsTonKho = frmTonKho.Query().Where(s => s.DonViID == item.MENUKICHTHUOCMON.DonViID && s.MonID == item.MENUKICHTHUOCMON.MonID && s.KhoID == transit.KhoID).OrderBy(s => s.NgayHetHan).ThenBy(s => s.TonKhoID);
                int SL = (int)(item.CHITIETBANHANG.SoLuongBan * item.MENUKICHTHUOCMON.KichThuocLoaiBan);
                if (SL < lsTonKho.Sum(s => s.SoLuongTon))
                {
                    foreach (var line in lsTonKho)
                    {
                        if (SL != 0)
                        {
                            if (SL > line.SoLuongTon)
                            {
                                SL = SL - (int)line.SoLuongTon;
                                line.SoLuongTon = 0;
                            }
                            else
                            {
                                SL = 0;
                                line.SoLuongTon = line.SoLuongTon - SL;
                            }
                            frmTonKho.Update(line);
                        }
                        else
                        {
                            result.Add(item);
                            break;
                        }
                    }
                    Data.TONKHOTONG tonKhoTong = KiemTraTonKhoTong(transit.KhoID, (int)item.MENUKICHTHUOCMON.MonID, (int)item.MENUKICHTHUOCMON.DonViID);
                    tonKhoTong.SoLuongBan += SL;
                    tonKhoTong.SoLuongTon -= SL;
                    frmTonKhoTong.Update(tonKhoTong);
                }

            }
            frmTonKho.Commit();
            frmTonKhoTong.Commit();
            return result;
        }

        public void NhapKho(List<BOChiTietNhapKho> lsArray, Data.Transit transit)
        {
            foreach (BOChiTietNhapKho item in lsArray)
            {
                Data.TONKHOTONG tonKhoTong = KiemTraTonKhoTong((int)item.NhapKho.KhoID, (int)item.MenuMon.MonID, (int)item.LoaiBan.DonViID);
                tonKhoTong.SoLuongNhap += (item.ChiTietNhapKho.TONKHO.SoLuongNhap * item.ChiTietNhapKho.TONKHO.DonViTinh * item.LoaiBan.KichThuocBan);
                frmTonKhoTong.Update(tonKhoTong);
            }
            frmTonKhoTong.Commit();
        }

        private Data.TONKHOTONG KiemTraTonKhoTong(int KhoID, int MonID, int DonViID)
        {
            if (frmTonKhoTong.Query().Where(s => s.KhoID == KhoID && s.MonID == MonID && s.DonViID == DonViID).Count() == 0)
            {
                Data.TONKHOTONG item = new TONKHOTONG();
                item.DonViID = DonViID;
                item.KhoID = KhoID;
                item.MonID = MonID;
                item.TenMonBaoCao = "";
                item.SoLuongBan = 0;
                item.SoLuongDieuChinh = 0;
                item.SoLuongHu = 0;
                item.SoLuongNhap = 0;
                item.SoLuongTon = 0;
                item.SoLuongMat = 0;
                frmTonKhoTong.AddObject(item);
                frmTonKhoTong.Commit();
            }
            return frmTonKhoTong.Query().Where(s => s.KhoID == KhoID && s.MonID == MonID && s.DonViID == DonViID).FirstOrDefault();
        }

        public void ChuyenKho()
        {

        }

        public void HuKho()
        {

        }

        public void ChinhKho()
        {

        }

        public void MatKho()
        {

        }
    }
}
