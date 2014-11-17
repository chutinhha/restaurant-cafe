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
        public List<LOAIBAN> ListLoaiBan { get; set; }

        FrameworkRepository<CHITIETNHAPKHO> frmChiTietNhapKho = null;
        FrameworkRepository<MENUMON> frmMenuMon = null;
        FrameworkRepository<LOAIBAN> frmLoaiBan = null;
        FrameworkRepository<NHAPKHO> frmNhapKho = null;

        public BOChiTietNhapKho(Transit transit)
        {
            transit.KaraokeEntities = new KaraokeEntities();
            frmChiTietNhapKho = new FrameworkRepository<CHITIETNHAPKHO>(transit.KaraokeEntities, transit.KaraokeEntities.CHITIETNHAPKHOes);
            frmMenuMon = new FrameworkRepository<MENUMON>(transit.KaraokeEntities, transit.KaraokeEntities.MENUMONs);
            frmLoaiBan = new FrameworkRepository<LOAIBAN>(transit.KaraokeEntities, transit.KaraokeEntities.LOAIBANs);
            frmNhapKho = new FrameworkRepository<NHAPKHO>(transit.KaraokeEntities, transit.KaraokeEntities.NHAPKHOes);
        }

        public BOChiTietNhapKho()
        {
            ChiTietNhapKho = new CHITIETNHAPKHO();
            MenuMon = new MENUMON();
            LoaiBan = new LOAIBAN();
        }

        public IQueryable<BOChiTietNhapKho> GetAll(int NhapKhoID, Transit mTransit)
        {
            var res = (from ctnk in frmChiTietNhapKho.Query()
                       join mm in frmMenuMon.Query() on ctnk.MonID equals mm.MonID
                       join lb in frmLoaiBan.Query() on ctnk.LoaiBanID equals lb.LoaiBanID
                       join nk in frmNhapKho.Query() on ctnk.NhapKhoID equals nk.NhapKhoID
                       where ctnk.NhapKhoID == NhapKhoID
                       select new BOChiTietNhapKho
                       {
                           ChiTietNhapKho = ctnk,
                           MenuMon = mm,
                           LoaiBan = lb,
                           NhapKho = nk
                       });
            return res;
        }
    }
}
