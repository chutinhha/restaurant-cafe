using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoTonKho
    {
        KaraokeEntities mKaraokeEntities = null;
        public BOBaoCaoTonKho()
        {
            mKaraokeEntities = new KaraokeEntities();

        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }

        public IQueryable<BAOCAOTONKHO> GetBaoCaoTonKho()
        {
            return mKaraokeEntities.BAOCAOTONKHOes;
                   
        }
    }
}
