using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServer
{
    public class ProcessReport
    {
        private Data.Transit mTransit;
        public ProcessReport()        
        {
            mTransit = new Data.Transit();
        }
        public string LoadInformation()
        {
            Data.BOThongTinCongTy thongtin = Data.BOThongTinCongTy.GetQueryNoTracking(mTransit);
            string s= Newtonsoft.Json.JsonConvert.SerializeObject(thongtin);
            return s;
        }
        public string LoadReportNow()
        {
            List<Data.BAOCAOLICHSUBANHANG> baocao = Data.BOBaoCaoLichSuBanHang.GetNoTracking(mTransit, DateTime.Now).ToList();
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(baocao);
            return s;
        }
    }
}
