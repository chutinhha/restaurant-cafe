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
            frGopBan.AddObject(gopban);
            frGopBan.Commit();
            foreach (var item in _ListBan)
            {
                CHITIETGOPBAN chitiet = new CHITIETGOPBAN();
                chitiet.GopBanID = gopban.GopBanID;
                chitiet.BanHangID = item.BANHANG.BanHangID;
                frChiTietGopBan.AddObject(chitiet);
            }
            if (banHang>0)
            {
                CHITIETGOPBAN chitiet = new CHITIETGOPBAN();
                chitiet.GopBanID = gopban.GopBanID;
                chitiet.BanHangID = banHang;
                frChiTietGopBan.AddObject(chitiet);
            }
            frChiTietGopBan.Commit();
        }
        public void XuliTachBan()
        {
 
        }
        public bool KiemTra()
        {
            if (mListBan.Count>0)
            {
                return true;
            }
            return false;
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
                if (item.BANHANG.BanHangID==bh.BANHANG.BanHangID)
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
    }
}
