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
        public BOBanHang(Transit tran)
        {                        
            mTransit = tran;
            mKaraokeEntities = new KaraokeEntities();
            mKaraokeEntities.BANHANGs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            mKaraokeEntities.CHITIETBANHANGs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            mKaraokeEntities.MENUKICHTHUOCMONs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            mKaraokeEntities.MENUMONs.MergeOption = System.Data.Objects.MergeOption.NoTracking;                

            mKaraokeEntities.CHITIETBANHANGs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            _ListChiTietBanHang = new List<BOChiTietBanHang>();            
            LoadBanHang();
        }        
        private void LoadBanHang()
        {
            var list = mKaraokeEntities.BANHANGs.Where(o => o.BanID == mTransit.Ban.BanID && o.TrangThaiID != 4);
            if (list.Count()>0)
            {
                BANHANG = list.FirstOrDefault();
                mKaraokeEntities.Attach(BANHANG);
                var listItem = BOChiTietBanHang.getAll(BANHANG.BanHangID, mKaraokeEntities);
                foreach (BOChiTietBanHang item in listItem)
                {
                    mKaraokeEntities.Attach(item.CHITIETBANHANG);
                    item.SoLuongBanTam = (int)item.CHITIETBANHANG.SoLuongBan;
                    this.AddChiTietBanHang(item);
                }
            }
            else
            {
                BANHANG = new BANHANG();
                //BANHANG.NHANVIEN = mTransit.NhanVien;
                BANHANG.NhanVienID = mTransit.NhanVien.NhanVienID;
                //BANHANG.BAN = mTransit.Ban;
                BANHANG.BanID = mTransit.Ban.BanID;                
                BANHANG.TrangThaiID = 1;
                BANHANG.NgayBan = DateTime.Now;
                BANHANG.MaHoaDon = 1;
                BANHANG.TienMat = 0;
                //BANHANG.THE = mTransit.The;
                BANHANG.TheID = mTransit.The.TheID;
                BANHANG.TienThe = 0;
                BANHANG.TienTraLai = 0;
                BANHANG.TienGiam = 0;
                BANHANG.ChietKhau = 0;
                BANHANG.TienBo = 0;
                BANHANG.PhiDichVu = 0;
                BANHANG.TongTien = 0;
                //BANHANG.KHACHHANG = mTransit.KhachHang;
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
            _ListChiTietBanHang.Add(chiTiet);
        }
        public void GuiNhaBep()
        {            
            if (this.BANHANG.BanHangID==0)
            {
                mKaraokeEntities.BANHANGs.AddObject(this.BANHANG);                
                mKaraokeEntities.SaveChanges();
                LICHSUBANHANG lichsu = GetLichSuBanHang();
                mKaraokeEntities.LICHSUBANHANGs.AddObject(lichsu);
                mKaraokeEntities.SaveChanges();

                foreach (BOChiTietBanHang item in _ListChiTietBanHang)
                {
                    item.CHITIETBANHANG.BanHangID = BANHANG.BanHangID;
                    CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                    mKaraokeEntities.CHITIETBANHANGs.AddObject(item.CHITIETBANHANG);
                    mKaraokeEntities.CHITIETLICHSUBANHANGs.AddObject(chitiet);
                }
                mKaraokeEntities.SaveChanges();
            }
            else
            {
                if (KiemTraThayDoi())
                {                    
                    LICHSUBANHANG lichsu = GetLichSuBanHang();
                    mKaraokeEntities.LICHSUBANHANGs.AddObject(lichsu);
                    mKaraokeEntities.SaveChanges();                    
                    foreach (BOChiTietBanHang item in _ListChiTietBanHang)
                    {                        
                        if (item.CHITIETBANHANG.ChiTietBanHangID==0)
                        {
                            mKaraokeEntities.CHITIETBANHANGs.AddObject(item.CHITIETBANHANG);
                        }
                        if (item.XoaMon)
                        {
                            CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                            chitiet.SoLuong = 0 - chitiet.SoLuong;
                            chitiet.ThanhTien = chitiet.SoLuong * chitiet.GiaBan;
                            mKaraokeEntities.CHITIETLICHSUBANHANGs.AddObject(chitiet);
                            mKaraokeEntities.CHITIETBANHANGs.DeleteObject(item.CHITIETBANHANG);
                        }
                        else if(item.SoLuongBanTam!=item.CHITIETBANHANG.SoLuongBan)
                        {
                            CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                            chitiet.SoLuong = item.CHITIETBANHANG.SoLuongBan - item.SoLuongBanTam;
                            chitiet.ThanhTien = chitiet.SoLuong * chitiet.GiaBan;
                            mKaraokeEntities.CHITIETLICHSUBANHANGs.AddObject(chitiet);                            
                        }                        
                    }                                        
                    mKaraokeEntities.SaveChanges();
                }
            }            
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
