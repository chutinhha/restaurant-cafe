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
        public void InPhieuThuChi(int thuchiID)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate { InPhieuThuChiThread(thuchiID); }));
            thread.Start();
        }        
        public void InReport(DateTime dtFrom, DateTime dtTo)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate { InReportThread(dtFrom,dtTo); }));
            thread.Start();
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
        private void InPhieuThuChiThread(int thuchiID)
        {
            lock (mXuliMayIn)
            {
                var list = mXuliMayIn.AllPrintingBill().ToList();
                foreach (var item in list)
                {
                    PrinterThuChi mayin = new PrinterThuChi(thuchiID, item, mXuliMayIn);
                    mayin.Print();
                }
            }
        }
        private void InReportThread(DateTime dtFrom, DateTime dtTo)
        {
            lock (mXuliMayIn)
            {
                var list = mXuliMayIn.AllPrintingBill().ToList();
                foreach (var item in list)
                {
                    PrinterReportDaily mayin = new PrinterReportDaily(dtFrom, dtTo, item, mXuliMayIn);
                    mayin.Print();
                }
            }
        }
        private void InBillThread(PrinterBillOrder.PrinterBillOrderType type,int banHangID)
        {
            lock (mXuliMayIn)
            {
                var list = mXuliMayIn.AllPrintingBill().ToList();
                foreach (var item in list)
                {
                    PrinterBillOrder mayInHoaDon = new PrinterBillOrder(type, banHangID, item, mXuliMayIn);
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
