using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBanHang
    {        
        public BANHANG BANHANG { get; set; }
        public List<BOChiTietBanHang> _ListChiTietBanHang { get; set; }        
        private Transit mTransit;

        public FrameworkRepository<BANHANG> frBanHang;
        public FrameworkRepository<CHITIETBANHANG> frChiTietBanHang;
        public FrameworkRepository<MENUKICHTHUOCMON> frMenuKichThuocMon;
        public FrameworkRepository<MENUMON> frMenuMon;
        public FrameworkRepository<LICHSUBANHANG> frLichSu;
        public FrameworkRepository<CHITIETLICHSUBANHANG> frChiTietLichSuBanHang;
        public BOBanHang(Transit tran)
        {                        
            mTransit = tran;
            
            frBanHang = new FrameworkRepository<Data.BANHANG>(mTransit.KaraokeEntities,mTransit.KaraokeEntities.BANHANGs);
            frChiTietBanHang = new FrameworkRepository<CHITIETBANHANG>(mTransit.KaraokeEntities,mTransit.KaraokeEntities.CHITIETBANHANGs);
            frMenuKichThuocMon = new FrameworkRepository<MENUKICHTHUOCMON>(mTransit.KaraokeEntities,mTransit.KaraokeEntities.MENUKICHTHUOCMONs);
            frMenuMon = new FrameworkRepository<MENUMON>(mTransit.KaraokeEntities,mTransit.KaraokeEntities.MENUMONs);
            frLichSu = new FrameworkRepository<LICHSUBANHANG>(mTransit.KaraokeEntities,mTransit.KaraokeEntities.LICHSUBANHANGs);
            frChiTietLichSuBanHang = new FrameworkRepository<CHITIETLICHSUBANHANG>(mTransit.KaraokeEntities,mTransit.KaraokeEntities.CHITIETLICHSUBANHANGs);
            
            _ListChiTietBanHang = new List<BOChiTietBanHang>();            
            LoadBanHang();
        }
        public static IQueryable<BANHANG> GetAllNotCompleted(Transit transit)
        {
            return FrameworkRepository<BANHANG>.QueryNoTracking(transit.KaraokeEntities.BANHANGs).Where(b=>b.TrangThaiID!=4);
        }
        private void LoadBanHang()
        {
            var list = frBanHang.Query().Where(o => o.BanID == mTransit.Ban.BanID && o.TrangThaiID != 4);
            if (list.Count()>0)
            {
                BANHANG = list.FirstOrDefault();                
                var listItem = BOChiTietBanHang.Query(BANHANG.BanHangID,this);
                foreach (BOChiTietBanHang item in listItem)
                {                    
                    item.SoLuongBanTam = (int)item.CHITIETBANHANG.SoLuongBan;
                    this.AddChiTietBanHang(item);
                }
            }
            else
            {
                BANHANG = new BANHANG();                
                BANHANG.NhanVienID = mTransit.NhanVien.NhanVienID;
                if (BANHANG.NhanVienID==0)
                {
                    BANHANG.NhanVienID = null;
                }
                BANHANG.BanID = mTransit.Ban.BanID;                                
                BANHANG.NgayBan = DateTime.Now;
                BANHANG.MaHoaDon = 1;
                BANHANG.TienMat = 0;                
                BANHANG.TheID = mTransit.The.TheID;
                BANHANG.TienThe = 0;
                BANHANG.TienTraLai = 0;
                BANHANG.TienGiam = 0;
                BANHANG.ChietKhau = 0;
                BANHANG.TienBo = 0;
                BANHANG.PhiDichVu = 0;
                BANHANG.TongTien = 0;
                BANHANG.TrangThaiID = 1;
                BANHANG.KhachHangID = mTransit.KhachHang.KhachHangID;
                BANHANG.TienKhacHang = 0;
            }

        }
        private bool KiemTraThayDoi()
        {            
            foreach (BOChiTietBanHang item in _ListChiTietBanHang)
            {
                if (item.XoaMon|| item.SoLuongBanTam!=item.CHITIETBANHANG.SoLuongBan || item.CHITIETBANHANG.ChiTietBanHangID==0)
                {
                    return true;
                }
            }
            return false;
        }        
        public int AddChiTietBanHang(BOChiTietBanHang chiTiet)
        {
            if (_ListChiTietBanHang.Count>0)
            {
                BOChiTietBanHang chitietLast=_ListChiTietBanHang.Last();
                if (chitietLast.CHITIETBANHANG.KichThuocMonID==chiTiet.CHITIETBANHANG.KichThuocMonID)
                {
                    chitietLast.ChangeQtyChiTietBanHang((int)(chitietLast.CHITIETBANHANG.SoLuongBan+chiTiet.CHITIETBANHANG.SoLuongBan));
                    return (int)chitietLast.CHITIETBANHANG.KichThuocMonID;
                }
            }
            chiTiet.CHITIETBANHANG.NhanVienID = this.BANHANG.NhanVienID;
            _ListChiTietBanHang.Add(chiTiet);
            return 0;
        }
        public int GuiNhaBep()
        {
            int lichSuBanHangId = 0;
            if (this.BANHANG.BanHangID==0)
            {                
                frBanHang.AddObject(this.BANHANG);
                frBanHang.Commit();
                LICHSUBANHANG lichsu = GetLichSuBanHang();
                frLichSu.AddObject(lichsu);
                frLichSu.Commit();
                lichSuBanHangId = lichsu.LichSuBanHangID;

                foreach (BOChiTietBanHang item in _ListChiTietBanHang)
                {
                    item.CHITIETBANHANG.BanHangID = BANHANG.BanHangID;
                    CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                    frChiTietBanHang.AddObject(item.CHITIETBANHANG);
                    frChiTietLichSuBanHang.AddObject(chitiet);
                }
                frChiTietBanHang.Commit();
                frChiTietLichSuBanHang.Commit();
            }
            else
            {
                frBanHang.Update(this.BANHANG);
                frBanHang.Commit();
                if (KiemTraThayDoi())
                {                    
                    LICHSUBANHANG lichsu = GetLichSuBanHang();
                    frLichSu.AddObject(lichsu);
                    frLichSu.Commit();
                    lichSuBanHangId = lichsu.LichSuBanHangID;
                    foreach (BOChiTietBanHang item in _ListChiTietBanHang)
                    {                        
                        if (item.CHITIETBANHANG.ChiTietBanHangID==0)
                        {
                            CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                            item.ChangeQtyChiTietLichSuBanHang(chitiet, (int)chitiet.SoLuong);
                            item.CHITIETBANHANG.BanHangID = this.BANHANG.BanHangID;
                            frChiTietBanHang.AddObject(item.CHITIETBANHANG);
                            frChiTietLichSuBanHang.AddObject(chitiet);
                        }
                        else if (item.XoaMon)
                        {
                            CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                            item.ChangeQtyChiTietLichSuBanHang(chitiet, 0 - (int)chitiet.SoLuong);                            
                            frChiTietLichSuBanHang.AddObject(chitiet);
                            frChiTietBanHang.DeleteObject(item.CHITIETBANHANG);
                        }
                        else if(item.SoLuongBanTam!=item.CHITIETBANHANG.SoLuongBan)
                        {
                            CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                            chitiet.SoLuong = item.CHITIETBANHANG.SoLuongBan - item.SoLuongBanTam;
                            chitiet.ThanhTien = chitiet.SoLuong * chitiet.GiaBan;
                            frChiTietLichSuBanHang.AddObject(chitiet);
                            frChiTietBanHang.Update(item.CHITIETBANHANG);
                        }                        
                    }                    
                    frChiTietBanHang.Commit();
                    frChiTietLichSuBanHang.Commit();
                }
            }
            return lichSuBanHangId;
        }
        public int TinhTien()
        {
            this.BANHANG.TrangThaiID = 4;
            frBanHang.Update(this.BANHANG);
            frBanHang.Commit();
            return this.BANHANG.BanHangID;
        }
        public decimal TongTien()
        {
            decimal tong = 0;
            foreach (var item in _ListChiTietBanHang)
            {
                if (item.IsDeleted==false)
                {
                    tong += (decimal)item.CHITIETBANHANG.ThanhTien;
                }
            }
            return tong;
        }
        private LICHSUBANHANG GetLichSuBanHang()
        {
            LICHSUBANHANG lichsu = new LICHSUBANHANG();
            lichsu.BanHangID = this.BANHANG.BanHangID;
            lichsu.NhanVienID = mTransit.NhanVien.NhanVienID;
            if (lichsu.NhanVienID==0)
            {
                lichsu.NhanVienID = null;
            }
            lichsu.NgayBan = DateTime.Now;
            lichsu.InNhaBep = false;
            return lichsu;
        }
        private CHITIETLICHSUBANHANG GetChiTietLichSuBanHang(BOChiTietBanHang item,LICHSUBANHANG lichsu)
        {            
            CHITIETLICHSUBANHANG chitiet = new CHITIETLICHSUBANHANG();
            chitiet.LichSuBanHangID = lichsu.LichSuBanHangID;
            chitiet.KichThuocMonID = item.CHITIETBANHANG.KichThuocMonID;
            chitiet.TonKhoID = item.CHITIETBANHANG.TonKhoID;
            chitiet.SoLuong = item.CHITIETBANHANG.SoLuongBan;
            chitiet.ThanhTien = item.CHITIETBANHANG.ThanhTien;
            chitiet.GiaBan = item.CHITIETBANHANG.GiaBan;
            chitiet.TrangThai = 0;
            return chitiet;
        }        
        
    }
}
