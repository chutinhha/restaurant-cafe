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
        private PrinterServer.ProcessPrinter mProcessPrinter;
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
            mProcessPrinter = new PrinterServer.ProcessPrinter(mTransit);
        }
        public Data.BOBanHang GetBanHang()
        {
            return mBanHang;
        }
        public void SendOrder()
        {
            int lichSuBanHangId= mBanHang.GuiNhaBep();
            if (lichSuBanHangId>0)
            {
                mProcessPrinter.InHoaDon(lichSuBanHangId);
            }
        }
        public void TinhTien()
        {
            if (BanHang.BanHangID>0)
            {
                mBanHang.TinhTien();    
            }
            else
            {
                mBanHang.GuiNhaBep();
                mBanHang.TinhTien();
            }
        }        
        public void XoaChiTietBanHang(Data.BOChiTietBanHang chitiet)
        {
            chitiet.IsDeleted = true;
        }
        public void AddChiTietBanHang(Data.BOChiTietBanHang chitiet)
        {
            mBanHang.AddChiTietBanHang(chitiet);
        }        
    }
}
