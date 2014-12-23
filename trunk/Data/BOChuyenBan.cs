using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChuyenBan
    {
        public BOBanHang _BanHang{set;get;}        
        private KaraokeEntities mKaraokeEntities;
        private Transit mTransit;
        public BOChuyenBan(Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
            mTransit = transit;            
        }
        public IQueryable<KHU> GetAllVisual()
        {
            return Data.BOKhu.GetAllVisual(mTransit);
        }
        public IQueryable<BAN> GetAllTableInOrderPerArea(KHU khu)
        {
            return Data.BOBan.GetAllTableInOrderPerArea(khu, mTransit);
        }
        public IQueryable<BAN> GetAllTableNotInOrderPerArea(KHU khu)
        {
            return Data.BOBan.GetAllTableNotInOrderPerArea(khu,mTransit);
        }
        public void ChuyenBan(BAN ban)
        {
            int banHangID = _BanHang.BANHANG.BanHangID;                
            _BanHang.ChuyenBan(ban);
            CHUYENBAN chuyen = new CHUYENBAN();
            chuyen.TuBanHangID = banHangID;
            chuyen.DenBanHangID = _BanHang.BANHANG.BanHangID;
            chuyen.NhanVienID = mTransit.NhanVien.NhanVienID;
            chuyen.ThoiGian = DateTime.Now;
            mKaraokeEntities.CHUYENBANs.AddObject(chuyen);
            mKaraokeEntities.SaveChanges();
        }
        public void LoadBanHang(BAN ban)
        {            
            _BanHang = new Data.BOBanHang(mTransit,mKaraokeEntities);
            _BanHang.LoadBanHang(ban);
        }
    }
}
