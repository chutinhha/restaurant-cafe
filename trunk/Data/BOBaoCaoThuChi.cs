using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoThuChi
    {
        KaraokeEntities mKaraokeEntities = null;
        Data.Transit mTransit = null;
        public BOBaoCaoThuChi(Data.Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }

        public IQueryable<BAOCAOTHUCHI> GetBOBaoCaoThuChi(DateTime dtFrom, DateTime dtTo)
        {
            return from x in mKaraokeEntities.BAOCAOTHUCHIs
                   where dtFrom.CompareTo(x.ThoiGian.Value) <= 0 && dtTo.CompareTo(x.ThoiGian.Value) >= 0
                   orderby x.ThoiGianFull
                   select x;
        }
    }
}
