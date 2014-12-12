using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoDinhLuong
    {
        KaraokeEntities mKaraokeEntities = null;
        public BOBaoCaoDinhLuong()
        {
            mKaraokeEntities = new KaraokeEntities();

        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }

        public IQueryable<BAOCAODINHLUONG> GetBaoCaoDinhLuong(DateTime dtFrom)
        {
            return from x in mKaraokeEntities.BAOCAODINHLUONGs
                   where x.NgayBan.Value.Year == dtFrom.Year && x.NgayBan.Value.Month == dtFrom.Month && x.NgayBan.Value.Day == dtFrom.Day
                   select x;
        }
        
    }
}
