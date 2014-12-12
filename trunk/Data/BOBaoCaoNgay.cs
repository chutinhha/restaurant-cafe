using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoNgay
    {
        KaraokeEntities mKaraokeEntities = null;
        Data.Transit mTransit = null;
        public BOBaoCaoNgay(Data.Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }

        public IQueryable<BAOCAONGAYTONG> GetBaoCaoNgayTong(DateTime dtFrom)
        {
            return from x in mKaraokeEntities.BAOCAONGAYTONGs
                   where x.NgayBan.Value.Year == dtFrom.Year && x.NgayBan.Value.Month == dtFrom.Month && x.NgayBan.Value.Day == dtFrom.Day
                   select x;
        }

        public IQueryable<BAOCAONGAYNHOM> GetBaoCaoNgayNhom(DateTime dtFrom)
        {
            return from x in mKaraokeEntities.BAOCAONGAYNHOMs
                   where x.NgayBan.Value.Year == dtFrom.Year && x.NgayBan.Value.Month == dtFrom.Month && x.NgayBan.Value.Day == dtFrom.Day
                   select x;
        }

        public IQueryable<BAOCAONGAYMON> GetBaoCaoNgayMon(DateTime dtFrom)
        {
            return from x in mKaraokeEntities.BAOCAONGAYMONs
                   where x.NgayBan.Value.Year == dtFrom.Year && x.NgayBan.Value.Month == dtFrom.Month && x.NgayBan.Value.Day == dtFrom.Day
                   select x;
        }
        public IQueryable<BAOCAONGAYTHE> GetBaoCaoThe(DateTime dtFrom)
        {
            return from x in mKaraokeEntities.BAOCAONGAYTHEs
                   where x.NgayBan.Value.Year == dtFrom.Year && x.NgayBan.Value.Month == dtFrom.Month && x.NgayBan.Value.Day == dtFrom.Day
                   select x;
        }
        public IQueryable<BAOCAONGAYKHACHHANG> GetBaoCaoNgayKhachHang(DateTime dtFrom)
        {
            return from x in mKaraokeEntities.BAOCAONGAYKHACHHANGs
                   where x.NgayBan.Value.Year == dtFrom.Year && x.NgayBan.Value.Month == dtFrom.Month && x.NgayBan.Value.Day == dtFrom.Day
                   select x;
        }
    }
}
