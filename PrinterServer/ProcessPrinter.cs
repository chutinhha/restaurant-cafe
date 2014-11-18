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
        private void InHoaDonThread(int lichSuBanHang)
        {
            var list = mXuliMayIn.AllPrinting(lichSuBanHang).ToList();
            foreach (var item in list)
            {
                PrinterData mayInHoaDon = new PrinterData(lichSuBanHang, item, mXuliMayIn);
                mayInHoaDon.Print();
            }
        }

    }
}
