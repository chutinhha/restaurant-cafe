using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOGiaoDienChucNangBanHang
    {

        KaraokeEntities mKaraokeEntities = null;
        public BOGiaoDienChucNangBanHang(Data.Transit transit)
        {
            mKaraokeEntities = new KaraokeEntities();
        }

        public IQueryable<GIAODIENCHUCNANGBANHANG> GetAll()
        {
            return mKaraokeEntities.GIAODIENCHUCNANGBANHANGs;
        }

        public void Luu()
        {
            mKaraokeEntities.SaveChanges();
        }

        public static IQueryable<GIAODIENCHUCNANGBANHANG> GetNoTracking(Transit transit)
        {
            return FrameworkRepository<GIAODIENCHUCNANGBANHANG>.QueryNoTracking(transit.KaraokeEntities.GIAODIENCHUCNANGBANHANGs);
        }
    }
}
