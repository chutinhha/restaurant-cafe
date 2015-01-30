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
        private float mWidthSoLuong = 17;
        private float mWidthThanhTien = 73;
        private static float mWidthDonGia = 73;
        private PrinterBillOrderType mPrinterBillOrderType;
        private Data.BOMayIn mBOMayIn;
        private Data.BOXuliMayIn mBOXuliMayIn;
        private Data.BOPrintOrder mBOPrintOrder;
        private List<Data.BOPrintOrderItem> mListPrintOrderItem;
        private List<Data.BOPrintOrderItem> mListPrintOrderItemTime;
        private PrinterFont mPrinterFont;

        private System.Drawing.Color mColorBlack;

        public PrinterBillOrder(PrinterBillOrderType type, int banHangID, Data.BOMayIn mayin, Data.BOXuliMayIn xuli)
        {
            mBOMayIn = mayin;
            mBOXuliMayIn = xuli;
            mBanHangID = banHangID;
            mPrinterBillOrderType = type;

            mPrinterFont = new PrinterFont(mBOXuliMayIn);

            mColorBlack = System.Drawing.Color.Black;


            mPOSPrinter = new POSPrinter();
            mPOSPrinter.POSSetPrinterName(mBOMayIn.TenMayIn);
            mPOSPrinter.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrinterData_PrintPage);
        }
        private void LoadData()
        {            
            mBOPrintOrder = mBOXuliMayIn.GetOrderFromBanHangID(mBanHangID).FirstOrDefault();
            mListPrintOrderItem = mBOXuliMayIn.GetPrintOrderItemFromBanHangID(mBanHangID).ToList();
            foreach (var item in mListPrintOrderItem)
            {
                item._ListKhuyenMai = mBOXuliMayIn.GetPrintOrderItemKM(mBanHangID,item).ToList();
            }
            mListPrintOrderItemTime = mBOXuliMayIn.GetPrintOrderItemTimeFromBanHangID(mBanHangID).ToList();
            foreach (var item in mListPrintOrderItemTime)
            {
                item._ListKhuyenMai = mBOXuliMayIn.GetPrintOrderItemKM(mBanHangID, item).ToList();
            }
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

            mWidthSoLuong = e.Graphics.MeasureString("888",mPrinterFont.FontItemBody).Width;
            mWidthDonGia = e.Graphics.MeasureString("1.000.000", mPrinterFont.FontItemBody).Width;
            mWidthThanhTien = e.Graphics.MeasureString("1.000.000", mPrinterFont.FontItemBody).Width;

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
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString1, e, mPrinterFont.FontHeader1, mColorBlack,x, y,widthHeader, TextAlign.Center);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString2 != null && mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString2!="")
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString2, e, mPrinterFont.FontHeader2, mColorBlack, x, y, widthHeader, TextAlign.Center);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString3 != null && mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString3!="")
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString3, e, mPrinterFont.FontHeader3, mColorBlack, x, y, widthHeader, TextAlign.Center);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString4 != null && mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString4!="")
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.HeaderTextString4, e, mPrinterFont.FontHeader4, mColorBlack, x, y, widthHeader, TextAlign.Center);

            y = yLogo > y ? yLogo : y;

            y += mPOSPrinter.POSGetFloat(5);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Dash, y, 0.3f, TextAlign.Center, 0);
            y += mPOSPrinter.POSGetFloat(30);

            y = mPOSPrinter.POSDrawString(mBOMayIn.TieuDeIn, e, mPrinterFont.FontTitle, mColorBlack, y, TextAlign.Center, 0);
            string note=PRINT_BILL_TYPE[(int)mPrinterBillOrderType];
            if (note.Length>0)
                y = mPOSPrinter.POSDrawString(note, e, mPrinterFont.FontTitle, mColorBlack, y, TextAlign.Center, 0);    

            y += mPOSPrinter.POSGetFloat(30);

            y = mPOSPrinter.POSDrawString("Mã HĐ: " + mBOPrintOrder.MaHoaDon, e, mPrinterFont.FontInfo, mColorBlack, y, TextAlign.Left, 0);
            if (mBOPrintOrder.TenBan!=null)
            {
                y = mPOSPrinter.POSDrawString("Bàn: " + mBOPrintOrder.TenBan, e, mPrinterFont.FontInfo, mColorBlack, y, TextAlign.Left, 0);
            }
            y = mPOSPrinter.POSDrawString("Nhân Viên: " + mBOPrintOrder.TenNhanVien, e, mPrinterFont.FontInfo, mColorBlack, y, TextAlign.Left, 0);
            if (mBOPrintOrder.KhachHang!=null)
            {
                y = mPOSPrinter.POSDrawString("Khách Hàng: " + mBOPrintOrder.KhachHang.TenKhachHang, e, mPrinterFont.FontInfo, mColorBlack, y, TextAlign.Left, 0);
                if (mBOPrintOrder.KhachHang.Mobile.Length>0)
                    y = mPOSPrinter.POSDrawString("Điện Thoại: " + mBOPrintOrder.KhachHang.Mobile, e, mPrinterFont.FontInfo, mColorBlack, y, TextAlign.Left, 0);
            }
            y = mPOSPrinter.POSDrawString("Ngày: " + Utilities.DateTimeConverter.ConvertDateTimeToStringDMYH(mBOPrintOrder.NgayBan), e, mPrinterFont.FontInfo, mColorBlack, y, TextAlign.Left, 0);
            float widthItem = 0;
            if (mListPrintOrderItemTime!=null &&mListPrintOrderItemTime.Count>0)
            {
                widthItem = mPOSPrinter.POSGetWidthPrinter(e) - mWidthThanhTien;
                y += mPOSPrinter.POSGetFloat(10);
                mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.DashDotDot, y, 1, TextAlign.Center, 10);
                y += mPOSPrinter.POSGetFloat(5);

                foreach (var item in mListPrintOrderItemTime)
                {
                    mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(item.ThanhTien), e, mPrinterFont.FontItemHeader, mColorBlack, widthItem, y, mWidthThanhTien, TextAlign.Right);
                    y = mPOSPrinter.POSDrawString(item.TenMon, e, mPrinterFont.FontItemHeader, mColorBlack, 0, y, widthItem, TextAlign.Left);                    
                }
                y = mPOSPrinter.POSDrawString(String.Format("Bắt đầu:{0}",Utilities.DateTimeConverter.ConvertDateTimeToStringDMYH(mBOPrintOrder.NgayBan)), e, mPrinterFont.FontItemBody, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(String.Format("Kết thúc:{0}", Utilities.DateTimeConverter.ConvertDateTimeToStringDMYH(mBOPrintOrder.NgayKetThuc)), e, mPrinterFont.FontItemBody, mColorBlack, y, TextAlign.Left, 0);

                y += mPOSPrinter.POSGetFloat(5);
                mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.DashDotDot, y, 1, TextAlign.Center, 10);
                y += mPOSPrinter.POSGetFloat(10);
            }
                        
            if (mListPrintOrderItem!=null && mListPrintOrderItem.Count>0)
            {
                widthItem = mPOSPrinter.POSGetWidthPrinter(e) - mWidthSoLuong - mWidthThanhTien - mWidthDonGia;
                float yTmp = 0;
                y += mPOSPrinter.POSGetFloat(10);
                mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
                y += mPOSPrinter.POSGetFloat(5);

                x = 0;
                mPOSPrinter.POSDrawString("Tên món", e, mPrinterFont.FontItemHeader, mColorBlack, 0, y, widthItem, TextAlign.Left);
                x += widthItem;
                mPOSPrinter.POSDrawString("SL.", e, mPrinterFont.FontItemHeader, mColorBlack, x, y, mWidthSoLuong, TextAlign.Right);
                x += mWidthSoLuong;
                mPOSPrinter.POSDrawString("Đ.Giá", e, mPrinterFont.FontItemHeader, mColorBlack, x, y, mWidthDonGia, TextAlign.Right);
                x += mWidthDonGia;
                y=mPOSPrinter.POSDrawString("T.Tiền", e, mPrinterFont.FontItemHeader, mColorBlack, x, y, mWidthThanhTien, TextAlign.Right);

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
                    y=mPOSPrinter.POSDrawString(item.TenMon, e, mPrinterFont.FontItemBody, mColorBlack, x, y, widthItem, TextAlign.Left);
                    if (item.GiamGia>0)
                    {
                        y=mPOSPrinter.POSDrawString(String.Format("Giảm giá: {0}%",item.GiamGia), e, mPrinterFont.FontItemBodyNote, mColorBlack, x, y, widthItem, TextAlign.Left);
                    }
                    foreach (var km in item._ListKhuyenMai)
                    {
                        y = mPOSPrinter.POSDrawString(String.Format("+{0}", km.TenMon), e, mPrinterFont.FontItemBodyNote, mColorBlack, x + mPOSPrinter.POSGetFloat(10), y, widthItem, TextAlign.Left);
                    }
                    x += widthItem;
                    mPOSPrinter.POSDrawString(String.Format("{0}", item.SoLuong), e, mPrinterFont.FontItemBody, mColorBlack, x, yTmp, mWidthSoLuong, TextAlign.Right);
                    x += mWidthSoLuong;
                    mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(item.GiaBan), e, mPrinterFont.FontItemBody, mColorBlack, x, yTmp, mWidthDonGia, TextAlign.Right);
                    x += mWidthDonGia;
                    mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(item.ThanhTien), e, mPrinterFont.FontItemBody, mColorBlack, x, yTmp, mWidthThanhTien, TextAlign.Right);
                
                }

                y += mPOSPrinter.POSGetFloat(5);
                mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
                y += mPOSPrinter.POSGetFloat(10);            
            }

            mPOSPrinter.POSDrawString("T.CỘNG: ", e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.BanHang.TongTien), e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Right, 0);            
            if (mBOPrintOrder.TienGiam > 0)
            {                
                mPOSPrinter.POSDrawString("GIẢM GIÁ: ", e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString("-"+Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.TienGiam), e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Right, 0);                                
            }
            if (mBOPrintOrder.TienPhiDichVu > 0)
            {
                mPOSPrinter.POSDrawString("PHÍ DỊCH VỤ: ", e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.TienPhiDichVu), e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Right, 0);
            }
            if (mBOPrintOrder.TienThueVAT > 0)
            {
                mPOSPrinter.POSDrawString("VAT: ", e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.TienThueVAT), e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Right, 0);
            }
            mPOSPrinter.POSDrawString("PHẢI TRẢ: ", e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.TongTienPhaiTra), e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Right, 0);
            y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ReadNumber((double)mBOPrintOrder.TongTienPhaiTra), e, mPrinterFont.FontItemBodyNote, mColorBlack, y, TextAlign.Left, 0);
            if (mBOPrintOrder.BanHang.TienThe > 0)
            {
                mPOSPrinter.POSDrawString("THẺ: ", e, mPrinterFont.FontBig, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.BanHang.TienThe), e, mPrinterFont.FontBig, mColorBlack, y, TextAlign.Right, 0);
                if (mBOPrintOrder.TenThe != null)
                    y = mPOSPrinter.POSDrawString(mBOPrintOrder.TenThe, e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Left, 0);
            }
            if (mBOPrintOrder.BanHang.TienKhacHang > 0)
            {
                mPOSPrinter.POSDrawString("KHÁCH ĐƯA: ", e, mPrinterFont.FontBig, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.BanHang.TienKhacHang), e, mPrinterFont.FontBig, mColorBlack, y, TextAlign.Right, 0);

            }
            if (mBOPrintOrder.BanHang.TienTraLai > 0)
            {
                mPOSPrinter.POSDrawString("TRẢ LẠI: ", e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOPrintOrder.BanHang.TienTraLai), e, mPrinterFont.FontSum, mColorBlack, y, TextAlign.Right, 0);
            }

            y += mPOSPrinter.POSGetFloat(10);
            mPOSPrinter.POSDrawLine(e, mColorBlack, System.Drawing.Drawing2D.DashStyle.Dash, y, 0.5f, TextAlign.Center, 3);
            y += mPOSPrinter.POSGetFloat(5);

            if (mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString1 != null)
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString1, e, mPrinterFont.FontFooter1, mColorBlack, y, TextAlign.Center, 0);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString2 != null)
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString2, e, mPrinterFont.FontFooter2, mColorBlack, y, TextAlign.Center, 0);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString3 != null)
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString3, e, mPrinterFont.FontFooter3, mColorBlack, y, TextAlign.Center, 0);
            if (mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString4 != null)
                y = mPOSPrinter.POSDrawString(mBOXuliMayIn._CAIDATMAYINHOADON.FooterTextString4, e, mPrinterFont.FontFooter4, mColorBlack, y, TextAlign.Center, 0);
        }
    }
}
