using System.Linq;
namespace Data
{
    public class BOTonKhoTong
    {
        public TONKHOTONG TonKhoTong { get; set; }

        public MENUMON Mon { get; set; }

        private FrameworkRepository<TONKHOTONG> frmTonKhoTong = null;
        private FrameworkRepository<MENUMON> frmMon = null;

        public BOTonKhoTong(Data.Transit mTransit)
        {
            frmTonKhoTong = new FrameworkRepository<TONKHOTONG>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.TONKHOTONGs);
            frmMon = new FrameworkRepository<MENUMON>(mTransit.KaraokeEntities, mTransit.KaraokeEntities.MENUMONs);
        }

        public BOTonKhoTong()
        {
            TonKhoTong = new TONKHOTONG();
            Mon = new MENUMON();
        }

        public IQueryable<BOTonKhoTong> GetAll(Data.Transit mTransit)
        {
            return from tkt in frmTonKhoTong.Query()
                   join m in frmMon.Query() on tkt.MonID equals m.MonID
                   select new BOTonKhoTong
                   {
                       TonKhoTong = tkt,
                       Mon = m
                   };
        }
    }
}