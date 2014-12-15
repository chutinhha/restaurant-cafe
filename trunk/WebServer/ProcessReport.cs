using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServer
{
    public class ProcessReport
    {
        private Data.Transit mTransit;
        private Data.KaraokeEntities mKaraokeEntities;
        public ProcessReport()        
        {
            mTransit = new Data.Transit();
            mKaraokeEntities = new Data.KaraokeEntities();
        }
        public string LoadInformation()
        {
            Data.BOThongTinCongTy thongtin = Data.BOThongTinCongTy.GetQueryNoTracking(mTransit);
            string s= Newtonsoft.Json.JsonConvert.SerializeObject(thongtin);
            return s;
        }
        public string LoadReportNow()
        {
            Data.BOBaoCaoNgay bobaocao = new Data.BOBaoCaoNgay(mTransit);
            //List<Data.BAOCAONGAYTONG> baocao = bobaocao.GetBaoCaoNgayTong(DateTime.Now).ToList();
            List<Data.BAOCAONGAYTONG> baocao = new List<Data.BAOCAONGAYTONG>();
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(baocao);
            return s;
        }
    }
}
