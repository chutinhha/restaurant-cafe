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

        public int ChuyenKho(BOChuyenKho item, List<BOChiTietChuyenKho> lsArray, Transit mTransit)
        {

            if (lsArray != null)
            {
                List<TONKHOTONG> lsTonKhoDi = new List<TONKHOTONG>();
                List<TONKHOTONG> lsTonKhoDen = new List<TONKHOTONG>();
                Data.TONKHOTONG tonKhoTong = null;
                foreach (BOChiTietChuyenKho line in lsArray)
                {
                    IQueryable<Data.TONKHO> lsTonKho = frmTonKho.Query().Where(s => s.DonViID == line.MenuMon.DonViID && s.MonID == line.MenuMon.MonID && s.KhoID == item.ChuyenKho.KhoDiID && s.SoLuongTon > 0).OrderBy(s => s.NgayHetHan).ThenBy(s => s.TonKhoID);
                    int SL = (int)(line.ChiTietChuyenKho.TONKHO.SoLuongNhap * line.ChiTietChuyenKho.TONKHO.DonViTinh);
                    if (SL < lsTonKho.Sum(s => s.SoLuongTon))
                    {
                        foreach (var l in lsTonKho)
                        {
                            if (SL != 0)
                            {
                                if (SL > l.SoLuongTon)
                                {
                                    SL = SL - (int)l.SoLuongTon;
                                    l.SoLuongTon = 0;
                                }
                                else
                                {
                                    l.SoLuongTon = l.SoLuongTon - SL;
                                    SL = 0;
                                }

                                BOChiTietChuyenKho chck = new BOChiTietChuyenKho();
                                chck.ChiTietChuyenKho.TONKHO = new TONKHO();
                                chck.ChiTietChuyenKho.TONKHO.DonViTinh = line.ChiTietChuyenKho.TONKHO.DonViTinh;
                                chck.ChiTietChuyenKho.TONKHO.Deleted = false;
                                chck.ChiTietChuyenKho.TONKHO.Edit = false;
                                chck.ChiTietChuyenKho.TONKHO.SoLuongNhap = (line.ChiTietChuyenKho.TONKHO.SoLuongNhap - SL);
                                line.ChiTietChuyenKho.TONKHO.SoLuongNhap = SL;
                                chck.ChiTietChuyenKho.TONKHO.SoLuongTon = chck.ChiTietChuyenKho.TONKHO.SoLuongNhap;
                                chck.ChiTietChuyenKho.TONKHO.Visual = true;
                                chck.ChiTietChuyenKho.TONKHO.KhoID = item.ChuyenKho.KhoDenID;
                                chck.ChiTietChuyenKho.TONKHO.SoLuongPhatSinh = 0;
                                chck.ChiTietChuyenKho.TONKHO.PhatSinhTuTonKhoID = l.TonKhoID;
                                chck.ChiTietChuyenKho.TONKHO.LoaiPhatSinhID = (int)Data.EnumLoaiPhatSinh.ChuyenKho;
                                chck.ChiTietChuyenKho.TONKHO.NgayHetHan = l.NgayHetHan;
                                chck.ChiTietChuyenKho.TONKHO.NgaySanXuat = l.NgaySanXuat;
                                chck.ChiTietChuyenKho.TONKHO.GiaBan = l.GiaBan;
                                chck.ChiTietChuyenKho.TONKHO.GiaNhap = l.GiaNhap;
                                chck.ChiTietChuyenKho.TONKHO.MonID = line.ChiTietChuyenKho.TONKHO.MonID;
                                chck.ChiTietChuyenKho.TONKHO.LoaiBanID = line.ChiTietChuyenKho.TONKHO.LoaiBanID;
                                chck.ChiTietChuyenKho.TONKHO.DonViID = line.ChiTietChuyenKho.TONKHO.DonViID;

                                chck.ChuyenKho = new CHUYENKHO();
                                chck.ChuyenKho.KhoDiID = item.ChuyenKho.KhoDiID;
                                chck.ChuyenKho.KhoDenID = item.ChuyenKho.KhoDenID;
                                item.ChuyenKho.CHITIETCHUYENKHOes.Add(chck.ChiTietChuyenKho);

                                frmTonKho.Update(l);

                                if (lsTonKhoDi.Exists(s => s.KhoID == chck.ChuyenKho.KhoDiID && s.MonID == line.MenuMon.MonID && s.DonViID == line.MenuMon.DonViID))
                                    tonKhoTong = lsTonKhoDi.Find(s => s.KhoID == chck.ChuyenKho.KhoDiID && s.MonID == line.MenuMon.MonID && s.DonViID == line.MenuMon.DonViID);
                                else
                                {
                                    tonKhoTong = KiemTraTonKhoTong(frmTonKhoTong, (int)chck.ChuyenKho.KhoDiID, (int)line.MenuMon.MonID, (int)line.MenuMon.DonViID);
                                    lsTonKhoDi.Add(tonKhoTong);
                                }
                                tonKhoTong.SoLuongChuyen -= chck.ChiTietChuyenKho.TONKHO.SoLuongNhap;
                                tonKhoTong.SoLuongTon -= chck.ChiTietChuyenKho.TONKHO.SoLuongNhap;

                                if (lsTonKhoDen.Exists(s => s.KhoID == chck.ChuyenKho.KhoDenID && s.MonID == line.MenuMon.MonID && s.DonViID == line.MenuMon.DonViID))
                                    tonKhoTong = lsTonKhoDen.Find(s => s.KhoID == chck.ChuyenKho.KhoDenID && s.MonID == line.MenuMon.MonID && s.DonViID == line.MenuMon.DonViID);
                                else
                                {
                                    tonKhoTong = KiemTraTonKhoTong(frmTonKhoTong, (int)chck.ChuyenKho.KhoDenID, (int)line.MenuMon.MonID, (int)line.MenuMon.DonViID);
                                    lsTonKhoDen.Add(tonKhoTong);
                                }
                                tonKhoTong.SoLuongChuyen += chck.ChiTietChuyenKho.TONKHO.SoLuongNhap;
                                tonKhoTong.SoLuongTon += chck.ChiTietChuyenKho.TONKHO.SoLuongNhap;
                                if (SL == 0)
                                    break;
                            }
                        }

                    }
                }
                foreach (var tkt in lsTonKhoDen)
                {
                    if (tkt.ID == 0)
                        frmTonKhoTong.AddObject(tkt);
                    else
                        frmTonKhoTong.Update(tkt);
                }
                foreach (var tkt in lsTonKhoDi)
                {
                    if (tkt.ID == 0)
                        frmTonKhoTong.AddObject(tkt);
                    else
                        frmTonKhoTong.Update(tkt);
                }
            }
            frmTonKho.Commit();
            frmTonKhoTong.Commit();
            return item.ChuyenKho.ChuyenKhoID;
        }

        public void NhapKho(List<BOChiTietNhapKho> lsArray, Data.Transit transit)
        {
            List<TONKHOTONG> lsTonKhoTong = new List<TONKHOTONG>();
            foreach (BOChiTietNhapKho item in lsArray)
            {
                Data.TONKHOTONG tonKhoTong = null;
                if (lsTonKhoTong.Exists(s => s.KhoID == item.NhapKho.KhoID && s.MonID == item.MenuMon.MonID && s.DonViID == item.LoaiBan.DonViID))
                    tonKhoTong = lsTonKhoTong.Find(s => s.KhoID == item.NhapKho.KhoID && s.MonID == item.MenuMon.MonID && s.DonViID == item.LoaiBan.DonViID);
                else
                {
                    tonKhoTong = KiemTraTonKhoTong((int)item.NhapKho.KhoID, (int)item.MenuMon.MonID, (int)item.LoaiBan.DonViID);
                    lsTonKhoTong.Add(tonKhoTong);
                }
                tonKhoTong.SoLuongNhap += (item.ChiTietNhapKho.TONKHO.SoLuongNhap);
                tonKhoTong.SoLuongTon += (item.ChiTietNhapKho.TONKHO.SoLuongNhap);


            }
            foreach (var item in lsTonKhoTong)
            {
                frmTonKhoTong.Update(item);
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
                item.SoLuongChuyen = 0;
                frmTonKhoTong.AddObject(item);
                frmTonKhoTong.Commit();
            }
            return frmTonKhoTong.Query().Where(s => s.KhoID == KhoID && s.MonID == MonID && s.DonViID == DonViID).FirstOrDefault();
        }

        private Data.TONKHOTONG KiemTraTonKhoTong(FrameworkRepository<TONKHOTONG> frm, int KhoID, int MonID, int DonViID)
        {
            Data.TONKHOTONG item = null;
            if (frm.Query().Where(s => s.KhoID == KhoID && s.MonID == MonID && s.DonViID == DonViID).Count() == 0)
            {
                item = new TONKHOTONG();
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
                item.SoLuongChuyen = 0;
                return item;
            }
            return frm.Query().Where(s => s.KhoID == KhoID && s.MonID == MonID && s.DonViID == DonViID).FirstOrDefault();
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
