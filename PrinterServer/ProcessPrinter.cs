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
        public void InBill(PrinterBillOrder.PrinterBillOrderType type, int banHangID)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate { InBillThread(type,banHangID); }));
            thread.Start();
        }
        private void InBillThread(PrinterBillOrder.PrinterBillOrderType type,int banHangID)
        {
            lock (mXuliMayIn)
            {
                var list = mXuliMayIn.AllPrintingBill().ToList();
                foreach (var item in list)
                {
                    PrinterBillOrder mayInHoaDon = new PrinterBillOrder(type,banHangID, item, mXuliMayIn);
                    mayInHoaDon.Print();
                }
            }
        }
        private void InHoaDonThread(int lichSuBanHang)
        {
            lock (mXuliMayIn)
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
}
