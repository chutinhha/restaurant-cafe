using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOGiaoDienChucNangBanHang
    {
        FrameworkRepository<GIAODIENCHUCNANGBANHANG> frmCaiDatGiaoDienBanHang = null;
        public BOGiaoDienChucNangBanHang(Data.Transit transit)
        {
            frmCaiDatGiaoDienBanHang = new FrameworkRepository<GIAODIENCHUCNANGBANHANG>(transit.KaraokeEntities, transit.KaraokeEntities.GIAODIENCHUCNANGBANHANGs);
        }

        public IQueryable<GIAODIENCHUCNANGBANHANG> GetAll(Data.Transit transit)
        {
            return frmCaiDatGiaoDienBanHang.Query();
        }

        public void CapNhat(List<Data.GIAODIENCHUCNANGBANHANG> lsArray, Data.Transit transit)
        {
            foreach (var item in lsArray)
            {
                frmCaiDatGiaoDienBanHang.Update(item);
            }
            frmCaiDatGiaoDienBanHang.Commit();
        }

        public static IQueryable<GIAODIENCHUCNANGBANHANG> GetNoTracking(Transit transit)
        {
            return FrameworkRepository<GIAODIENCHUCNANGBANHANG>.QueryNoTracking(transit.KaraokeEntities.GIAODIENCHUCNANGBANHANGs);
        }
    }
}
