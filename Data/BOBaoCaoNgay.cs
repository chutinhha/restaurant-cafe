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
        public static IQueryable<BAOCAONGAYTONG> GetBaoCaoNgayTong(KaraokeEntities kara,DateTime dtFrom, DateTime dtTo)
        {
            //return from x in kara.BAOCAONGAYTONGs
            //       where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
            //       select x;
            
            var paraFrom = new System.Data.SqlClient.SqlParameter("@NgayBatDau", System.Data.SqlDbType.DateTime);
            paraFrom.Value = dtFrom;
            var paraTo = new System.Data.SqlClient.SqlParameter("@NgayKetThuc", System.Data.SqlDbType.DateTime);
            paraTo.Value = dtTo;
            var sql = kara.ExecuteStoreQuery<BAOCAONGAYTONG>("SP_REPORT_BAOCAONGAYTONG @NgayBatDau,@NgayKetThuc", paraFrom, paraTo);
            return sql.AsQueryable<BAOCAONGAYTONG>();
        }
        public IQueryable<BAOCAONGAYTONG> GetBaoCaoNgayTong(DateTime dtFrom, DateTime dtTo)
        {
            return GetBaoCaoNgayTong(mTransit.KaraokeEntities,dtFrom, dtTo);            
            //return from x in mKaraokeEntities.BAOCAONGAYTONGs
            //       where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
            //       select x;
        }
        public static IQueryable<BAOCAONGAYNHOM> GetBaoCaoNgayNhom(KaraokeEntities kara,DateTime dtFrom, DateTime dtTo)
        {
            return from x in kara.BAOCAONGAYNHOMs
                   where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
                   orderby x.TenDai
                   select x;
        }
        public IQueryable<BAOCAONGAYNHOM> GetBaoCaoNgayNhom(DateTime dtFrom, DateTime dtTo)
        {
            //return from x in mKaraokeEntities.BAOCAONGAYNHOMs
            //       where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
            //       orderby x.TenDai
            //       select x;
            return GetBaoCaoNgayNhom(mKaraokeEntities, dtFrom, dtTo);
        }
        public static IQueryable<BAOCAONGAYMON> GetBaoCaoNgayMon(KaraokeEntities kara,DateTime dtFrom, DateTime dtTo)
        {
            return from x in kara.BAOCAONGAYMONs
                   where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
                   orderby x.Ten
                   select x;
        }
        public IQueryable<BAOCAONGAYMON> GetBaoCaoNgayMon(DateTime dtFrom, DateTime dtTo)
        {
            //return from x in mKaraokeEntities.BAOCAONGAYMONs
            //       where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
            //       orderby x.Ten
            //       select x;
            return GetBaoCaoNgayMon(mKaraokeEntities, dtFrom, dtTo);
        }
        public static IQueryable<BAOCAONGAYTHE> GetBaoCaoThe(KaraokeEntities kara,DateTime dtFrom, DateTime dtTo)
        {
            return from x in kara.BAOCAONGAYTHEs
                   where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
                   orderby x.TenThe
                   select x;
        }
        public IQueryable<BAOCAONGAYTHE> GetBaoCaoThe(DateTime dtFrom, DateTime dtTo)
        {
            //return from x in mKaraokeEntities.BAOCAONGAYTHEs
            //       where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
            //       orderby x.TenThe
            //       select x;
            return GetBaoCaoThe(mKaraokeEntities, dtFrom, dtTo);
        }
        public static IQueryable<BAOCAONGAYKHACHHANG> GetBaoCaoNgayKhachHang(KaraokeEntities kara,DateTime dtFrom, DateTime dtTo)
        {
            return from x in kara.BAOCAONGAYKHACHHANGs
                   where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
                   orderby x.TenKhachHang
                   select x;
        }
        public IQueryable<BAOCAONGAYKHACHHANG> GetBaoCaoNgayKhachHang(DateTime dtFrom, DateTime dtTo)
        {
            //return from x in mKaraokeEntities.BAOCAONGAYKHACHHANGs
            //       where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
            //       orderby x.TenKhachHang
            //       select x;
            return GetBaoCaoNgayKhachHang(mKaraokeEntities, dtFrom, dtTo);
        }
    }
}
