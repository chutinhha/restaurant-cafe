using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrinterServer
{
    class PrinterData
    {
        private int mLichSuBanHangID;        
        private Data.BOXuliMayIn mXuLiMayIn;
        private POSPrinter mPOSPrinter;
        private Data.BOMayIn mBOMayIn;
        private System.Drawing.Font mFont;
        private System.Drawing.Font mFontHeader;
        private System.Drawing.Color mColorBlack;
        private Data.BOPrintOrder mBOPrintOrder;
        private List<Data.BOPrintOrderItem> mListPrintOrderItem;
        public PrinterData(int lichsu,Data.BOMayIn mayin,Data.BOXuliMayIn xuli)
        {            
            mBOMayIn = mayin;
            mLichSuBanHangID = lichsu;
            mXuLiMayIn = xuli;
            mFont = new System.Drawing.Font("Arial", 12);
            mFontHeader = new System.Drawing.Font("Arial",18,System.Drawing.FontStyle.Bold);
            mColorBlack = System.Drawing.Color.Black;
            mPOSPrinter = new POSPrinter();
            mPOSPrinter.POSSetPrinterName(mBOMayIn.TenMayIn);            
            mPOSPrinter.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrinterData_PrintPage);
            
        }
        private void LoadData()
        {
            mBOPrintOrder = mXuLiMayIn.GetOrderFromLichSuBanHang(mLichSuBanHangID).FirstOrDefault();
            mListPrintOrderItem = mXuLiMayIn.GetPrintOrderItem(mLichSuBanHangID, mBOMayIn.MayInID).ToList();
        }
        public void Print()
        {
            LoadData();
            if (mBOPrintOrder!=null)
            {
                for (int i = 0; i < mBOMayIn.SoLanIn; i++)
                {
                    mPOSPrinter.Print();
                }
            }
        }
        void PrinterData_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float y = mPOSPrinter.POSGetFloat(50);
            y = mPOSPrinter.POSDrawString(mBOMayIn.TieuDeIn, e, mFontHeader, mColorBlack, y, TextAlign.Center, 3);
            y += mPOSPrinter.POSGetFloat(50);
            mPOSPrinter.POSDrawString("Tên Bàn:" + mBOPrintOrder.TenBan, e, mFont, mColorBlack, y, TextAlign.Left, 3);
            y = mPOSPrinter.POSDrawString("Hóa Đơn:" + mBOPrintOrder.MaHoaDon, e, mFont, mColorBlack, y, TextAlign.Right, 3);
            y=mPOSPrinter.POSDrawString("Nhân Viên:" + mBOPrintOrder.TenNhanVien, e, mFont, mColorBlack, y, TextAlign.Left, 3);
            y = mPOSPrinter.POSDrawString("Ngày Bán:" + Utilities.DateTimeConverter.ConvertDateTimeToStringDMYH(mBOPrintOrder.NgayBan), e, mFont, mColorBlack, y, TextAlign.Left, 3);
            y += mPOSPrinter.POSGetFloat(10);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center,3);
            y += mPOSPrinter.POSGetFloat(5);
            int totalCount = 0;
            int totalCountCancel = 0;
            float yTmp=0;
            foreach (var item in mListPrintOrderItem)
            {
                if (yTmp>0)
                {
                    y += mPOSPrinter.POSGetFloat(5);
                    mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.DashDot, y,1, TextAlign.Center,10);
                    y += mPOSPrinter.POSGetFloat(5);
                }
                yTmp=y;
                y = mPOSPrinter.POSDrawString(String.Format("{0,3:###}  {1}", item.SoLuong, item.TenMon), e, mFont, mColorBlack, y, TextAlign.Left, 10);
                if (item.SoLuong<0)
                {
                    mPOSPrinter.POSDrawCancelLine(e, yTmp, y,10);
                }
                if (item.SoLuong > 0)
                {
                    totalCount+=item.SoLuong;
                }
                else
                {
                    totalCountCancel+=(0-item.SoLuong);
                }
            }

            y += mPOSPrinter.POSGetFloat(5);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center,3);
            y += mPOSPrinter.POSGetFloat(10);
            System.Drawing.Font fontFoot=new System.Drawing.Font("Arial",14,System.Drawing.FontStyle.Bold);
            if (totalCount>0)
                y=mPOSPrinter.POSDrawString(String.Format("THÊM :{0}", totalCount), e, fontFoot, mColorBlack, y, TextAlign.Right, 10);
            if (totalCountCancel>0)            
                y=mPOSPrinter.POSDrawString(String.Format("HỦY :{0}", totalCountCancel), e, fontFoot, mColorBlack, y, TextAlign.Right, 10);            
        }
    }
}
