using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOTachGopBan
    {
        private Transit mTransit;
        private List<Data.BOBanHang> mListBan;
        public BOBanHang _CurrentBanHang{set;get;}
        private FrameworkRepository<GOPBAN> frGopBan;
        private FrameworkRepository<CHITIETGOPBAN> frChiTietGopBan;
        private FrameworkRepository<TACHBAN> frTachBan;
        private FrameworkRepository<CHITIETTACHBAN> frChiTietTachBan;
        public Data.BOBanHang BanHang { get; set; }
        public List<Data.BOBanHang> _ListBan 
        {
            get { return mListBan; }
        }
        public BOTachGopBan(Transit transit)
        {
            mTransit = transit;
            mListBan = new List<BOBanHang>();
            frGopBan = new FrameworkRepository<GOPBAN>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.GOPBANs);
            frChiTietGopBan = new FrameworkRepository<CHITIETGOPBAN>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.CHITIETGOPBANs);
            frTachBan = new FrameworkRepository<TACHBAN>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.TACHBANs);
            frChiTietTachBan = new FrameworkRepository<CHITIETTACHBAN>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.CHITIETTACHBANs);
        }

        public IQueryable<KHU> GetAllKhuVisual()
        {
            return Data.BOKhu.GetAllVisual(mTransit);
        }
        public IQueryable<BAN> GetAllTableNotInOrderPerArea(KHU khu)
        {
            return Data.BOBan.GetAllTableNotInOrderPerArea(khu, mTransit);
        }
        public IQueryable<BAN> GetAllTableInOrderPerArea(KHU khu,int banID)
        {
            return from a in Data.BOBan.GetAllTableInOrderPerArea(khu,mTransit)
                   where a.BanID!=banID || banID==0
                   select a;
        }
        public IQueryable<BAN> GetVisualTablePerArea(KHU khu)
        {
            return Data.BOBan.GetVisualTablePerArea(khu, mTransit);
        }
        public void XuliGopBan()
        {
            int banHang = this.BanHang.BANHANG.BanHangID;
            this.BanHang.GopBan(this);
            GOPBAN gopban = new GOPBAN();            
            gopban.BanHangID = this.BanHang.BANHANG.BanHangID;
            gopban.NhanVienID = mTransit.NhanVien.NhanVienID;
            gopban.ThoiGian = DateTime.Now;                                    
            foreach (var item in _ListBan)
            {
                CHITIETGOPBAN chitiet = new CHITIETGOPBAN();                                
                chitiet.BanHangID = item.BANHANG.BanHangID;
                gopban.CHITIETGOPBANs.Add(chitiet);                
            }
            if (banHang>0)
            {
                CHITIETGOPBAN chitiet = new CHITIETGOPBAN();                
                chitiet.BanHangID = banHang;                
                gopban.CHITIETGOPBANs.Add(chitiet);                
            }            
            frGopBan.AddObject(gopban);
            frGopBan.Commit();
        }
        public void XuliTachBan()
        {
            int banHang = this.BanHang.BANHANG.BanHangID;
            this.BanHang.TachBan(this);
            TACHBAN tachBan = new TACHBAN();
            tachBan.NhanVienID = mTransit.NhanVien.NhanVienID;
            tachBan.BanHangID = banHang;
            tachBan.ThoiGian = DateTime.Now;            
            foreach (var item in _ListBan)
            {
                CHITIETTACHBAN chitiet = new CHITIETTACHBAN();                
                chitiet.BanHangID = item.BANHANG.BanHangID;
                tachBan.CHITIETTACHBANs.Add(chitiet);      
            }
            if (this.BanHang._ListChiTietBanHang.Count>0)
            {
                CHITIETTACHBAN chitiet = new CHITIETTACHBAN();                
                chitiet.BanHangID = this.BanHang.BANHANG.BanHangID;
                tachBan.CHITIETTACHBANs.Add(chitiet);
            }
            frTachBan.AddObject(tachBan);
            frTachBan.Commit();
        }
        public bool KiemTra()
        {
            if (mListBan.Count>0)
            {
                return true;
            }
            return false;
        }
        public Data.BOBanHang GetTachBan(BAN ban)
        {
            foreach (var item in _ListBan)
            {
                if (item.BANHANG.BanID==ban.BanID)
                {
                    _CurrentBanHang = item;
                    return _CurrentBanHang;
                }
            }
            return _CurrentBanHang= null;
        }
        
        public Data.BOBanHang GetBanHang(BAN ban)
        {
            Data.BOBanHang banhang = new Data.BOBanHang(mTransit);            
            banhang.LoadBanHang(ban);
            return banhang;
        }
        private bool KiemTraBanHang(Data.BOBanHang bh)
        {
            foreach (var item in _ListBan)
            {
                if (item.BANHANG.BanHangID==bh.BANHANG.BanHangID && item.BANHANG.BanID==bh.BANHANG.BanID)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddBanHang(Data.BOBanHang bh)
        {
            if (!KiemTraBanHang(bh))
            {
                mListBan.Add(bh);
            }
        }
        public void XoaBanHang(Data.BOBanHang bh)
        {
            mListBan.Remove(bh);
        }
        /// <summary>
        /// neu ko co item thi tra ve false
        /// </summary>
        /// <param name="chitiet"></param>
        /// <param name="ban"></param>
        /// <returns></returns>
        public bool ThemTachBan(BOChiTietBanHang chitiet,BAN ban)
        {
            if(_CurrentBanHang==null)
	        {
		        BOBanHang banhang=GetBanHang(ban);
                banhang.AddChiTietBanHang(chitiet);
                this.AddBanHang(banhang);
                _CurrentBanHang = banhang;
                return false;
	        }
            _CurrentBanHang.AddChiTietBanHang(chitiet);
                return true;
        }
        public void XoaTachBan(BOChiTietBanHang chitiet)
        {
            _CurrentBanHang.DeleteChiTietBanHang(chitiet);
            if (_CurrentBanHang._ListChiTietBanHang.Count==0)
            {
                this.XoaBanHang(_CurrentBanHang);
            }
        }
    }
}
