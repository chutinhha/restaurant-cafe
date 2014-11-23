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
        private static string TAM_TINH="(TẠM TÍNH)";
        private bool mTamTinh;
        private Data.BOMayIn mBOMayIn;
        private Data.BOXuliMayIn mBOXuliMayIn;
        private Data.BOPrintOrder mBOPrintOrder;
        private List<Data.BOPrintOrderItem> mListPrintOrderItem;

        private System.Drawing.Font mFont;
        private System.Drawing.Font mFontTopHeader;
        private System.Drawing.Font mFontHeader;
        private System.Drawing.Font mFontSum;
        private System.Drawing.Font mFontFoot;
        private System.Drawing.Color mColorBlack;

        public PrinterBillOrder(bool tamTinh,int banHangID, Data.BOMayIn mayin, Data.BOXuliMayIn xuli)
        {
            mBOMayIn = mayin;
            mBOXuliMayIn = xuli;
            mBanHangID = banHangID;
            mTamTinh = tamTinh;

            mFont = new System.Drawing.Font("Arial", 12);
            mFontTopHeader = new System.Drawing.Font("Arial", 10);
            mFontHeader = new System.Drawing.Font("Arial", 18, System.Drawing.FontStyle.Bold);
            mFontSum = new System.Drawing.Font("Arial", 12);
            mFontFoot = new System.Drawing.Font("Arial", 10,System.Drawing.FontStyle.Italic);

            mColorBlack = System.Drawing.Color.Black;

            mPOSPrinter = new POSPrinter();
            mPOSPrinter.POSSetPrinterName(mBOMayIn.TenMayIn);
            mPOSPrinter.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrinterData_PrintPage);
        }
        private void LoadData()
        {
            mBOPrintOrder = mBOXuliMayIn.GetOrderFromBanHangID(mBanHangID).FirstOrDefault();
            mListPrintOrderItem = mBOXuliMayIn.GetPrintOrderItemFromBanHangID(mBanHangID, mBOMayIn.MayInID).ToList();
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
        void PrinterData_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float y = 0;
            y += mPOSPrinter.POSGetFloat(10);
            y = mPOSPrinter.POSDrawString("Cafe ABC", e, mFontTopHeader, mColorBlack, y, TextAlign.Center, 3);
            y = mPOSPrinter.POSDrawString("Số 2, Thành Thái, Phường 3, Quận 10, HCM", e, mFontTopHeader, mColorBlack, y, TextAlign.Center, 3);
            y += mPOSPrinter.POSGetFloat(5);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Dash, y, 0.3f, TextAlign.Center, 3);

            y += mPOSPrinter.POSGetFloat(30);
            y = mPOSPrinter.POSDrawString(mBOMayIn.TieuDeIn, e, mFontHeader, mColorBlack, y, TextAlign.Center, 3);
            if (mTamTinh)
                y = mPOSPrinter.POSDrawString(TAM_TINH, e, mFontHeader, mColorBlack, y, TextAlign.Center, 3);    
            y += mPOSPrinter.POSGetFloat(30);
            mPOSPrinter.POSDrawString("Tên Bàn:" + mBOPrintOrder.TenBan, e, mFont, mColorBlack, y, TextAlign.Left, 3);
            y = mPOSPrinter.POSDrawString("Hóa Đơn:" + mBOPrintOrder.MaHoaDon, e, mFont, mColorBlack, y, TextAlign.Right, 3);
            y = mPOSPrinter.POSDrawString("Nhân Viên:" + mBOPrintOrder.TenNhanVien, e, mFont, mColorBlack, y, TextAlign.Left, 3);
            y = mPOSPrinter.POSDrawString("Ngày Bán:" + Utilities.DateTimeConverter.ConvertDateTimeToStringDMYH(mBOPrintOrder.NgayBan), e, mFont, mColorBlack, y, TextAlign.Left, 3);
            y += mPOSPrinter.POSGetFloat(10);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 3);
            y += mPOSPrinter.POSGetFloat(5);
            
            float yTmp = 0;
            foreach (var item in mListPrintOrderItem)
            {
                if (yTmp > 0)
                {
                    y += mPOSPrinter.POSGetFloat(5);
                    mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.DashDot, y, 1, TextAlign.Center, 10);
                    y += mPOSPrinter.POSGetFloat(5);
                }
                yTmp = y;
                y = mPOSPrinter.POSDrawString(String.Format("{0,3:###}  {1}", item.SoLuong, item.TenMon), e, mFont, mColorBlack, y, TextAlign.Left, 10);
                y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToStringFull(item.ThanhTien),e,mFont,mColorBlack,y,TextAlign.Right,10);                
            }

            y += mPOSPrinter.POSGetFloat(5);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 3);
            y += mPOSPrinter.POSGetFloat(10);            
            y = mPOSPrinter.POSDrawString("TỔNG: " + Utilities.MoneyFormat.ConvertToStringFull(mBOPrintOrder.BanHang.TongTien), e, mFontSum, mColorBlack, y, TextAlign.Right, 10);
            if (mBOPrintOrder.BanHang.TienGiam > 0)
            {
                y = mPOSPrinter.POSDrawString("GIẢM GIÁ: " + Utilities.MoneyFormat.ConvertToStringFull(mBOPrintOrder.BanHang.TienGiam), e, mFontSum, mColorBlack, y, TextAlign.Right, 10);
                if (mTamTinh)
                {
                    y = mPOSPrinter.POSDrawString("PHẢI TRẢ: " + Utilities.MoneyFormat.ConvertToStringFull(mBOPrintOrder.BanHang.TongTien-mBOPrintOrder.BanHang.TienGiam), e, mFontSum, mColorBlack, y, TextAlign.Right, 10);    
                }
            }
            if (mBOPrintOrder.BanHang.TienThe>0)
                y = mPOSPrinter.POSDrawString("THẺ: " + Utilities.MoneyFormat.ConvertToStringFull(mBOPrintOrder.BanHang.TienThe), e, mFontSum, mColorBlack, y, TextAlign.Right, 10);
            if (mBOPrintOrder.BanHang.TienKhacHang > 0)
                y = mPOSPrinter.POSDrawString("KHÁCH ĐƯA: " + Utilities.MoneyFormat.ConvertToStringFull(mBOPrintOrder.BanHang.TienKhacHang), e, mFontSum, mColorBlack, y, TextAlign.Right, 10);
            if (mBOPrintOrder.BanHang.TienTraLai > 0)
                y = mPOSPrinter.POSDrawString("TRẢ LẠI: " + Utilities.MoneyFormat.ConvertToStringFull(mBOPrintOrder.BanHang.TienTraLai), e, mFontSum, mColorBlack, y, TextAlign.Right, 10);
            y += mPOSPrinter.POSGetFloat(10);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Dash, y, 0.5f, TextAlign.Center, 3);
            y += mPOSPrinter.POSGetFloat(5);
            y = mPOSPrinter.POSDrawString("Thanks you! :=)", e, mFontFoot, mColorBlack, y, TextAlign.Center, 5);
            y = mPOSPrinter.POSDrawString("Hẹn gặp lại quý khách lần sau !", e, mFontFoot, mColorBlack, y, TextAlign.Center, 5);
            y = mPOSPrinter.POSDrawString("---(^_^)---", e, mFontFoot, mColorBlack, y, TextAlign.Center, 5);
        }
    }
}
