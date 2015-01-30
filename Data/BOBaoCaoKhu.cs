using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOBaoCaoKhu
    {
        KaraokeEntities mKaraokeEntities = null;
        Data.Transit mTransit = null;
        public BOBaoCaoKhu(Data.Transit transit)
        {
            mTransit = transit;
            mKaraokeEntities = new KaraokeEntities();
        }

        public IQueryable<CAIDATTHONGTINCONGTY> GetCaiDatThongTinCongTy()
        {
            return mKaraokeEntities.CAIDATTHONGTINCONGTies;
        }

        public IQueryable<BAOCAOKHU> GetBaoCaoKhu(DateTime dtFrom, DateTime dtTo)
        {
            //return from x in mKaraokeEntities.BAOCAOKHUs
            //       where dtFrom.CompareTo(x.NgayBan.Value) <= 0 && dtTo.CompareTo(x.NgayBan.Value) >= 0
            //       orderby x.TenKhu
            //       select x;
            var paraFrom = new System.Data.SqlClient.SqlParameter("@NgayBatDau", System.Data.SqlDbType.DateTime);
            paraFrom.Value = dtFrom;
            var paraTo = new System.Data.SqlClient.SqlParameter("@NgayKetThuc", System.Data.SqlDbType.DateTime);
            paraTo.Value = dtTo;
            var sql = mKaraokeEntities.ExecuteStoreQuery<BAOCAOKHU>("SP_REPORT_BAOCAOKHU @NgayBatDau,@NgayKetThuc", paraFrom, paraTo);
            return sql.AsQueryable<BAOCAOKHU>();
        }
    }
}
