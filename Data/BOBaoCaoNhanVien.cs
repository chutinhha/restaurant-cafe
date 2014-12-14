using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoNhanVien
    {
        KaraokeEntities mKaraokeEntities = null;
        Data.Transit mTransit = null;
        public BOBaoCaoNhanVien(Data.Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }

        public IQueryable<BAOCAONHANVIEN> GetBaoCaoNhanVien(DateTime dtFrom, DateTime dtTo)
        {
            return from x in mKaraokeEntities.BAOCAONHANVIENs
                   where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
                   orderby x.TenNhanVien
                   select x;
        }
    }
}
