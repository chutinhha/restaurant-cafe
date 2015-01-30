using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoBan
    {
        KaraokeEntities mKaraokeEntities = null;
        Data.Transit mTransit = null;
        public BOBaoCaoBan(Data.Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }

        public IQueryable<BAOCAOBAN> GetBaoCaoBan(DateTime dtFrom, DateTime dtTo)
        {
            //var sql = from x in mKaraokeEntities.BAOCAOBANs
            //          where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
            //          orderby x.TenBan
            //          select x;
            //return sql;
            var paraFrom = new System.Data.SqlClient.SqlParameter("@NgayBatDau", System.Data.SqlDbType.DateTime);
            paraFrom.Value = dtFrom;
            var paraTo = new System.Data.SqlClient.SqlParameter("@NgayKetThuc", System.Data.SqlDbType.DateTime);
            paraTo.Value = dtTo;
            var sql = mKaraokeEntities.ExecuteStoreQuery<BAOCAOBAN>("SP_REPORT_BAOCAOBAN @NgayBatDau,@NgayKetThuc", paraFrom, paraTo);
            return sql.AsQueryable<BAOCAOBAN>();
        }
    }
}
