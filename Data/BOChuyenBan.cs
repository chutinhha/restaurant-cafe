using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChuyenBan
    {
        public BOBanHang _BanHang{set;get;}
        private Transit mTransit;
        private FrameworkRepository<CHUYENBAN> frChuyenBan;
        public BOChuyenBan(Data.Transit tran)
        {
            mTransit = tran;
            frChuyenBan = new FrameworkRepository<CHUYENBAN>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.CHUYENBANs);
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
            frChuyenBan.AddObject(chuyen);
            frChuyenBan.Commit();
        }
        public void LoadBanHang(BAN ban)
        {            
            _BanHang = new Data.BOBanHang(mTransit);
            _BanHang.LoadBanHang(ban);
        }
    }
}
