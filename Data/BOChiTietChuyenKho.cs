using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChiTietChuyenKho
    {
        public Data.CHITIETCHUYENKHO ChiTietChuyenKho { get; set; }
        public Data.MENUMON MenuMon { get; set; }
        public Data.LOAIBAN LoaiBan { get; set; }
        public Data.CHUYENKHO ChuyenKho { get; set; }
        public Data.TONKHO TonKho { get; set; }
        public List<LOAIBAN> ListLoaiBan { get; set; }

        FrameworkRepository<CHITIETCHUYENKHO> frmChiTietChuyenKho = null;
        FrameworkRepository<MENUMON> frmMenuMon = null;
        FrameworkRepository<LOAIBAN> frmLoaiBan = null;
        FrameworkRepository<CHUYENKHO> frmChuyenKho = null;
        FrameworkRepository<TONKHO> frmTonKho = null;

        public BOChiTietChuyenKho(Transit transit)
        {
            frmChiTietChuyenKho = new FrameworkRepository<CHITIETCHUYENKHO>(transit.KaraokeEntities, transit.KaraokeEntities.CHITIETCHUYENKHOes);
            frmMenuMon = new FrameworkRepository<MENUMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUMONs);
            frmLoaiBan = new FrameworkRepository<LOAIBAN>(transit.KaraokeEntities, transit.KaraokeEntities.LOAIBANs);
            frmChuyenKho = new FrameworkRepository<CHUYENKHO>(transit.KaraokeEntities, transit.KaraokeEntities.CHUYENKHOes);
            frmTonKho = new FrameworkRepository<TONKHO>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOes);
        }

        public BOChiTietChuyenKho()
        {
            ChiTietChuyenKho = new CHITIETCHUYENKHO();
            MenuMon = new MENUMON();
            LoaiBan = new LOAIBAN();
            TonKho = new TONKHO();
            ChuyenKho = new CHUYENKHO();
        }

        public IQueryable<BOChiTietChuyenKho> GetAll(int ChuyenKhoID, Transit mTransit)
        {
            //return from ctck in frmChiTietChuyenKho.Query()
            //       join tk in frmTonKho.Query() on ctck.TonKhoID equals tk.TonKhoID
            //       join lb in frmLoaiBan.Query() on tk.LoaiBanID equals lb.LoaiBanID
            //       join mm in frmMenuMon.Query() on tk.MonID equals mm.MonID
            //       join ck in frmChuyenKho.Query() on ctck.ChuyenKhoID equals ck.ChuyenKhoID
            //       select new BOChiTietChuyenKho
            //       {
            //           ChiTietChuyenKho = ctck,
            //           ChuyenKho = ck,
            //           LoaiBan = lb,
            //           TonKho = tk,
            //           MenuMon = mm
            //       };
            return null;
        }
    }
}
