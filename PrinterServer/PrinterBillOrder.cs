using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrinterServer
{
    public class PrinterBillOrder
    {
        public enum PrinterBillOrderType
        {
            HoaDon=0,
            TamTinh=1,
            InLai=2
        };
        private POSPrinter mPOSPrinter;
        private int mBanHangID;
        private static string[] PRINT_BILL_TYPE = { "", "(TẠM TÍNH)" ,"(IN LẠI)"};
        private static string FONT_NAME = "Times New Roman";
        private static float WIDTH_SO_LUONG = 17;
        private static float WIDTH_THANH_TIEN = 73;
        private static float WIDTH_DON_GIA = 73;
        private PrinterBillOrderType mPrinterBillOrderType;
        private Data.BOMayIn mBOMayIn;
        private Data.BOXuliMayIn mBOXuliMayIn;
        private Data.BOPrintOrder mBOPrintOrder;
        private List<Data.BOPrintOrderItem> mListPrintOrderItem;

        private System.Drawing.Font mFontHeader1;
        private System.Drawing.Font mFontHeader2;
        private System.Drawing.Font mFontHeader3;
        private System.Drawing.Font mFontHeader4;
        private System.Drawing.Font mFontTitle;
        private System.Drawing.Font mFontInfo;
        private System.Drawing.Font mFontItemHeader;
        private System.Drawing.Font mFontItemBody;
        private System.Drawing.Font mFontItemBodyNote;
        private System.Drawing.Font mFontSum;
        private System.Drawing.Font mFontBig;
        private System.Drawing.Font mFontFooter1;
        private System.Drawing.Font mFontFooter2;
        private System.Drawing.Font mFontFooter3;
        private System.Drawing.Font mFontFooter4;



        private System.Drawing.Color mColorBlack;

        public PrinterBillOrder(PrinterBillOrderType type, int banHangID, Data.BOMayIn mayin, Data.BOXuliMayIn xuli)
        {
            mBOMayIn = mayin;
            mBOXuliMayIn = xuli;
            mBanHangID = banHangID;
            mPrinterBillOrderType = type;

            mFontHeader1 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontSize1, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontStyle1);
            mFontHeader2 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontSize2, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontStyle2);
            mFontHeader3 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontSize3, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontStyle3);
            mFontHeader4 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontSize4, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextFontStyle4);

            mFontTitle = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.TitleTextFontSize, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.TitleTextFontStyle);
            mFontInfo = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.InfoTextFontSize, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.InfoTextFontStyle);
            mFontItemHeader = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.InfoTextFontSize, System.Drawing.FontStyle.Bold);
            mFontItemBody = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.InfoTextFontSize, System.Drawing.FontStyle.Regular);
            mFontItemBodyNote = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.InfoTextFontSize, System.Drawing.FontStyle.Italic);
            mFontSum = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.SumanyFontSize, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.SumanyFontStyle);
            mFontBig = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.SumanyFontSizeBig, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.SumanyFontStyleBig);
            mFontFooter1 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontSize1, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontStyle1);
            mFontFooter2 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontSize2, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontStyle2);
            mFontFooter3 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontSize3, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontStyle3);
            mFontFooter4 = new System.Drawing.Font(FONT_NAME, (float)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontSize4, (System.Drawing.FontStyle)mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextFontStyle4);

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
            float y = 0;
            float x = 0;                     

            y += mPOSPrinter.POSGetFloat(10);

            x = 0;
            float yLogo = y;
            if (mBOXuliMayIn._ImageLogo!=null)
	        {                
                yLogo = mPOSPrinter.POSDrawImage(mBOXuliMayIn._ImageLogo, e, x, y, mBOXuliMayIn._CAIDATMAYINHOADON.LogoWidth, mBOXuliMayIn._CAIDATMAYINHOADON.LogoHeight);
	        }
            x = mBOXuliMayIn._CAIDATMAYINHOADON.LogoWidth;
            float widthHeader=mPOSPrinter.POSGetWidthPrinter(e)-x-10;
            if (mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString1 != null && mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString1!="")
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString1, e, mFontHeader1, mColorBlack,x, y,widthHeader, TextAlign.Center);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString2 != null && mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString2!="")
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString2, e, mFontHeader2, mColorBlack, x, y, widthHeader, TextAlign.Center);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString3 != null && mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString3!="")
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString3, e, mFontHeader3, mColorBlack, x, y, widthHeader, TextAlign.Center);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString4 != null && mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString4!="")
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString4, e, mFontHeader4, mColorBlack, x, y, widthHeader, TextAlign.Center);

            y = yLogo > y ? yLogo : y;

            y += mPOSPrinter.POSGetFloat(5);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Dash, y, 0.3f, TextAlign.Center, 0);
            y += mPOSPrinter.POSGetFloat(30);

            y = mPOSPrinter.POSDrawString(mBOMayIn.TieuDeIn, e, mFontTitle, mColorBlack, y, TextAlign.Center, 0);
            string note=PRINT_BILL_TYPE[(int)mPrinterBillOrderType];
            if (note.Length>0)
                y = mPOSPrinter.POSDrawString(note, e, mFontTitle, mColorBlack, y, TextAlign.Center, 0);    

            y += mPOSPrinter.POSGetFloat(30);

            y = mPOSPrinter.POSDrawString("Mã HĐ: " + mBOPrintOrder.MaHoaDon, e, mFontInfo, mColorBlack, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString("Bàn: " + mBOPrintOrder.TenBan, e, mFontInfo, mColorBlack, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString("Nhân Viên: " + mBOPrintOrder.TenNhanVien, e, mFontInfo, mColorBlack, y, TextAlign.Left, 0);
            if (mBOPrintOrder.KhachHang!=null)
            {
                y = mPOSPrinter.POSDrawString("Khách Hàng: " + mBOPrintOrder.KhachHang.TenKhachHang, e, mFontInfo, mColorBlack, y, TextAlign.Left, 0);
                if (mBOPrintOrder.KhachHang.Mobile.Length>0)
                    y = mPOSPrinter.POSDrawString("Điện Thoại: " + mBOPrintOrder.KhachHang.Mobile, e, mFontInfo, mColorBlack, y, TextAlign.Left, 0);
            }
            y = mPOSPrinter.POSDrawString("Ngày: " + Utilities.DateTimeConverter.ConvertDateTimeToStringDMYH(mBOPrintOrder.NgayBan), e, mFontInfo, mColorBlack, y, TextAlign.Left, 0);
            

            float widthItem = mPOSPrinter.POSGetWidthPrinter(e) - WIDTH_SO_LUONG - WIDTH_THANH_TIEN-WIDTH_DON_GIA;
            float yTmp = 0;

            y += mPOSPrinter.POSGetFloat(10);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
            y += mPOSPrinter.POSGetFloat(5);

            x = 0;
            mPOSPrinter.POSDrawString("Tên món", e, mFontItemHeader, mColorBlack, 0, y, widthItem, TextAlign.Left);
            x += widthItem;
            mPOSPrinter.POSDrawString("SL.", e, mFontItemHeader, mColorBlack, x, y, WIDTH_SO_LUONG, TextAlign.Right);
            x += WIDTH_SO_LUONG;
            mPOSPrinter.POSDrawString("Đ.Giá", e, mFontItemHeader, mColorBlack, x, y, WIDTH_DON_GIA, TextAlign.Right);
            x += WIDTH_DON_GIA;
            y=mPOSPrinter.POSDrawString("T.Tiền", e, mFontItemHeader, mColorBlack, x, y, WIDTH_THANH_TIEN, TextAlign.Right);

            y += mPOSPrinter.POSGetFloat(5);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
            y += mPOSPrinter.POSGetFloat(5);

            foreach (var item in mListPrintOrderItem)
            {
                if (yTmp > 0)
                {
                    y += mPOSPrinter.POSGetFloat(5);
                    mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Dot, y, 1, TextAlign.Center, 0);
                    y += mPOSPrinter.POSGetFloat(5);
                }
                yTmp = y;
                x=0;
                y=mPOSPrinter.POSDrawString(item.TenMon, e, mFontItemBody, mColorBlack, x, yTmp, widthItem, TextAlign.Left);
                if (item.GiamGia>0)
                {
                    y=mPOSPrinter.POSDrawString(String.Format("Giảm giá: {0}%",item.GiamGia), e, mFontItemBodyNote, mColorBlack, x, y, widthItem, TextAlign.Left);
                }
                x += widthItem;
                mPOSPrinter.POSDrawString(String.Format("{0}", item.SoLuong), e, mFontItemBody, mColorBlack, x, yTmp, WIDTH_SO_LUONG, TextAlign.Right);
                x += WIDTH_SO_LUONG;
                mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(item.GiaBan), e, mFontItemBody, mColorBlack, x, yTmp, WIDTH_DON_GIA, TextAlign.Right);
                x += WIDTH_DON_GIA;
                mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(item.ThanhTien), e, mFontItemBody, mColorBlack, x, yTmp, WIDTH_THANH_TIEN, TextAlign.Right);
                
            }

            y += mPOSPrinter.POSGetFloat(5);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
            y += mPOSPrinter.POSGetFloat(10);            


            mPOSPrinter.POSDrawString("T.CỘNG: ", e, mFontSum, mColorBlack, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.BanHang.TongTien), e, mFontSum, mColorBlack, y, TextAlign.Right, 0);            
            if (mBOPrintOrder.TienGiam > 0)
            {                
                mPOSPrinter.POSDrawString("GIẢM GIÁ: ", e, mFontSum, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.TienGiam), e, mFontSum, mColorBlack, y, TextAlign.Right, 0);
                //if (mPrinterBillOrderType==PrinterBillOrderType.TamTinh)
                //{
                //    mPOSPrinter.POSDrawString("PHẢI TRẢ: ", e, mFontSum, mColorBlack, y, TextAlign.Left, 0);
                //    y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.TienPhaiTra), e, mFontSum, mColorBlack, y, TextAlign.Right, 0);
                //}
                mPOSPrinter.POSDrawString("PHẢI TRẢ: ", e, mFontSum, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.TienPhaiTra), e, mFontSum, mColorBlack, y, TextAlign.Right, 0);
            }
            y=mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ReadNumber((double)mBOPrintOrder.TienPhaiTra),e,mFontItemBodyNote,mColorBlack,y,TextAlign.Left,0);
            if (mBOPrintOrder.BanHang.TienThe > 0)
            {
                mPOSPrinter.POSDrawString("THẺ: ", e, mFontBig, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.BanHang.TienThe), e, mFontBig, mColorBlack, y, TextAlign.Right, 0);
                if (mBOPrintOrder.TenThe != null)
                    y = mPOSPrinter.POSDrawString(mBOPrintOrder.TenThe, e, mFontSum, mColorBlack, y, TextAlign.Left, 0);
            }
            if (mBOPrintOrder.BanHang.TienKhacHang > 0)
            {
                mPOSPrinter.POSDrawString("KHÁCH ĐƯA: ", e, mFontBig, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.BanHang.TienKhacHang), e, mFontBig, mColorBlack, y, TextAlign.Right, 0);

            }
            if (mBOPrintOrder.BanHang.TienTraLai > 0)
            {
                mPOSPrinter.POSDrawString("TRẢ LẠI: ", e, mFontSum, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.BanHang.TienTraLai), e, mFontSum, mColorBlack, y, TextAlign.Right, 0);
            }

            y += mPOSPrinter.POSGetFloat(10);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Dash, y, 0.5f, TextAlign.Center, 3);
            y += mPOSPrinter.POSGetFloat(5);

            if (mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString1 != null)
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString1, e, mFontFooter1, mColorBlack, y, TextAlign.Center, 0);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString2 != null)
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString2, e, mFontFooter2, mColorBlack, y, TextAlign.Center, 0);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString3 != null)
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString3, e, mFontFooter3, mColorBlack, y, TextAlign.Center, 0);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString4 != null)
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString4, e, mFontFooter4, mColorBlack, y, TextAlign.Center, 0);
        }
    }
}
