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
        private CAIDATBANHANG _CAIDATBANHANG;
        private Transit mTransit;
        private KaraokeEntities mKaraokeEntities;        

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
            get { return Utilities.DateTimeConverter.ConvertToTimeString(BANHANG.NgayBan.Value); }
        }
        public string TongTienDaBan 
        {
            get { return Utilities.MoneyFormat.ConvertToString(BANHANG.TongTien-BANHANG.GiamGia*BANHANG.TongTien/100); }
        }
        public BOBanHang()
        {            
            _ListChiTietBanHang = new List<BOChiTietBanHang>();
        }
        public BOBanHang(Transit tran,KaraokeEntities kara)
        {                        
            mTransit = tran;
            mKaraokeEntities = kara;
            _CAIDATBANHANG = mKaraokeEntities.CAIDATBANHANGs.FirstOrDefault();
            if (_CAIDATBANHANG==null)
            {
                _CAIDATBANHANG = new CAIDATBANHANG();
            }
            _ListChiTietBanHang = new List<BOChiTietBanHang>();
            mListChiTietBanHangDeleted = new List<BOChiTietBanHang>();
            //LoadBanHang();
        }
        public IQueryable<BANHANG> GetAllNotCompleted()
        {
            return mKaraokeEntities.BANHANGs.Where(b => b.TrangThaiID < 3);
        }
        public static IQueryable<BANHANG> GetAllNotCompleted(KaraokeEntities kara)
        {
            return kara.BANHANGs.Where(b=>b.TrangThaiID<3);
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
                   join b in kara.BANs on a.BanID equals b.BanID
                   select new BOBanHang
                   {
                       BAN=b,
                       BANHANG=a
                   };
        }
        public IQueryable<BOBanHang> GetAllBanHang(BAN ban)
        {
            return from a in GetAllNotCompleted()
                   join b in mKaraokeEntities.KHACHHANGs on a.KhachHangID equals b.KhachHangID into b1
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
            var listItem = BOChiTietBanHang.Query(BANHANG, mKaraokeEntities);
            foreach (BOChiTietBanHang item in listItem)
            {
                item.SoLuongBanTam = item.ChiTietBanHang.SoLuongBan;
                this.AddChiTietBanHang(item);
                var listKM = BOChiTietBanHang.QueryKhuyenMai(item.ChiTietBanHang, mKaraokeEntities);
                foreach (var km in listKM)
                {
                    item._ListKhuyenMai.Add(km);
                }
            }
        }
        public void LoadBanHang(BAN ban)
        {
            BAN = ban;            
            BOBanHang bh = GetAllBanHang(ban).FirstOrDefault();
            if (bh!=null)
            {                
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
                BANHANG.MaHoaDon = String.Format("HD-{0:00}-{1:000000}", mTransit.ThamSo.SoMay, mTransit.ThamSo.ThuTuMaHoaDon);                
                BANHANG.TrangThaiID = 1;
                if (_CAIDATBANHANG.ChoPhepPhiDichVu)
                {
                    BANHANG.PhiDichVu = _CAIDATBANHANG.PhiDichVu;
                }
                if (_CAIDATBANHANG.ChoPhepThueVAT)
                {
                    BANHANG.ThueVAT = _CAIDATBANHANG.ThueVAT;
                }
            }

        }
        public void LoadBanHang()
        {
            LoadBanHang(mTransit.Ban);
        }
        private bool KiemTraThayDoiLichSu()
        {
            if (mListChiTietBanHangDeleted.Count>0)
            {
                return true;
            }
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
            GetKhuyenMai(chiTiet);
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
                mKaraokeEntities.SaveChanges();
                return this.BANHANG.BanHangID;
            }
            return 0;
        }
        public int GuiNhaBep()
        {            
            LICHSUBANHANG lichsu = GetLichSuBanHang();
            if (this.BANHANG.BanHangID == 0)
            {
                mKaraokeEntities.BANHANGs.AddObject(this.BANHANG);                                                
                this.BANHANG.LICHSUBANHANGs.Add(lichsu);                                
                foreach (BOChiTietBanHang item in _ListChiTietBanHang)
                {                    
                    CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                    lichsu.CHITIETLICHSUBANHANGs.Add(chitiet);
                    this.BANHANG.CHITIETBANHANGs.Add(item.ChiTietBanHang);                    
                    foreach (var km in item._ListKhuyenMai)
                    {
                        CHITIETLICHSUBANHANG kmChiTiet = GetChiTietLichSuBanHang(km,lichsu);
                        item.ChiTietBanHang.CHITIETBANHANG1.Add(km.ChiTietBanHang);
                        chitiet.CHITIETLICHSUBANHANG1.Add(kmChiTiet);
                        //km.ChangeQtyChiTietBanHang(item.ChiTietBanHang.SoLuongBan);
                        km.ChangeQtyChiTietLichSuBanHang(kmChiTiet, chitiet.SoLuong);
                        this.BANHANG.CHITIETBANHANGs.Add(km.ChiTietBanHang);
                        lichsu.CHITIETLICHSUBANHANGs.Add(kmChiTiet);
                    }
                }                
            }
            else
            {                
                if (KiemTraThayDoiLichSu())
                {                    
                    this.BANHANG.LICHSUBANHANGs.Add(lichsu);                    
                    foreach (BOChiTietBanHang item in _ListChiTietBanHang)
                    {
                        CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                        if (item.ChiTietBanHang.ChiTietBanHangID == 0)
                        {                            
                            item.ChangeQtyChiTietLichSuBanHang(chitiet, (int)chitiet.SoLuong);
                            this.BANHANG.CHITIETBANHANGs.Add(item.ChiTietBanHang);
                            lichsu.CHITIETLICHSUBANHANGs.Add(chitiet);
                            foreach (var km in item._ListKhuyenMai)
                            {
                                CHITIETLICHSUBANHANG kmChiTiet = GetChiTietLichSuBanHang(km, lichsu);
                                //km.ChangeQtyChiTietBanHang(item.ChiTietBanHang.SoLuongBan);
                                km.ChangeQtyChiTietLichSuBanHang(kmChiTiet, chitiet.SoLuong);
                                item.ChiTietBanHang.CHITIETBANHANG1.Add(km.ChiTietBanHang);
                                chitiet.CHITIETLICHSUBANHANG1.Add(kmChiTiet);
                                this.BANHANG.CHITIETBANHANGs.Add(km.ChiTietBanHang);
                                lichsu.CHITIETLICHSUBANHANGs.Add(kmChiTiet);
                            }
                        }
                        else if (item.SoLuongBanTam != item.ChiTietBanHang.SoLuongBan)
                        {                            
                            item.ChangeQtyChiTietLichSuBanHang(chitiet,item.ChiTietBanHang.SoLuongBan - item.SoLuongBanTam);
                            lichsu.CHITIETLICHSUBANHANGs.Add(chitiet);
                            foreach (var km in item._ListKhuyenMai)
                            {
                                CHITIETLICHSUBANHANG kmChiTiet = GetChiTietLichSuBanHang(km, lichsu);
                                //km.ChangeQtyChiTietBanHang(item.ChiTietBanHang.SoLuongBan);
                                km.ChangeQtyChiTietLichSuBanHang(kmChiTiet, chitiet.SoLuong);                                
                                chitiet.CHITIETLICHSUBANHANG1.Add(kmChiTiet);
                                lichsu.CHITIETLICHSUBANHANGs.Add(kmChiTiet);
                            }
                        }                        
                    }
                    foreach (BOChiTietBanHang item in mListChiTietBanHangDeleted)
                    {
                        if (item.ChiTietBanHang.ChiTietBanHangID > 0)
                        {
                            CHITIETLICHSUBANHANG chitiet = GetChiTietLichSuBanHang(item, lichsu);
                            item.ChangeQtyChiTietLichSuBanHang(chitiet, 0 - (int)chitiet.SoLuong);
                            lichsu.CHITIETLICHSUBANHANGs.Add(chitiet);
                            foreach (var km in item._ListKhuyenMai)
                            {
                                CHITIETLICHSUBANHANG kmChiTiet = GetChiTietLichSuBanHang(km, lichsu);
                                //km.ChangeQtyChiTietBanHang(item.ChiTietBanHang.SoLuongBan);
                                km.ChangeQtyChiTietLichSuBanHang(kmChiTiet, chitiet.SoLuong);
                                chitiet.CHITIETLICHSUBANHANG1.Add(kmChiTiet);
                                lichsu.CHITIETLICHSUBANHANGs.Add(kmChiTiet);
                                mKaraokeEntities.CHITIETBANHANGs.DeleteObject(km.ChiTietBanHang);
                            }
                            mKaraokeEntities.CHITIETBANHANGs.DeleteObject(item.ChiTietBanHang);
                        }
                    }
                    mListChiTietBanHangDeleted.Clear();                                
                }                                                 
            }
            mKaraokeEntities.SaveChanges();
            return lichsu.LichSuBanHangID;
        }        
        public int TinhTien()
        {
            if (this.BANHANG.TrangThaiID==1 || this.BANHANG.TrangThaiID==2)
            {
                this.BANHANG.TrangThaiID = 4;
                this.BANHANG.NgayBan = DateTime.Now;
                mKaraokeEntities.SaveChanges();
                return this.BANHANG.BanHangID;
            }
            return 0;
        }
        public int TamTinh()
        {
            if (this.BANHANG.TrangThaiID == 1 || this.BANHANG.TrangThaiID == 2)
            {
                this.BANHANG.TrangThaiID = 2;
                mKaraokeEntities.SaveChanges();
                return this.BANHANG.BanHangID;
            }
            return 0;
        }
        public int HuyBan()
        {
            if (this.BANHANG.TrangThaiID == 1 || this.BANHANG.TrangThaiID == 2)
            {
                this.BANHANG.TrangThaiID = 8;
                this.BANHANG.NhanVienID = mTransit.NhanVien.NhanVienID;
                this.BANHANG.NgayBan = DateTime.Now;
                mKaraokeEntities.SaveChanges();
                return this.BANHANG.BanHangID;
            }
            return 0;
        }
        public void ChuyenBan(BAN ban)
        {            
            Nullable<int> trangThai = this.BANHANG.TrangThaiID;
            //chuyen ban
            this.BANHANG.TrangThaiID = 5;
            mKaraokeEntities.SaveChanges();

            //ban moi
            mKaraokeEntities.BANHANGs.Detach(this.BANHANG);
            this.BANHANG.TrangThaiID = trangThai;
            this.BANHANG.BanID = ban.BanID;            
            this.BANHANG.BanHangID = 0;            
            foreach (var item in _ListChiTietBanHang)
            {
                mKaraokeEntities.CHITIETBANHANGs.Detach(item.ChiTietBanHang);
                item.ChiTietBanHang.ChiTietBanHangID = 0;                
            }
            GuiNhaBep();

        }
        public void TachBan(BOTachGopBan tachban)
        {
            Nullable<int> trangThai = this.BANHANG.TrangThaiID;
            this.BANHANG.TrangThaiID = 7;
            mKaraokeEntities.SaveChanges();
            mKaraokeEntities.BANHANGs.Detach(this.BANHANG);
            this.BANHANG.TrangThaiID = trangThai;
            this.BANHANG.BanHangID = 0;
            foreach (var bahang in tachban._ListBan)
            {
                bahang.BANHANG.BanHangID = 0;                
                foreach (var item in bahang._ListChiTietBanHang)
                {
                    mKaraokeEntities.CHITIETBANHANGs.Detach(item.ChiTietBanHang);
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
                mKaraokeEntities.SaveChanges();
                mKaraokeEntities.BANHANGs.Detach(this.BANHANG);
                this.BANHANG.TrangThaiID = trangthaiID;
            }            
            this.BANHANG.BanHangID = 0;            
            foreach (var banhang in gopban._ListBan)
            {
                banhang.BANHANG.TrangThaiID = 6;
                mKaraokeEntities.SaveChanges();
                foreach (var item in banhang._ListChiTietBanHang)
                {
                    if (item.ChiTietBanHang.ChiTietBanHangID>0)
                    {
                        mKaraokeEntities.CHITIETBANHANGs.Detach(item.ChiTietBanHang);
                        item.ChiTietBanHang.ChiTietBanHangID = 0;
                    }
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
        private List<BOChiTietBanHang> GetKhuyenMai(BOChiTietBanHang chititet)
        {
            var query = from a in mKaraokeEntities.MENUMONs
                        join b in mKaraokeEntities.MENUKICHTHUOCMONs  on a.MonID equals b.MonID 
                        join c in mKaraokeEntities.MENUKHUYENMAIs.Where(o=>o.Visual==true && o.Deleted==false) on b.KichThuocMonID equals c.KichThuocMonTang
                        where c.KichThuocMonID == chititet.MenuKichThuocMon.KichThuocMonID
                        select new BOMenuKichThuocMon
                        {
                            MenuKichThuocMon=b,
                            MenuMon=a
                        };
            List<BOChiTietBanHang> list = new List<BOChiTietBanHang>();
            foreach (var item in query)
            {
                BOChiTietBanHang ct = new BOChiTietBanHang(item, mTransit);
                ct.SoLuongBanTam = chititet.SoLuongBanTam;
                ct.ChiTietBanHang.SoLuongBan = chititet.ChiTietBanHang.SoLuongBan;
                ct.ChiTietBanHang.GiaBan = 0;
                ct.ChiTietBanHang.NhanVienID = chititet.ChiTietBanHang.NhanVienID;
                list.Add(ct);
            }
            return list;
        }
    }          
}
