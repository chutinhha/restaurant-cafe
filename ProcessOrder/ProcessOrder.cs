using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace ProcessOrder
{
    public class ProcessOrder
    {
        private Data.BOBanHang mBanHang;
        private Data.Transit mTransit;        
        public Data.BOChiTietBanHang CurrentChiTietBanHang { get; set; }
        public Data.BANHANG BanHang
        {
            get { return mBanHang.BANHANG; }
        }
        public List<Data.BOChiTietBanHang> ListChiTietBanHang
        {
            get { return mBanHang._ListChiTietBanHang; }
        }
        public ProcessOrder(Data.Transit transit)
        {
            mTransit = transit;
            mBanHang = new Data.BOBanHang(mTransit);
        }
        public void SendOrder()
        {
            int lichSuBanHangId= mBanHang.GuiNhaBep();
            if (lichSuBanHangId>0)
            {
                
            }
        }
        public void AddChiTietBanHang(Data.BOChiTietBanHang chitiet)
        {
            mBanHang.AddChiTietBanHang(chitiet);
        }        
    }
}
