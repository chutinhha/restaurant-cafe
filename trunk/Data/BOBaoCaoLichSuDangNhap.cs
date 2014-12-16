using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoLichSuDangNhap
    {
        KaraokeEntities mKaraokeEntities = null;
        public BOBaoCaoLichSuDangNhap()
        {
            mKaraokeEntities = new KaraokeEntities();

        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }

        public IQueryable<BAOCAOLICHDANGNHAP> GetBaoCaoLichSuDangNhap(DateTime dtFrom)
        {
            return from x in mKaraokeEntities.BAOCAOLICHDANGNHAPs
                   where x.ThoiGian.Value.Year == dtFrom.Year && x.ThoiGian.Value.Month == dtFrom.Month && x.ThoiGian.Value.Day == dtFrom.Day
                   select x;
        }
    }
}
