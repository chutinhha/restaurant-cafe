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
        private KaraokeEntities mKaraokeEntities;
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
            mKaraokeEntities = new KaraokeEntities();
            frBanHang = new FrameworkRepository<Data.BANHANG>(mKaraokeEntities,mKaraokeEntities.BANHANGs);
            frChiTietBanHang = new FrameworkRepository<CHITIETBANHANG>(mKaraokeEntities,mKaraokeEntities.CHITIETBANHANGs);
            frMenuKichThuocMon = new FrameworkRepository<MENUKICHTHUOCMON>(mKaraokeEntities,mKaraokeEntities.MENUKICHTHUOCMONs);
            frMenuMon = new FrameworkRepository<MENUMON>(mKaraokeEntities,mKaraokeEntities.MENUMONs);
            frLichSu = new FrameworkRepository<LICHSUBANHANG>(mKaraokeEntities,mKaraokeEntities.LICHSUBANHANGs);
            frChiTietLichSuBanHang = new FrameworkRepository<CHITIETLICHSUBANHANG>(mKaraokeEntities,mKaraokeEntities.CHITIETLICHSUBANHANGs);
            
            _ListChiTietBanHang = new List<BOChiTietBanHang>();            
            LoadBanHang();
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
                if (item.XoaMon||(item.SoLuongBanTam!=item.CHITIETBANHANG.SoLuongBan))
                {
                    return true;
                }
            }
            return false;
        }        
        public void AddChiTietBanHang(BOChiTietBanHang chiTiet)
        {
            chiTiet.CHITIETBANHANG.NhanVienID = this.BANHANG.NhanVienID;
            _ListChiTietBanHang.Add(chiTiet);
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
                            chitiet.ThanhTien = chitiet.SoLuong * chitiet.GiaBan;
                            frChiTietBanHang.AddObject(item.CHITIETBANHANG);
                            frChiTietLichSuBanHang.AddObject(chitiet);
                        }
                        else if (item.XoaMon)
                        {
                            CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                            chitiet.SoLuong = 0 - chitiet.SoLuong;
                            chitiet.ThanhTien = chitiet.SoLuong * chitiet.GiaBan;
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
        public double TongTien()
        {
            double tong = 0;
            foreach (var item in _ListChiTietBanHang)
            {
                if (item.IsDeleted==false)
                {
                    tong += (double)item.CHITIETBANHANG.ThanhTien;
                }
            }
            return tong;
        }
        private LICHSUBANHANG GetLichSuBanHang()
        {
            LICHSUBANHANG lichsu = new LICHSUBANHANG();
            lichsu.BanHangID = this.BANHANG.BanHangID;
            lichsu.NhanVienID = mTransit.NhanVien.NhanVienID;
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
