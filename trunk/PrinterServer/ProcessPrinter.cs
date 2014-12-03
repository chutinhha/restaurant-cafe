using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PrinterServer
{
    public class ProcessPrinter
    {
        private Data.Transit mTransit;
        private Data.BOXuliMayIn mXuliMayIn;
        public ProcessPrinter(Data.Transit tran)
        {
            mTransit = tran;
            mXuliMayIn = new Data.BOXuliMayIn(mTransit);
        }
        public void InHoaDon(int lichSuBanHang)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate { InHoaDonThread(lichSuBanHang); }));
            thread.Start();
        }
        public void InBill(bool tamtinh,int banHangID)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate { InBillThread(tamtinh,banHangID); }));
            thread.Start();
        }
        private void InBillThread(bool tamtinh,int banHangID)
        {            
            var list = mXuliMayIn.AllPrintingBill().ToList();
            foreach (var item in list)
            {
                PrinterBillOrder mayInHoaDon = new PrinterBillOrder(tamtinh,banHangID, item, mXuliMayIn);
                mayInHoaDon.Print();
            }
        }
        private void InHoaDonThread(int lichSuBanHang)
        {
            var list = mXuliMayIn.AllPrinting(lichSuBanHang).ToList();
            foreach (var item in list)
            {
                PrinterSendOrder mayInHoaDon = new PrinterSendOrder(lichSuBanHang, item, mXuliMayIn);
                mayInHoaDon.Print();
            }
        }

    }
}
