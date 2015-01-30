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

        public static IQueryable<BAOCAOTONKHO> GetBaoCaoTonKho(KaraokeEntities kara,DateTime dtFrom)
        {
            return from x in kara.BAOCAOTONKHOes
                   where (x.NgayBan.Value.Year == dtFrom.Year && x.NgayBan.Value.Month == dtFrom.Month && x.NgayBan.Value.Day == dtFrom.Day) || x.NgayBan == null
                   select x;

        }
        public IQueryable<BAOCAOTONKHO> GetBaoCaoTonKho(DateTime dtFrom)
        {
            //return from x in mKaraokeEntities.BAOCAOTONKHOes
            //       where (x.NgayBan.Value.Year == dtFrom.Year && x.NgayBan.Value.Month == dtFrom.Month && x.NgayBan.Value.Day == dtFrom.Day) || x.NgayBan == null
            //       select x;
            return GetBaoCaoTonKho(mKaraokeEntities, dtFrom);

        }
    }
}
