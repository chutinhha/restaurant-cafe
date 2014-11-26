using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOGopBan
    {
        private Transit mTransit;
        private List<Data.BOBanHang> mListBanGop;
        private FrameworkRepository<GOPBAN> frGopBan;
        private FrameworkRepository<CHITIETGOPBAN> frChiTietGopBan;
        public Data.BOBanHang BanHang { get; set; }
        public List<Data.BOBanHang> _ListBanGop 
        {
            get { return mListBanGop; }
        }
        public BOGopBan(Transit transit)
        {
            mTransit = transit;
            mListBanGop = new List<BOBanHang>();
            frGopBan = new FrameworkRepository<GOPBAN>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.GOPBANs);
            frChiTietGopBan = new FrameworkRepository<CHITIETGOPBAN>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.CHITIETGOPBANs);
        }

        public IQueryable<KHU> GetAllKhuVisual()
        {
            return Data.BOKhu.GetAllVisual(mTransit);
        }
        public IQueryable<BAN> GetAllTableInOrderPerArea(KHU khu)
        {
            return from a in Data.BOBan.GetAllTableInOrderPerArea(khu,mTransit)
                   where a.BanID!=BanHang.BANHANG.BanID
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
            foreach (var item in _ListBanGop)
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
        public bool KiemTraGopBan()
        {
            if (mListBanGop.Count>0)
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
            foreach (var item in _ListBanGop)
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
                mListBanGop.Add(bh);
            }
        }
        public void XoaBanHang(Data.BOBanHang bh)
        {
            mListBanGop.Remove(bh);
        }
    }
}
