using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BODinhLuong
    {
        public DINHLUONG DinhLuong { get; set; }
        public MENUMON MenuMon { get; set; }
        public LOAIBAN LoaiBan { get; set; }
        private FrameworkRepository<DINHLUONG> frmDinhLuong = null;
        private FrameworkRepository<MENUMON> frmMenuMon = null;
        private FrameworkRepository<LOAIBAN> frmLoaiBan = null;

        public BODinhLuong(Data.Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmDinhLuong = new FrameworkRepository<DINHLUONG>(transit.KaraokeEntities, transit.KaraokeEntities.DINHLUONGs);
            frmMenuMon = new FrameworkRepository<MENUMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUMONs);
            frmLoaiBan = new FrameworkRepository<LOAIBAN>(transit.KaraokeEntities, transit.KaraokeEntities.LOAIBANs);
        }
        public BODinhLuong()
        {

        }

        public IQueryable<BODinhLuong> GetAll(int KichThuocMonID, Transit mTransit)
        {
            var res = (from g in frmDinhLuong.Query()
                       join l in frmMenuMon.Query() on g.MonID equals l.MonID
                       where g.KichThuocMonChinhID == KichThuocMonID
                       select new BODinhLuong
                       {
                           DinhLuong = g,
                           MenuMon = l
                       });
            return res;
        }

        public void Luu(List<BODinhLuong> lsArray, List<BODinhLuong> lsArrayDeleted, Transit mTransit)
        {
            if (lsArray != null)
                foreach (BODinhLuong item in lsArray)
                {
                    if (item.DinhLuong.ID > 0)
                        Sua(item, mTransit);
                    else
                        Them(item, mTransit);
                }
            if (lsArrayDeleted != null)
                foreach (BODinhLuong item in lsArrayDeleted)
                {
                    Xoa(item, mTransit);
                }
            frmDinhLuong.Commit();
        }

        private int Sua(BODinhLuong item, Transit mTransit)
        {
            frmDinhLuong.Update(item.DinhLuong);
            return item.DinhLuong.ID;
        }

        private int Them(BODinhLuong item, Transit mTransit)
        {
            frmDinhLuong.AddObject(item.DinhLuong);
            return item.DinhLuong.ID;
        }

        private int Xoa(BODinhLuong item, Transit mTransit)
        {
            frmDinhLuong.DeleteObject(item.DinhLuong);
            return item.DinhLuong.ID;
        }
    }
}
