using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoMayIn
    {
        KaraokeEntities mKaraokeEntities = null;
        public BOBaoCaoMayIn()
        {
            mKaraokeEntities = new KaraokeEntities();

        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }       

        public IQueryable<BAOCAOCHITIETLICHSUBANHANG> GetBaoCaoChiTietLichSuBanHang(DateTime dtFrom)
        {
            return from x in mKaraokeEntities.BAOCAOCHITIETLICHSUBANHANGs
                   where x.NgayBan.Value.Year == dtFrom.Year && x.NgayBan.Value.Month == dtFrom.Month && x.NgayBan.Value.Day == dtFrom.Day
                   select x;
        }
    }
}
