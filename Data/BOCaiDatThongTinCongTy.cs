using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOCaiDatThongTinCongTy
    {
        FrameworkRepository<CAIDATTHONGTINCONGTY> frmCaiDatThongTinCongTy = null;
        public BOCaiDatThongTinCongTy(Data.Transit transit)
        {
            frmCaiDatThongTinCongTy = new FrameworkRepository<CAIDATTHONGTINCONGTY>(transit.KaraokeEntities, transit.KaraokeEntities.CAIDATTHONGTINCONGTies);
        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetAll(Data.Transit transit)
        {
            return frmCaiDatThongTinCongTy.Query();
        }

        public void CapNhat(Data.CAIDATTHONGTINCONGTY item, bool IsUpdate, Data.Transit transit)
        {
            if (IsUpdate)
                frmCaiDatThongTinCongTy.Update(item);
            else
                frmCaiDatThongTinCongTy.AddObject(item);
            frmCaiDatThongTinCongTy.Commit();
        }

        public static IQueryable<CAIDATTHONGTINCONGTY> GetNoTracking(Transit transit)
        {
            return FrameworkRepository<CAIDATTHONGTINCONGTY>.QueryNoTracking(transit.KaraokeEntities.CAIDATTHONGTINCONGTies);
        }

    }
}
