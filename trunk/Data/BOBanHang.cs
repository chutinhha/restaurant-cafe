using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBanHang
    {
        public BAN BAN { get; set; }
        public BANHANG BANHANG { get; set; }
        public KHACHHANG KHACHHANG { get; set; }
        private List<BOChiTietBanHang> mListChiTietBanHangDeleted;
        public List<BOChiTietBanHang> _ListChiTietBanHang { get; set; }        
        private Transit mTransit;
        private KaraokeEntities mKaraokeEntities;
        public FrameworkRepository<BANHANG> frBanHang;
        public FrameworkRepository<CHITIETBANHANG> frChiTietBanHang;
        public FrameworkRepository<MENUKICHTHUOCMON> frMenuKichThuocMon;
        public FrameworkRepository<MENUMON> frMenuMon;
        public FrameworkRepository<LICHSUBANHANG> frLichSu;
        public FrameworkRepository<CHITIETLICHSUBANHANG> frChiTietLichSuBanHang;
        public FrameworkRepository<KHACHHANG> frKhachHang;

        public string TenBan 
        {
            get { return BAN.TenBan; }
        }
        public string MaHoaDon 
        {
            get { return BANHANG.MaHoaDon; }
        }
        public string GioBan 
        {
            get { return Utilities.DateTimeConverter.ConvertTimeToString(BANHANG.NgayBan.Value); }
        }
        public string TongTienDaBan 
        {
            get { return Utilities.MoneyFormat.ConvertToString(BANHANG.TongTien); }
        }
        public BOBanHang()
        {            
            _ListChiTietBanHang = new List<BOChiTietBanHang>();
        }
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
            frKhachHang = new FrameworkRepository<Data.KHACHHANG>(mKaraokeEntities, mKaraokeEntities.KHACHHANGs);
            _ListChiTietBanHang = new List<BOChiTietBanHang>();
            mListChiTietBanHangDeleted = new List<BOChiTietBanHang>();
            //LoadBanHang();
        }
        public IQueryable<BANHANG> GetAllNotCompleted()
        {
            return frBanHang.Query().Where(b => b.TrangThaiID < 3);
        }
        public static IQueryable<BANHANG> GetAllNotCompleted(Transit transit)
        {
            return FrameworkRepository<BANHANG>.QueryNoTracking(transit.KaraokeEntities.BANHANGs).Where(b=>b.TrangThaiID<3);
        }
        public static IQueryable<BOBanHang> GetAllCompleted(KaraokeEntities kara,DateTime date)
        {
            return from a in FrameworkRepository<BANHANG>.QueryNoTracking(kara.BANHANGs).Where(
                       b => 
                           b.TrangThaiID == 4 &&
                           b.NgayBan.Value.Day==date.Day &&
                           b.NgayBan.Value.Month==date.Month&&
                           b.NgayBan.Value.Year==date.Year
                       )
                   join b in FrameworkRepository<BAN>.QueryNoTracking(kara.BANs) on a.BanID equals b.BanID
                   select new BOBanHang
                   {
                       BAN=b,
                       BANHANG=a
                   };
        }
        public IQueryable<BOBanHang> GetAllBanHang(BAN ban)
        {
            return from a in GetAllNotCompleted()
                   join b in frKhachHang.Query() on a.KhachHangID equals b.KhachHangID into b1
                   from c in b1.DefaultIfEmpty()
                   where a.BanID == ban.BanID
                   select new BOBanHang
                   {
                       BANHANG=a,
                       KHACHHANG=c
                   };
        }
        public void LoadChiTiet()
        {
            _ListChiTietBanHang.Clear();
            if (mKaraokeEntities==null)
            {
                mKaraokeEntities = new KaraokeEntities();
            }
            var listItem = BOChiTietBanHang.Query(BANHANG.BanHangID, mKaraokeEntities);
            foreach (BOChiTietBanHang item in listItem)
            {
                item.SoLuongBanTam = (int)item.ChiTietBanHang.SoLuongBan;
                this.AddChiTietBanHang(item);
            }
        }
        public void LoadBanHang(BAN ban)
        {
            BAN = ban;
            //var list = frBanHang.Query().Where(o => o.BanID == ban.BanID && o.TrangThaiID < 3).ToList();
            BOBanHang bh = GetAllBanHang(ban).FirstOrDefault();
            if (bh!=null)
            {
                //BANHANG = list.FirstOrDefault();
                BANHANG = bh.BANHANG;
                KHACHHANG = bh.KHACHHANG;
                LoadChiTiet();
            }
            else
            {
                BANHANG = new BANHANG();
                BANHANG.NhanVienID = mTransit.NhanVien.NhanVienID;
                if (BANHANG.NhanVienID == 0)
                {
                    BANHANG.NhanVienID = null;
                }
                BANHANG.BanID = ban.BanID;
                BANHANG.NgayBan = DateTime.Now;
                BANHANG.MaHoaDon = String.Format("HD{0:000000}", 1);
                BANHANG.TienMat = 0;
                //BANHANG.TheID = mTransit.The.TheID;
                BANHANG.TienThe = 0;
                BANHANG.TienTraLai = 0;
                BANHANG.GiamGia = 0;
                BANHANG.ChietKhau = 0;
                BANHANG.TienBo = 0;
                BANHANG.PhiDichVu = 0;
                BANHANG.TongTien = 0;
                BANHANG.TrangThaiID = 1;                                
                BANHANG.TienKhacHang = 0;
            }

        }
        public void LoadBanHang()
        {
            LoadBanHang(mTransit.Ban);
        }
        private bool KiemTraThayDoiLichSu()
        {            
            foreach (BOChiTietBanHang item in _ListChiTietBanHang)
            {
                if (
                    item.SoLuongBanTam!=item.ChiTietBanHang.SoLuongBan || 
                    item.ChiTietBanHang.ChiTietBanHangID==0                     
                    )
                {
                    return true;
                }
            }
            return false;
        }
        private bool KiemTraThayDoi()
        {
            foreach (BOChiTietBanHang item in _ListChiTietBanHang)
            {
                if (
                    item.IsChanged
                    )
                {
                    return true;
                }
            }
            return false;
        }
        private BOChiTietBanHang KiemTraTonTai(BOChiTietBanHang chiTiet)
        {
            foreach (var item in _ListChiTietBanHang)
            {
                if (chiTiet.ChiTietBanHang.KichThuocMonID==item.ChiTietBanHang.KichThuocMonID)
                {
                    return item;
                }
            }
            return null;
        }
        public int AddChiTietBanHang(BOChiTietBanHang chiTiet)
        {
            if (_ListChiTietBanHang.Count>0)
            {
                BOChiTietBanHang chitietLast=_ListChiTietBanHang.Last();
                if (chitietLast.ChiTietBanHang.KichThuocMonID==chiTiet.ChiTietBanHang.KichThuocMonID)
                {
                    chitietLast.ChangeQtyChiTietBanHang((int)(chitietLast.ChiTietBanHang.SoLuongBan+chiTiet.ChiTietBanHang.SoLuongBan));
                    return (int)chitietLast.ChiTietBanHang.KichThuocMonID;
                }
            }
            var item = KiemTraTonTai(chiTiet);
            chiTiet.ChiTietBanHang.NhanVienID = this.BANHANG.NhanVienID;
            _ListChiTietBanHang.Add(chiTiet);
            if (item!=null)
            {
                chiTiet.ChangePriceChiTietBanHang(item.ChiTietBanHang.GiaBan);
                return (int)item.ChiTietBanHang.KichThuocMonID;
            }
            return 0;
        }
        public void DeleteChiTietBanHang(BOChiTietBanHang chitiet)
        {
            if (chitiet.ChiTietBanHang.ChiTietBanHangID>0)
            {
                mListChiTietBanHangDeleted.Add(chitiet);
            }
            _ListChiTietBanHang.Remove(chitiet);
        }
        public void XoaAllXoaChiTietBanHang()
        {
            foreach (var item in _ListChiTietBanHang)
            {
                if (item.ChiTietBanHang.ChiTietBanHangID>0)
                {
                    mListChiTietBanHangDeleted.Add(item);
                }
            }
            _ListChiTietBanHang.Clear();
        }
        public int DongBan()
        {            
            if (this.BANHANG.TrangThaiID==3)
            {
                this.BANHANG.TrangThaiID = 4;
                frBanHang.Update(this.BANHANG);
                frBanHang.Commit();
                return this.BANHANG.BanHangID;
            }
            return 0;
        }
        public int GuiNhaBep()
        {
            int lichSuBanHangId = 0;
            if (this.BANHANG.BanHangID == 0)
            {
                frBanHang.AddObject(this.BANHANG);
                frBanHang.Commit();
                LICHSUBANHANG lichsu = GetLichSuBanHang();
                frLichSu.AddObject(lichsu);
                frLichSu.Commit();
                lichSuBanHangId = lichsu.LichSuBanHangID;

                foreach (BOChiTietBanHang item in _ListChiTietBanHang)
                {
                    item.ChiTietBanHang.BanHangID = BANHANG.BanHangID;
                    CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                    frChiTietBanHang.AddObject(item.ChiTietBanHang);
                    frChiTietLichSuBanHang.AddObject(chitiet);                                        
                }
                frChiTietBanHang.Commit();
                frChiTietLichSuBanHang.Commit();                
            }
            else
            {
                frBanHang.Update(this.BANHANG);
                frBanHang.Commit();
                if (KiemTraThayDoiLichSu())
                {
                    LICHSUBANHANG lichsu = GetLichSuBanHang();
                    frLichSu.AddObject(lichsu);
                    frLichSu.Commit();
                    lichSuBanHangId = lichsu.LichSuBanHangID;
                    foreach (BOChiTietBanHang item in _ListChiTietBanHang)
                    {
                        if (item.ChiTietBanHang.ChiTietBanHangID == 0)
                        {
                            CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                            item.ChangeQtyChiTietLichSuBanHang(chitiet, (int)chitiet.SoLuong);
                            item.ChiTietBanHang.BanHangID = this.BANHANG.BanHangID;
                            frChiTietBanHang.AddObject(item.ChiTietBanHang);
                            frChiTietLichSuBanHang.AddObject(chitiet);
                        }
                        else if (item.SoLuongBanTam != item.ChiTietBanHang.SoLuongBan)
                        {
                            CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                            chitiet.SoLuong = item.ChiTietBanHang.SoLuongBan - item.SoLuongBanTam;
                            chitiet.ThanhTien = chitiet.SoLuong * chitiet.GiaBan;
                            frChiTietLichSuBanHang.AddObject(chitiet);
                            frChiTietBanHang.Update(item.ChiTietBanHang);
                        }
                    }
                    foreach (BOChiTietBanHang item in mListChiTietBanHangDeleted)
                    {
                        if (item.ChiTietBanHang.ChiTietBanHangID > 0)
                        {
                            CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                            item.ChangeQtyChiTietLichSuBanHang(chitiet, 0 - (int)chitiet.SoLuong);
                            frChiTietLichSuBanHang.AddObject(chitiet);
                            frChiTietBanHang.DeleteObject(item.ChiTietBanHang);
                        }
                    }
                    mListChiTietBanHangDeleted.Clear();
                    frChiTietBanHang.Commit();
                    frChiTietLichSuBanHang.Commit();
                }
                //========================== 
                //thay doi
                foreach (BOChiTietBanHang item in _ListChiTietBanHang)
                {
                    if (item.IsChanged)
                    {
                        frChiTietBanHang.Update(item.ChiTietBanHang);
                    }
                }
                frChiTietBanHang.Commit();                              
            }
            return lichSuBanHangId;
        }        
        public int TinhTien()
        {
            if (this.BANHANG.TrangThaiID==1 || this.BANHANG.TrangThaiID==2)
            {
                this.BANHANG.TrangThaiID = 4;
                frBanHang.Update(this.BANHANG);
                frBanHang.Commit();
                return this.BANHANG.BanHangID;
            }
            return 0;
        }
        public int TamTinh()
        {
            if (this.BANHANG.TrangThaiID == 1 || this.BANHANG.TrangThaiID == 2)
            {
                this.BANHANG.TrangThaiID = 2;
                frBanHang.Update(this.BANHANG);
                frBanHang.Commit();
                return this.BANHANG.BanHangID;
            }
            return 0;
        }
        public int HuyBan()
        {
            if (this.BANHANG.TrangThaiID == 1 || this.BANHANG.TrangThaiID == 2)
            {
                this.BANHANG.TrangThaiID = 8;
                frBanHang.Update(this.BANHANG);
                frBanHang.Commit();
                return this.BANHANG.BanHangID;
            }
            return 0;
        }
        public void ChuyenBan(BAN ban)
        {            
            Nullable<int> trangThai = this.BANHANG.TrangThaiID;
            //chuyen ban
            this.BANHANG.TrangThaiID = 5;
            frBanHang.Update(this.BANHANG);
            frBanHang.Commit();

            //ban moi
            this.BANHANG.TrangThaiID = trangThai;
            this.BANHANG.BanID = ban.BanID;
            this.BANHANG.BanHangID = 0;            
            foreach (var item in _ListChiTietBanHang)
            {
                item.ChiTietBanHang.ChiTietBanHangID = 0;                
            }
            GuiNhaBep();

        }
        public void TachBan(BOTachGopBan tachban)
        {
            Nullable<int> trangThai = this.BANHANG.TrangThaiID;
            this.BANHANG.TrangThaiID = 7;
            frBanHang.Update(this.BANHANG);
            frBanHang.Commit();
            this.BANHANG.TrangThaiID = trangThai;
            this.BANHANG.BanHangID = 0;
            foreach (var bahang in tachban._ListBan)
            {
                bahang.BANHANG.BanHangID = 0;                
                foreach (var item in bahang._ListChiTietBanHang)
                {
                    item.ChiTietBanHang.ChiTietBanHangID = 0;                    
                }
                if (bahang._ListChiTietBanHang.Count>0)
                {
                    bahang.GuiNhaBep();
                }
            }
            if (this._ListChiTietBanHang.Count>0)
            {
                this.GuiNhaBep();
            }
        }
        public void GopBan(BOTachGopBan gopban)
        {
            if (this.BANHANG.BanHangID>0)
            {
                Nullable<int> trangthaiID=this.BANHANG.TrangThaiID;
                this.BANHANG.TrangThaiID = 6;
                frBanHang.Update(this.BANHANG);
                frBanHang.Commit();
                this.BANHANG.TrangThaiID = trangthaiID;
            }
            this.BANHANG.BanHangID = 0;            
            foreach (var banhang in gopban._ListBan)
            {
                banhang.BANHANG.TrangThaiID = 6;
                frBanHang.Update(banhang.BANHANG);
                foreach (var item in banhang._ListChiTietBanHang)
                {
                    item.ChiTietBanHang.ChiTietBanHangID = 0;
                    this.AddChiTietBanHang(item);
                }
            }
            GuiNhaBep();
        }
        public decimal SoTienPhaiTra()
        {
            decimal tong = this.TongTien();
            decimal tienGiam = tong * this.BANHANG.GiamGia / 100;
            return tong - tienGiam;
        }
        public decimal TongTien()
        {
            decimal tong = 0;
            foreach (var item in _ListChiTietBanHang)
            {
                tong += item.ChiTietBanHang.ThanhTien;
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
            chitiet.KichThuocMonID = item.ChiTietBanHang.KichThuocMonID;            
            chitiet.SoLuong = item.ChiTietBanHang.SoLuongBan;
            chitiet.ThanhTien = item.ChiTietBanHang.ThanhTien;
            chitiet.GiaBan = item.ChiTietBanHang.GiaBan;
            chitiet.GiamGia = item.ChiTietBanHang.GiamGia;
            chitiet.TrangThai = 0;
            return chitiet;
        }        
        
    }
}
