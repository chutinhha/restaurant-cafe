using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoLichSuBanHang
    {      
       
        KaraokeEntities mKaraokeEntities = null;
        Data.Transit mTransit = null;
        public BOBaoCaoLichSuBanHang(Data.Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }

        public IQueryable<BAOCAOLICHSUBANHANG> GetBaoCaoLichSuBanHang(DateTime dtFrom)
        {
            return from x in mKaraokeEntities.BAOCAOLICHSUBANHANGs
                   where x.NgayBan.Value.Year == dtFrom.Year && x.NgayBan.Value.Month == dtFrom.Month && x.NgayBan.Value.Day == dtFrom.Day
                   select x;
        }
        
    }
}
