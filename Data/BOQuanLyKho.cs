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
        FrameworkRepository<TONKHOTONGLOG> frmTonKhoTongLog = null;
        FrameworkRepository<TONKHOCHITIETBANHANG> frmTonKhoChiTietBanHang = null;
        private Transit mTransit;
        BODinhLuong BODinhLuong = null;

        public BOQuanLyKho(Data.Transit transit)
        {
            mTransit = transit;
            frmTonKhoTong = new FrameworkRepository<TONKHOTONG>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOTONGs);
            frmTonKho = new FrameworkRepository<TONKHO>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOes);
            frmTonKhoTongLog = new FrameworkRepository<TONKHOTONGLOG>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOTONGLOGs);
            frmTonKhoChiTietBanHang = new FrameworkRepository<TONKHOCHITIETBANHANG>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOCHITIETBANHANGs);
            frmTonKhoTong.Refresh();
            frmTonKho.Refresh();
            frmTonKhoTongLog.Refresh();
            frmTonKhoChiTietBanHang.Refresh();
            BODinhLuong = new BODinhLuong(transit);
        }

        ///// <summary>
        ///// Sẽ trả về một danh danh chi tiết bán hàng gồm những món không đủ trong kho
        ///// </summary>
        ///// <param name="lsArray"></param>
        ///// <param name="transit"></param>
        ///// <returns></returns>
        //public void TruTonKho(List<BOChiTietBanHang> lsArray, Data.Transit transit)
        //{
        //    List<BOChiTietBanHang> result = new List<BOChiTietBanHang>();
        //    foreach (BOChiTietBanHang item in lsArray)
        //    {
        //        IQueryable<Data.TONKHO> lsTonKho = frmTonKho.Query().Where(s => s.DonViID == item.MENUKICHTHUOCMON.DonViID && s.MonID == item.MENUKICHTHUOCMON.MonID && s.KhoID == transit.KhoID && s.SoLuongTon > 0).OrderBy(s => s.NgayHetHan).ThenBy(s => s.TonKhoID);
        //        int SL = (int)(item.CHITIETBANHANG.SoLuongBan * item.MENUKICHTHUOCMON.KichThuocLoaiBan);
        //        if (SL < lsTonKho.Sum(s => s.SoLuongTon))
        //        {
        //            foreach (var l in lsTonKho)
        //            {
        //                if (SL != 0)
        //                {
        //                    if (SL > l.SoLuongTon)
        //                    {
        //                        SL = SL - (int)l.SoLuongTon;
        //                        l.SoLuongTon = 0;
        //                    }
        //                    else
        //                    {
        //                        l.SoLuongTon = l.SoLuongTon - SL;
        //                        SL = 0;
        //                    }

        //                    //item.CHITIETBANHANG.TONKHOCHITIETBANHANGs.Add(new ).TONKHOes.Add(l);
        //                    frmTonKho.Update(l);
        //                }
        //                else
        //                    break;
        //            }
        //        }

        //    }
        //    frmTonKho.Commit();
        //}

        /// <summary>
        /// Return false là không đủ, True là đã tính trừ kho xong rồi
        /// </summary>
        /// <param name="lsTonKho"></param>
        /// <param name="frmTK"></param>
        /// <param name="frmTKCT"></param>
        /// <param name="ChiTietBanHangID"></param>
        /// <param name="SL"></param>
        /// <param name="KhoID"></param>
        /// <param name="MonID"></param>
        /// <param name="DonViID"></param>
        /// <param name="mTransit"></param>
        /// <returns></returns>
        private bool ThemTruTonKho(List<TONKHO> lsTonKho, List<TONKHOTONG> lsTonKhoTong, FrameworkRepository<TONKHO> frmTK, FrameworkRepository<TONKHOCHITIETBANHANG> frmTKCT, FrameworkRepository<TONKHOTONG> frmTKT, int BanHangID, int ChiTietBanHangID, int SL, int KhoID, int MonID, int DonViID, Transit mTransit)
        {
            IQueryable<Data.TONKHO> lsArray = frmTK.Query().Where(s => s.DonViID == DonViID && s.MonID == MonID && s.KhoID == KhoID && s.SoLuongTon > 0).OrderBy(s => s.NgayHetHan).ThenBy(s => s.TonKhoID);
            if (lsArray.Count() > 0 && SL < lsArray.Sum(s => s.SoLuongTon))
            {
                foreach (var l in lsArray)
                {
                    //-----------------------------------------------------------
                    Data.TONKHOTONG tonkhotong = null;
                    if (lsTonKhoTong.Exists(s => s.KhoID == KhoID && s.MonID == MonID && s.DonViID == DonViID))
                    {
                        tonkhotong = lsTonKhoTong.Find(s => s.KhoID == KhoID && s.MonID == MonID && s.DonViID == DonViID);
                    }
                    else
                    {
                        tonkhotong = KiemTraTonKhoTong(frmTKT, KhoID, MonID, DonViID);
                        lsTonKhoTong.Add(tonkhotong);
                    }
                    tonkhotong.SoLuongTon -= SL;
                    tonkhotong.SoLuongBan += SL;
                    //-----------------------------------------------------------
                    Data.TONKHO tonkho = null;
                    if (lsTonKho.Exists(s => s.TonKhoID == l.TonKhoID))
                    {
                        tonkho = lsTonKho.Find(s => s.TonKhoID == l.TonKhoID);
                    }
                    else
                    {
                        tonkho = l;
                        lsTonKho.Add(tonkho);
                    }
                    //-----------------------------------------------------------

                    if (SL != 0)
                    {
                        if (SL > l.SoLuongTon)
                        {
                            SL = SL - (int)l.SoLuongTon;
                            frmTKCT.AddObject(new TONKHOCHITIETBANHANG()
                                {
                                    KhoID = KhoID,
                                    ChiTietBanHangID = ChiTietBanHangID,
                                    TonKhoID = l.TonKhoID,
                                    MonID = MonID,
                                    DonViID = DonViID,
                                    SoLuong = l.SoLuongTon,
                                    BanHangID = BanHangID
                                });
                            l.SoLuongTon = 0;
                        }
                        else
                        {
                            l.SoLuongTon = l.SoLuongTon - SL;
                            frmTKCT.AddObject(new TONKHOCHITIETBANHANG()
                            {
                                KhoID = KhoID,
                                ChiTietBanHangID = ChiTietBanHangID,
                                TonKhoID = l.TonKhoID,
                                MonID = MonID,
                                DonViID = DonViID,
                                SoLuong = SL,
                                BanHangID = BanHangID
                            });
                            SL = 0;
                        }
                    }
                    else
                        break;
                }
            }
            else
                return false;
            return true;
        }

        //private void SuaTruTonKho(List<TONKHO> lsTonKho, FrameworkRepository<TONKHO> frm, FrameworkRepository<TONKHOCHITIETBANHANG> frmChiTiet, int ChiTietBanHangID, int SL, int KhoID, int MonID, int DonViID, int TonKhoID, Transit mTransit)
        //{

        //    Data.TONKHO tonkho = null;
        //    if (lsTonKho.Exists(s => s.TonKhoID == TonKhoID))
        //    {
        //        tonkho = lsTonKho.Find(s => s.TonKhoID == TonKhoID);
        //    }
        //    else
        //    {
        //        tonkho = frm.Query().Where(s => s.TonKhoID == TonKhoID).FirstOrDefault();
        //        lsTonKho.Add(tonkho);
        //    }

        //    if (SL > tonkho.SoLuongTon)
        //    {
        //        ThemTruTonKho(lsTonKho, frm, frmChiTiet, ChiTietBanHangID, SL - tonkho.SoLuongTon, KhoID, MonID, DonViID, mTransit);
        //        tonkho.SoLuongTon = 0;
        //    }
        //    else
        //    {
        //        tonkho.SoLuongTon -= SL;
        //    }
        //}

        private void XoaTonKho(FrameworkRepository<TONKHO> frmTK, FrameworkRepository<TONKHOCHITIETBANHANG> frmTKCT, FrameworkRepository<TONKHOTONG> frmTKT, IQueryable<TONKHOCHITIETBANHANG> lsTonKhoChiTietBanHang)
        {
            List<TONKHO> lsTonKho = new List<TONKHO>();
            List<TONKHOTONG> lsTonKhoTong = new List<TONKHOTONG>();
            Data.TONKHO tonkho = null;
            foreach (TONKHOCHITIETBANHANG item in lsTonKhoChiTietBanHang)
            {
                if (lsTonKho.Exists(s => s.TonKhoID == item.TonKhoID))
                {
                    tonkho = lsTonKho.Find(s => s.TonKhoID == item.TonKhoID);
                }
                else
                {
                    tonkho = frmTK.Query().Where(s => s.TonKhoID == item.TonKhoID).FirstOrDefault();
                    lsTonKho.Add(tonkho);
                }
                tonkho.SoLuongTon += (int)item.SoLuong;
                frmTKCT.DeleteObject(item);
            }
            foreach (TONKHO item in lsTonKho)
            {
                frmTK.Update(item);
            }
            frmTKCT.Commit();
            frmTK.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void LuuTonKho(BOChiTietBanHang item)
        {
            List<BOChiTietBanHang> lsArray = new List<BOChiTietBanHang>();
            lsArray.Add(item);
            LuuTonKho(lsArray);
        }

        /// <summary>
        /// Lưu kho xuống
        /// </summary>
        /// <param name="lsArray">Truyền toàn bộ BOChiTietBanHang xuống</param>
        /// <param name="transit">Biến trung chuyển</param>
        public void LuuTonKho(List<BOChiTietBanHang> lsArray)
        {
            List<TONKHO> lsTonKho = new List<TONKHO>();
            List<TONKHOTONG> lsTonKhoTong = new List<TONKHOTONG>();
            //Duyệt toàn bộ danh sách
            List<int> ListChiTietBanHangIDs = new List<int>();
            int BanHangID = 0;
            foreach (BOChiTietBanHang item in lsArray)
            {
                BanHangID = (int)item.ChiTietBanHang.BanHangID;
                //Kiểm tra món đó có trong tồn kho hay không, nếu tồn kho thì bắt đầu trừ, còn lại nếu là định lượng phải lấy danh sách món định lượng rồi mới trừ
                if (item.MenuKichThuocMon.ChoPhepTonKho == true)
                {
                    IQueryable<TONKHOCHITIETBANHANG> lsTonKhoChiTietBanHang = frmTonKhoChiTietBanHang.Query().Where(s => s.ChiTietBanHangID == item.ChiTietBanHang.ChiTietBanHangID);
                    if (lsTonKhoChiTietBanHang.Count() == 0)
                    {
                        ThemTruTonKho(lsTonKho, lsTonKhoTong, frmTonKho, frmTonKhoChiTietBanHang, frmTonKhoTong, (int)item.ChiTietBanHang.BanHangID, item.ChiTietBanHang.ChiTietBanHangID, item.ChiTietBanHang.SoLuongBan * item.MenuKichThuocMon.KichThuocLoaiBan, mTransit.KhoID, (int)item.MenuKichThuocMon.MonID, (int)item.MenuKichThuocMon.DonViID, mTransit);
                    }
                    else
                    {
                        if (item.ChiTietBanHang.SoLuongBan != lsTonKhoChiTietBanHang.Sum(s => s.SoLuong))
                        {
                            XoaTonKho(frmTonKho, frmTonKhoChiTietBanHang, frmTonKhoTong, lsTonKhoChiTietBanHang);
                            if (item.ChiTietBanHang.SoLuongBan > 0)
                                ThemTruTonKho(lsTonKho, lsTonKhoTong, frmTonKho, frmTonKhoChiTietBanHang, frmTonKhoTong, (int)item.ChiTietBanHang.BanHangID, item.ChiTietBanHang.ChiTietBanHangID, item.ChiTietBanHang.SoLuongBan * item.MenuKichThuocMon.KichThuocLoaiBan, mTransit.KhoID, (int)item.MenuKichThuocMon.MonID, (int)item.MenuKichThuocMon.DonViID, mTransit);
                        }
                    }
                }
                else
                {
                    IQueryable<BODinhLuong> lsDinhLuong = BODinhLuong.GetAll(item.MenuKichThuocMon.KichThuocMonID, mTransit);
                    foreach (BODinhLuong line in lsDinhLuong)
                    {
                        IQueryable<TONKHOCHITIETBANHANG> lsTonKhoChiTietBanHang = frmTonKhoChiTietBanHang.Query().Where(s => s.ChiTietBanHangID == item.ChiTietBanHang.ChiTietBanHangID && s.MonID == line.MenuMon.MonID);
                        if (lsTonKhoChiTietBanHang.Count() == 0)
                        {
                            ThemTruTonKho(lsTonKho, lsTonKhoTong, frmTonKho, frmTonKhoChiTietBanHang, frmTonKhoTong, (int)item.ChiTietBanHang.BanHangID, item.ChiTietBanHang.ChiTietBanHangID, item.ChiTietBanHang.SoLuongBan * line.DinhLuong.KichThuocBan * line.DinhLuong.SoLuong, mTransit.KhoID, (int)line.MenuMon.MonID, (int)line.MenuMon.DonViID, mTransit);
                        }
                        else
                        {
                            if (item.ChiTietBanHang.SoLuongBan != lsTonKhoChiTietBanHang.Sum(s => s.SoLuong))
                            {
                                XoaTonKho(frmTonKho, frmTonKhoChiTietBanHang, frmTonKhoTong, lsTonKhoChiTietBanHang);
                                if (item.ChiTietBanHang.SoLuongBan > 0)
                                    ThemTruTonKho(lsTonKho, lsTonKhoTong, frmTonKho, frmTonKhoChiTietBanHang, frmTonKhoTong, (int)item.ChiTietBanHang.BanHangID, item.ChiTietBanHang.ChiTietBanHangID, item.ChiTietBanHang.SoLuongBan * line.DinhLuong.KichThuocBan * line.DinhLuong.SoLuong, mTransit.KhoID, (int)line.MenuMon.MonID, (int)line.MenuMon.DonViID, mTransit);
                            }
                        }
                    }
                }
            }
            ///Xóa những món không có trong ChiTietBanHangID
            IQueryable<TONKHOCHITIETBANHANG> lsTonKhoChiTietBanHangDelete = frmTonKhoChiTietBanHang.Query().Where(s => !ListChiTietBanHangIDs.Contains((int)s.ChiTietBanHangID) && s.BanHangID == BanHangID);
            XoaTonKho(frmTonKho, frmTonKhoChiTietBanHang, frmTonKhoTong, lsTonKhoChiTietBanHangDelete);
            foreach (TONKHO item in lsTonKho)
            {
                frmTonKho.Update(item);
            }
            foreach (TONKHOTONG item in lsTonKhoTong)
            {
                frmTonKhoTong.Update(item);
            }
            frmTonKhoTong.Commit();
            frmTonKhoChiTietBanHang.Commit();
            frmTonKho.Commit();

        }

        public int KiemTraTonKhoTong(Data.Transit mTransit, BOChiTietBanHang item)
        {
            if (item.MenuKichThuocMon.ChoPhepTonKho == true)
                return KiemTraTonKhoTong((int)mTransit.KhoID, (int)item.MenuMon.MonID, (int)item.MenuMon.DonViID).SoLuongTon;
            else
            {
                int result = -1;
                IQueryable<BODinhLuong> lsDinhLuong = BODinhLuong.GetAll(item.MenuKichThuocMon.KichThuocMonID, mTransit);
                foreach (var line in lsDinhLuong)
                {
                    int SL = KiemTraTonKhoTong((int)mTransit.KhoID, (int)line.MenuMon.MonID, (int)line.MenuMon.DonViID).SoLuongTon;
                    if (SL == 0)
                        return SL;
                    if (line.DinhLuong.SoLuong != 0)
                        if (result == -1 || SL / line.DinhLuong.SoLuong * line.DinhLuong.KichThuocBan < result)
                        {
                            result = SL / line.DinhLuong.SoLuong * line.DinhLuong.KichThuocBan;
                        }
                }
                return result;
            }
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
                            }
                            else
                                break;

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

        private void ThemLog(FrameworkRepository<TONKHOTONGLOG> frm, Data.Transit mTransit, int MonID, int DonViID, int SL)
        {
            TONKHOTONG tonKhoTong = KiemTraTonKhoTong(mTransit.KhoID, MonID, DonViID);
            tonKhoTong.SoLuongBan += SL;
            tonKhoTong.SoLuongTon -= SL;

            frm.AddObject(new TONKHOTONGLOG()
                {
                    MayID = mTransit.MayID,
                    KhoID = mTransit.KhoID,
                    MonID = MonID,
                    DonViID = DonViID,
                    SoLuong = SL
                });

            frmTonKhoTong.Update(tonKhoTong);
            frmTonKhoTong.Commit();
        }

        private void TruLog(FrameworkRepository<TONKHOTONGLOG> frm, Data.Transit mTransit, int MonID, int DonViID, int SL)
        {
            TONKHOTONG tonKhoTong = KiemTraTonKhoTong(mTransit.KhoID, MonID, DonViID);
            tonKhoTong.SoLuongBan += SL;
            tonKhoTong.SoLuongTon -= SL;

            frm.AddObject(new TONKHOTONGLOG()
            {
                MayID = mTransit.MayID,
                KhoID = mTransit.KhoID,
                MonID = MonID,
                DonViID = DonViID,
                SoLuong = SL
            });
            frmTonKhoTong.Update(tonKhoTong);
            frmTonKhoTong.Commit();

        }

        public void ThayDoiLog(Data.Transit mTransit, int MonID, int DonViID, int SLThem, int SLXoa)
        {
            if (SLThem != SLXoa)
            {
                if (SLThem > 0 && SLXoa > 0)
                {
                    if (SLThem > SLXoa)
                    {
                        ThemLog(frmTonKhoTongLog, mTransit, MonID, DonViID, SLThem - SLXoa);
                    }
                    else
                    {
                        TruLog(frmTonKhoTongLog, mTransit, MonID, DonViID, SLXoa - SLThem);
                    }
                }
                else if (SLThem > 0)
                {
                    ThemLog(frmTonKhoTongLog, mTransit, MonID, DonViID, SLThem);
                }
                else
                {
                    TruLog(frmTonKhoTongLog, mTransit, MonID, DonViID, SLXoa);
                }
                frmTonKhoTong.Commit();
            }
        }

        public void XoaLog()
        {

        }

        #region Bán hàng

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">Data.BOChiTietBanHang</param>
        /// <param name="transit">Dùng để biết trừ kho nào</param>
        /// <param name="Kara">KaraokeEntities</param>
        public void TonKho_Them(Data.BOChiTietBanHang item, Transit transit, KaraokeEntities Kara)
        {
            IQueryable<TONKHO> lsArray = Kara.TONKHOes.Where(s => s.MonID == item.MenuKichThuocMon.MonID && s.KhoID == transit.KhoID && s.SoLuongTon > 0).OrderBy(s => s.NgayHetHan);
            Data.TONKHOTONG tonkhotong = Kara.TONKHOTONGs.Where(s => s.MonID == item.MenuKichThuocMon.MonID && s.KhoID == transit.KhoID).FirstOrDefault();
            tonkhotong.SoLuongBan += item.ChiTietBanHang.KichThuocLoaiBan;
            tonkhotong.SoLuongTon -= item.ChiTietBanHang.KichThuocLoaiBan;
            int SL = item.ChiTietBanHang.KichThuocLoaiBan;
            foreach (var l in lsArray)
            {
                if (SL != 0)
                {
                    if (SL > l.SoLuongTon)
                    {
                        SL = SL - (int)l.SoLuongTon;
                        item.ChiTietBanHang.TONKHOCHITIETBANHANGs.Add(
                        new TONKHOCHITIETBANHANG()
                            {
                                KhoID = transit.KhoID,
                                TonKhoID = l.TonKhoID,
                                MonID = item.MenuKichThuocMon.MonID,
                                DonViID = item.MenuKichThuocMon.DonViID,
                                SoLuong = l.SoLuongTon
                            });
                        l.SoLuongTon = 0;
                    }
                    else
                    {
                        l.SoLuongTon = l.SoLuongTon - SL;
                        item.ChiTietBanHang.TONKHOCHITIETBANHANGs.Add(new TONKHOCHITIETBANHANG()
                        {
                            KhoID = transit.KhoID,
                            TonKhoID = l.TonKhoID,
                            MonID = item.MenuKichThuocMon.MonID,
                            DonViID = item.MenuKichThuocMon.DonViID,
                            SoLuong = SL
                        });
                        SL = 0;
                    }
                }
                else
                    break;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">Data.BOChiTietBanHang - Truyền xuống item.ChiTietBanHang.KichThuocLoaiBan thực, Giả sử đang là 2 sửa thành 5 thì truyền xuống 5</param>
        /// <param name="transit">Dùng để biết trừ kho nào</param>
        /// <param name="Kara">KaraokeEntities</param>
        public void TonKho_CapNhat(Data.BOChiTietBanHang item, Transit transit, KaraokeEntities Kara)
        {
            TonKho_Xoa(item, transit, Kara);
            TonKho_Them(item, transit, Kara);
        }

        public void TonKho_Xoa(Data.BOChiTietBanHang item, Transit transit, KaraokeEntities Kara)
        {
            if (item.ChiTietBanHang.ChiTietBanHangID > 0)
            {
                IQueryable<TONKHOCHITIETBANHANG> lsArray = Kara.TONKHOCHITIETBANHANGs.Where(s => s.ChiTietBanHangID == item.ChiTietBanHang.ChiTietBanHangID);
                foreach (var line in lsArray)
                {
                    TONKHO tonKho = Kara.TONKHOes.Where(s => s.TonKhoID == line.TonKhoID).FirstOrDefault();
                    tonKho.SoLuongTon += (int)line.SoLuong;
                    Data.TONKHOTONG tonkhotong = Kara.TONKHOTONGs.Where(s => s.MonID == item.MenuKichThuocMon.MonID && s.KhoID == transit.KhoID).FirstOrDefault();
                    tonkhotong.SoLuongBan += (int)line.SoLuong;
                    tonkhotong.SoLuongTon -= (int)line.SoLuong;
                    Kara.TONKHOCHITIETBANHANGs.DeleteObject(line);
                }
            }
        }
        #endregion
    }
}
