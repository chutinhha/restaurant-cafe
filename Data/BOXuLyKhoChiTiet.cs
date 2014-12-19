using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOXuLyKhoChiTiet
    {
        public Data.XULYKHOCHITIET XuLyKhoChiTiet { get; set; }
        public Data.MENUMON MenuMon { get; set; }
        public Data.LOAIBAN LoaiBan { get; set; }
        public Data.XULYKHO XuLyKho { get; set; }
        public Data.TONKHO TonKho { get; set; }
        public List<LOAIBAN> ListLoaiBan { get; set; }

        FrameworkRepository<XULYKHOCHITIET> frmChiTietXuLyKho = null;
        FrameworkRepository<MENUMON> frmMenuMon = null;
        FrameworkRepository<LOAIBAN> frmLoaiBan = null;
        FrameworkRepository<XULYKHO> frmXyLyKho = null;
        FrameworkRepository<TONKHO> frmTonKho = null;

        public BOXuLyKhoChiTiet(Transit transit)
        {
            frmChiTietXuLyKho = new FrameworkRepository<XULYKHOCHITIET>(transit.KaraokeEntities, transit.KaraokeEntities.XULYKHOCHITIETs);
            frmMenuMon = new FrameworkRepository<MENUMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUMONs);
            frmLoaiBan = new FrameworkRepository<LOAIBAN>(transit.KaraokeEntities, transit.KaraokeEntities.LOAIBANs);
            frmXyLyKho = new FrameworkRepository<XULYKHO>(transit.KaraokeEntities, transit.KaraokeEntities.XULYKHOes);
            frmTonKho = new FrameworkRepository<TONKHO>(transit.KaraokeEntities, transit.KaraokeEntities.TONKHOes);
        }

        public BOXuLyKhoChiTiet()
        {
            XuLyKhoChiTiet = new XULYKHOCHITIET();
            MenuMon = new MENUMON();
            LoaiBan = new LOAIBAN();
            TonKho = new TONKHO();
            XuLyKho = new XULYKHO();
        }

        public IQueryable<BOXuLyKhoChiTiet> GetAll(int NhapKhoID, Transit mTransit)
        {
            return from ctnk in frmChiTietXuLyKho.Query()
                   join tk in frmTonKho.Query() on ctnk.TonKhoID equals tk.TonKhoID
                   join lb in frmLoaiBan.Query() on tk.LoaiBanID equals lb.LoaiBanID
                   join mm in frmMenuMon.Query() on tk.MonID equals mm.MonID
                   join nk in frmXyLyKho.Query() on ctnk.ChinhKhoID equals nk.ChinhKhoID
                   select new BOXuLyKhoChiTiet
                   {
                       XuLyKhoChiTiet = ctnk,
                       XuLyKho = nk,
                       LoaiBan = lb,
                       TonKho = tk,
                       MenuMon = mm
                   };
        }
    }
}
