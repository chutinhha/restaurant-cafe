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

        public IQueryable<BAOCAONGAYTONG> GetBaoCaoNgayTong(DateTime dtFrom, DateTime dtTo)
        {
            return from x in mKaraokeEntities.BAOCAONGAYTONGs
                   where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
                   select x;
        }

        public IQueryable<BAOCAONGAYNHOM> GetBaoCaoNgayNhom(DateTime dtFrom, DateTime dtTo)
        {
            return from x in mKaraokeEntities.BAOCAONGAYNHOMs
                   where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
                   orderby x.TenDai
                   select x;
        }

        public IQueryable<BAOCAONGAYMON> GetBaoCaoNgayMon(DateTime dtFrom, DateTime dtTo)
        {
            return from x in mKaraokeEntities.BAOCAONGAYMONs
                   where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
                   orderby x.Ten
                   select x;
        }
        public IQueryable<BAOCAONGAYTHE> GetBaoCaoThe(DateTime dtFrom, DateTime dtTo)
        {
            return from x in mKaraokeEntities.BAOCAONGAYTHEs
                   where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
                   orderby x.TenThe
                   select x;
        }
        public IQueryable<BAOCAONGAYKHACHHANG> GetBaoCaoNgayKhachHang(DateTime dtFrom, DateTime dtTo)
        {
            return from x in mKaraokeEntities.BAOCAONGAYKHACHHANGs
                   where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
                   orderby x.TenKhachHang
                   select x;
        }
    }
}
