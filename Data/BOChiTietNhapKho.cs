using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOChiTietNhapKho
    {
        public Data.CHITIETNHAPKHO ChiTietNhapKho { get; set; }
        public Data.MENUMON MenuMon { get; set; }
        public Data.LOAIBAN LoaiBan { get; set; }
        public Data.NHAPKHO NhapKho { get; set; }
        public Data.TONKHO TonKho { get; set; }
        public List<LOAIBAN> ListLoaiBan { get; set; }

        FrameworkRepository<CHITIETNHAPKHO> frmChiTietNhapKho = null;
        FrameworkRepository<MENUMON> frmMenuMon = null;
        FrameworkRepository<LOAIBAN> frmLoaiBan = null;
        FrameworkRepository<NHAPKHO> frmNhapKho = null;
        FrameworkRepository<TONKHO> frmTonKho = null;

        public BOChiTietNhapKho(Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmChiTietNhapKho = new FrameworkRepository<CHITIETNHAPKHO>(transit.KaraokeEntities, transit.KaraokeEntities.CHITIETNHAPKHOes);
            frmMenuMon = new FrameworkRepository<MENUMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUMONs);
            frmLoaiBan = new FrameworkRepository<LOAIBAN>(transit.KaraokeEntities, transit.KaraokeEntities.LOAIBANs);
            frmNhapKho = new FrameworkRepository<NHAPKHO>(transit.KaraokeEntities, transit.KaraokeEntities.NHAPKHOes);
            frmTonKho = new FrameworkRepository<TONKHO>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOes);
        }

        public BOChiTietNhapKho()
        {
            ChiTietNhapKho = new CHITIETNHAPKHO();
            MenuMon = new MENUMON();
            LoaiBan = new LOAIBAN();
            TonKho = new TONKHO();
            NhapKho = new NHAPKHO();
        }

        public IQueryable<BOChiTietNhapKho> GetAll(int NhapKhoID, Transit mTransit)
        {
            return from ctnk in frmChiTietNhapKho.Query()
                   join tk in frmTonKho.Query() on ctnk.TonKhoID equals tk.TonKhoID
                   join lb in frmLoaiBan.Query() on tk.LoaiBanID equals lb.LoaiBanID
                   join mm in frmMenuMon.Query() on tk.MonID equals mm.MonID
                   join nk in frmNhapKho.Query() on ctnk.NhapKhoID equals nk.NhapKhoID
                   select new BOChiTietNhapKho
                   {
                       ChiTietNhapKho = ctnk,
                       NhapKho = nk,
                       LoaiBan = lb,
                       TonKho = tk,
                       MenuMon = mm
                   };
        }
    }
}
