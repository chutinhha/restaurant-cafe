using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrinterServer
{
    class PrinterSendOrder
    {
        private int mLichSuBanHangID;        
        private Data.BOXuliMayIn mXuLiMayIn;
        private POSPrinter mPOSPrinter;
        private Data.BOMayIn mBOMayIn;
        private static float WIDTH_SO_LUONG = 25;
        private System.Drawing.Font mFontTitle;
        private System.Drawing.Font mFontInfo;
        private System.Drawing.Font mFontItem;
        private System.Drawing.Font mFontFoot;
        
        private System.Drawing.Color mColorBlack;
        private static string FONT_NAME = "Times New Roman";
        private Data.BOPrintOrder mBOPrintOrder;
        private List<Data.BOPrintOrderItem> mListPrintOrderItem;
        public PrinterSendOrder(int lichsu,Data.BOMayIn mayin,Data.BOXuliMayIn xuli)
        {            
            mBOMayIn = mayin;
            mLichSuBanHangID = lichsu;
            mXuLiMayIn = xuli;            
            mFontTitle = new System.Drawing.Font(FONT_NAME, (float)mXuLiMayIn._CAIDATMAYINBEP.TitleTextFontSize, (System.Drawing.FontStyle)mXuLiMayIn._CAIDATMAYINBEP.TitleTextFontStyle);
            mFontInfo = new System.Drawing.Font(FONT_NAME, (float)mXuLiMayIn._CAIDATMAYINBEP.InfoTextFontSize, (System.Drawing.FontStyle)mXuLiMayIn._CAIDATMAYINBEP.InfoTextFontStyle);
            mFontItem = new System.Drawing.Font(FONT_NAME, (float)mXuLiMayIn._CAIDATMAYINBEP.ItemTextFontSize, (System.Drawing.FontStyle)mXuLiMayIn._CAIDATMAYINBEP.ItemTextFontStyle);
            mFontFoot = new System.Drawing.Font(FONT_NAME, (float)mXuLiMayIn._CAIDATMAYINBEP.SumTextFontSize, (System.Drawing.FontStyle)mXuLiMayIn._CAIDATMAYINBEP.SumTextFontStyle);

            mColorBlack = System.Drawing.Color.Black;
            mPOSPrinter = new POSPrinter();
            mPOSPrinter.POSSetPrinterName(mBOMayIn.TenMayIn);            
            mPOSPrinter.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrinterData_PrintPage);            
        }
        private void LoadData()
        {
            mBOPrintOrder = mXuLiMayIn.GetOrderFromLichSuBanHang(mLichSuBanHangID).FirstOrDefault();
            mListPrintOrderItem = mXuLiMayIn.GetPrintOrderItem(mLichSuBanHangID, mBOMayIn.MayInID).ToList();
            foreach (var item in mListPrintOrderItem)
            {
                item._ListKhuyenMai = mXuLiMayIn.GetPrintOrderItemKM(item, mBOMayIn.MayInID).ToList();
            }
        }
        public void Print()
        {
            LoadData();
            if (mBOPrintOrder!=null)
            {
                for (int i = 0; i < mBOMayIn.SoLanIn; i++)
                {
                    try
                    {
                        mPOSPrinter.Print();
                    }
                    catch (Exception)
                    {                        
                    }
                }
            }
        }
        void PrinterData_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float y = mPOSPrinter.POSGetFloat(30);
            y = mPOSPrinter.POSDrawString(mBOMayIn.TieuDeIn, e, mFontTitle, mColorBlack, y, TextAlign.Center, 0);
            y += mPOSPrinter.POSGetFloat(30);
            y = mPOSPrinter.POSDrawString("Mã HĐ:" + mBOPrintOrder.MaHoaDon, e, mFontInfo, mColorBlack, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString("Bàn:" + mBOPrintOrder.TenBan, e, mFontInfo, mColorBlack, y, TextAlign.Left, 0);            
            y = mPOSPrinter.POSDrawString("Nhân Viên:" + mBOPrintOrder.TenNhanVien, e, mFontInfo, mColorBlack, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString("Ngày: " + Utilities.DateTimeConverter.ConvertDateTimeToStringDMYH(mBOPrintOrder.NgayBan), e, mFontInfo, mColorBlack, y, TextAlign.Left, 0);
            y += mPOSPrinter.POSGetFloat(15);            
            int totalCount = 0;
            int totalCountCancel = 0;
            float yTmp=0;
            float widthName=mPOSPrinter.POSGetWidthPrinter(e)-WIDTH_SO_LUONG;

            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
            y += mPOSPrinter.POSGetFloat(5);

            mPOSPrinter.POSDrawString("S.lượng", e, mFontItem, mColorBlack, widthName, y, WIDTH_SO_LUONG, TextAlign.Right);
            y = mPOSPrinter.POSDrawString("Tên món", e, mFontItem, mColorBlack, 0, y, widthName, TextAlign.Left);
           

            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
            y += mPOSPrinter.POSGetFloat(5);

            foreach (var item in mListPrintOrderItem)
            {                
                if (yTmp>0)
                {
                    y += mPOSPrinter.POSGetFloat(5);
                    mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Dot, y,1, TextAlign.Center,0);
                    y += mPOSPrinter.POSGetFloat(5);
                }
                yTmp=y;
                mPOSPrinter.POSDrawString(String.Format("{0}",item.SoLuong), e, mFontItem, mColorBlack, widthName, y, WIDTH_SO_LUONG, TextAlign.Right);
                y=mPOSPrinter.POSDrawString(item.TenMon, e, mFontItem, mColorBlack, 0, y, widthName, TextAlign.Left);
                foreach (var km in item._ListKhuyenMai)
                {
                    y = mPOSPrinter.POSDrawString(String.Format("+{0}", km.TenMon), e, mFontItem, mColorBlack,mPOSPrinter.POSGetFloat(10) , y, widthName, TextAlign.Left);
                }
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
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center,0);
            y += mPOSPrinter.POSGetFloat(10);
            if (totalCount > 0)
            {
                mPOSPrinter.POSDrawString(String.Format("{0}", totalCount), e, mFontFoot, mColorBlack, widthName, y, WIDTH_SO_LUONG, TextAlign.Right);
                y = mPOSPrinter.POSDrawString("THÊM: ", e, mFontItem, mColorBlack, 0, y, widthName, TextAlign.Right);                
            }
            if (totalCountCancel > 0)
            {                
                mPOSPrinter.POSDrawString(String.Format("{0}", totalCountCancel), e, mFontFoot, mColorBlack, widthName, y, WIDTH_SO_LUONG, TextAlign.Right);
                y = mPOSPrinter.POSDrawString("HỦY: ", e, mFontItem, mColorBlack, 0, y, widthName, TextAlign.Right);                
            }
        }
    }
}
