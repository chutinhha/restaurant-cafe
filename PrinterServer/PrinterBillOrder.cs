using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrinterServer
{
    public class PrinterBillOrder
    {
        private POSPrinter mPOSPrinter;
        private int mBanHangID;
        private Data.BOMayIn mBOMayIn;
        private Data.BOXuliMayIn mBOXuliMayIn;
        private Data.BOPrintOrder mBOPrintOrder;
        private List<Data.BOPrintOrderItem> mListPrintOrderItem;

        public PrinterBillOrder(int banHangID, Data.BOMayIn mayin, Data.BOXuliMayIn xuli)
        {
            mBOMayIn = mayin;
            mBOXuliMayIn = xuli;
            mBanHangID = banHangID;
            mPOSPrinter = new POSPrinter();
            mPOSPrinter.POSSetPrinterName(mBOMayIn.TenMayIn);
            mPOSPrinter.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(mPOSPrinter_PrintPage);
        }
        private void LoadData()
        {
            mBOPrintOrder = mBOXuliMayIn.GetOrderFromBanHangID(mBanHangID).FirstOrDefault();
            //mListPrintOrderItem = mBOXuliMayIn.GetPrintOrderItem(mLichSuBanHangID, mBOMayIn.MayInID).ToList();
        }
        public void Print()
        {
            LoadData();
            if (mBOPrintOrder != null)
            {
                for (int i = 0; i < mBOMayIn.SoLanIn; i++)
                {
                    mPOSPrinter.Print();
                }
            }
        }
        void mPOSPrinter_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }
    }
}
