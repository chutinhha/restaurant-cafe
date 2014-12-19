using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOXuLyKho
    {
        private FrameworkRepository<KHO> frmKho = null;

        private FrameworkRepository<XULYKHOLOAI> frmXuLyKhoLoai = null;

        private FrameworkRepository<NHANVIEN> frmNhanVien = null;

        private FrameworkRepository<XULYKHO> frmXuLyKho = null;
        private FrameworkRepository<TONKHO> frmTonKho = null;
        private FrameworkRepository<TONKHOTONG> frmTonKhoTong = null;

        public BOXuLyKho(Data.Transit transit)
        {
            frmXuLyKho = new FrameworkRepository<XULYKHO>(transit.KaraokeEntities, transit.KaraokeEntities.XULYKHOes);
            frmKho = new FrameworkRepository<KHO>(transit.KaraokeEntities, transit.KaraokeEntities.KHOes);
            frmNhanVien = new FrameworkRepository<NHANVIEN>(transit.KaraokeEntities, transit.KaraokeEntities.NHANVIENs);
            frmXuLyKhoLoai = new FrameworkRepository<XULYKHOLOAI>(transit.KaraokeEntities, transit.KaraokeEntities.XULYKHOLOAIs);
            frmTonKho = new FrameworkRepository<TONKHO>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOes);
            frmTonKhoTong = new FrameworkRepository<TONKHOTONG>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOTONGs);
        }

        public BOXuLyKho()
        {
            XuLyKho = new XULYKHO();
            Kho = new KHO();
            NhanVien = new NHANVIEN();
        }

        public Data.KHO Kho { get; set; }

        public Data.NHANVIEN NhanVien { get; set; }

        public Data.XULYKHO XuLyKho { get; set; }

        public Data.XULYKHOLOAI Loai { get; set; }

        public IQueryable<BOXuLyKho> GetAll(Transit mTransit, DateTime dt)
        {
            return (from xlk in frmXuLyKho.Query()
                    join k in frmKho.Query() on xlk.KhoID equals k.KhoID
                    join nv in frmNhanVien.Query() on xlk.NhanVienID equals nv.NhanVienID
                    join l in frmXuLyKhoLoai.Query() on xlk.LoaiID equals l.ID
                    where xlk.ThoiGian.Value.Year == dt.Year && xlk.ThoiGian.Value.Month == dt.Month && xlk.ThoiGian.Value.Day == dt.Day
                    select new BOXuLyKho
                    {
                        XuLyKho = xlk,
                        Kho = k,
                        NhanVien = nv,
                        Loai = l
                    }
                        );
        }

        public void Luu(List<BOXuLyKho> lsArray, List<BOXuLyKho> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BOXuLyKho item in lsArray)
                {
                    if (item.XuLyKho.ChinhKhoID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, null, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BOXuLyKho item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            mTransit.KaraokeEntities.SaveChanges();
        }

        public int Them(BOXuLyKho item, List<BOXuLyKhoChiTiet> lsArray, Transit mTransit)
        {
            ThemMoi(item, lsArray, mTransit);
            frmXuLyKho.AddObject(item.XuLyKho);
            frmXuLyKho.Commit();
            return item.XuLyKho.ChinhKhoID;
        }

        private int ThemMoi(BOXuLyKho item, List<BOXuLyKhoChiTiet> lsArray, Transit mTransit)
        {
            if (lsArray != null)
            {
                List<Data.TONKHOTONG> lsTonKhoTong = new List<TONKHOTONG>();
                List<Data.TONKHO> lsTonKho = new List<TONKHO>();
                foreach (BOXuLyKhoChiTiet line in lsArray)
                {
                    switch (item.XuLyKho.LoaiID)
                    {
                        case 1:
                            break;
                        case 2:
                        case 3:
                            IQueryable<Data.TONKHO> lsArrayLine = frmTonKho.Query().Where(s => s.DonViID == line.TonKho.DonViID && s.MonID == line.TonKho.MonID && s.KhoID == item.XuLyKho.KhoID && s.SoLuongTon > 0).OrderBy(s => s.NgayHetHan).ThenBy(s => s.TonKhoID);
                            if (lsArrayLine.Count() > 0 && line.TonKho.SoLuongNhap < lsArrayLine.Sum(s => s.SoLuongTon))
                            {
                                foreach (var l in lsArrayLine)
                                {
                                    Data.TONKHOTONG tonkhotong = null;
                                    if (lsTonKhoTong.Exists(s => s.KhoID == item.XuLyKho.KhoID && s.MonID == line.TonKho.MonID && s.DonViID == line.TonKho.DonViID))
                                    {
                                        tonkhotong = lsTonKhoTong.Find(s => s.KhoID == item.XuLyKho.KhoID && s.MonID == line.TonKho.MonID && s.DonViID == line.TonKho.DonViID);
                                    }
                                    else
                                    {
                                        tonkhotong = KiemTraTonKhoTong(frmTonKhoTong, (int)item.XuLyKho.KhoID, (int)line.TonKho.MonID, (int)line.TonKho.DonViID);
                                        lsTonKhoTong.Add(tonkhotong);
                                    }
                                    tonkhotong.SoLuongTon -= line.TonKho.SoLuongNhap;
                                    if (item.XuLyKho.LoaiID == 2)
                                    {
                                        tonkhotong.SoLuongMat += line.TonKho.SoLuongNhap;
                                    }
                                    if (item.XuLyKho.LoaiID == 3)
                                    {
                                        tonkhotong.SoLuongHu += line.TonKho.SoLuongNhap;
                                    }

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

                                }
                            }
                            break;
                        default:
                            break;
                    }

                    line.XuLyKho = new XULYKHO();
                    line.XuLyKho.KhoID = item.XuLyKho.KhoID;
                    item.XuLyKho.XULYKHOCHITIETs.Add(line.XuLyKhoChiTiet);
                }
                item.XuLyKho.TongTien = lsArray.Sum(s => s.TonKho.SoLuongNhap * s.TonKho.GiaBan);
                foreach (TONKHO i in lsTonKho)
                {
                    frmTonKho.Update(i);
                }
                foreach (TONKHOTONG i in lsTonKhoTong)
                {
                    frmTonKhoTong.Update(i);
                }
            }
            return item.XuLyKho.ChinhKhoID;
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

        private int Sua(BOXuLyKho item, Transit mTransit)
        {
            item.XuLyKho.Edit = false;
            frmXuLyKho.Update(item.XuLyKho);
            return item.XuLyKho.ChinhKhoID;
        }

        private int Them(BOXuLyKho item, Transit mTransit)
        {
            frmXuLyKho.AddObject(item.XuLyKho);
            return item.XuLyKho.ChinhKhoID;
        }

        private int Xoa(BOXuLyKho item, Transit mTransit)
        {
            item.XuLyKho.Deleted = true;
            frmXuLyKho.Update(item.XuLyKho);
            return item.XuLyKho.ChinhKhoID;
        }
    }
}
