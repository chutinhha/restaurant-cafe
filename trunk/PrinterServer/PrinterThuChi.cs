using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PrinterServer
{
    public class PrinterThuChi
    {
        private POSPrinter mPOSPrinter;        
        private Data.BOMayIn mBOMayIn;
        private int mThuChiID;
        private Data.BOXuliMayIn mBOXuliMayIn;
        private Data.BOThuChi mBOThuChi;
        private List<Data.BOChiTietThuChi> mListChiTiet;
        private PrinterFont mPrinterFont;
        private float mWidthThanhTien;

        public PrinterThuChi(int thuchiID, Data.BOMayIn mayin, Data.BOXuliMayIn xuli)
        {
            mThuChiID = thuchiID;
            mBOMayIn = mayin;
            mBOXuliMayIn = xuli;
            mPOSPrinter = new POSPrinter();
            mPOSPrinter.POSSetPrinterName(mBOMayIn.TenMayIn);
            mPOSPrinter.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(mPOSPrinter_PrintPage);

            mPrinterFont = new PrinterFont(mBOXuliMayIn);
        }

        void mPOSPrinter_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float y = mPOSPrinter.POSGetFloat(20);
            mWidthThanhTien = e.Graphics.MeasureString("1.000.000", mPrinterFont.FontItemBody).Width;                        

            y = mPOSPrinter.POSDrawString(mBOThuChi.LoaiThuChi.TenLoaiThuChi, e, mPrinterFont.FontTitle, Color.Black, y, TextAlign.Center, 0);
            y = mPOSPrinter.POSDrawString(Utilities.DateTimeConverter.ConvertDateTimeToStringDMYH(DateTime.Now), e, mPrinterFont.FontInfo, Color.Black, y, TextAlign.Center, 0);
                        
            y += mPOSPrinter.POSGetFloat(10);

            if (mBOThuChi.NhanVien != null)
            {
                mPOSPrinter.POSDrawString("Nhân Viên: ", e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
                y = mPOSPrinter.POSDrawString(mBOThuChi.NhanVien.TenNhanVien, e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Right, 0);
            }
            
            if (mBOThuChi.ThuChi.GhiChu!=null || mBOThuChi.ThuChi.GhiChu!="")
            {
                y= mPOSPrinter.POSDrawString(String.Format("Ghi Chú: {0}",mBOThuChi.ThuChi.GhiChu), e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
            }
            if (mBOThuChi.ThuChi.LyDo != null || mBOThuChi.ThuChi.LyDo != "")
            {
                y = mPOSPrinter.POSDrawString(String.Format("Lý Do: {0}", mBOThuChi.ThuChi.LyDo), e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
            }


            if (mListChiTiet.Count>0)
            {
                float yTmp = 0;
                float widthItem = mPOSPrinter.POSGetWidthPrinter(e) - mWidthThanhTien;
                y += mPOSPrinter.POSGetFloat(10);
                mPOSPrinter.POSDrawLine(e, Color.Black, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
                y += mPOSPrinter.POSGetFloat(5);

                mPOSPrinter.POSDrawString("Chi Tiết", e, mPrinterFont.FontItemHeader, Color.Black, 0, y, widthItem, TextAlign.Left);                        
                y = mPOSPrinter.POSDrawString("T.Tiền", e, mPrinterFont.FontItemHeader, Color.Black, widthItem, y, mWidthThanhTien, TextAlign.Right);

                y += mPOSPrinter.POSGetFloat(5);
                mPOSPrinter.POSDrawLine(e, Color.Black, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
                y += mPOSPrinter.POSGetFloat(5);
                foreach (var item in mListChiTiet)
                {
                    if (yTmp>0)
                    {
                        y += mPOSPrinter.POSGetFloat(5);
                        mPOSPrinter.POSDrawLine(e, Color.Black, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
                        y += mPOSPrinter.POSGetFloat(5);
                    }
                    yTmp=y;

                    mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(item.ChiTietThuChi.SoTien), e, mPrinterFont.FontItemBody, Color.Black, widthItem, y, mWidthThanhTien, TextAlign.Right);
                    y = mPOSPrinter.POSDrawString(item.ChiTietThuChi.GhiChu, e, mPrinterFont.FontItemBody, Color.Black, 0, y, widthItem, TextAlign.Left);                
                }

                y += mPOSPrinter.POSGetFloat(5);
                mPOSPrinter.POSDrawLine(e, Color.Black, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
                y += mPOSPrinter.POSGetFloat(10);
            }
            
            mPOSPrinter.POSDrawString("Tổng Tiền: ", e, mPrinterFont.FontItemHeader, Color.Black, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(Utilities.MoneyFormat.ConvertToString(mBOThuChi.ThuChi.TongTien), e, mPrinterFont.FontItemHeader, Color.Black, y, TextAlign.Right, 0);
        }
        private float DrawLine(float y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            y += mPOSPrinter.POSGetFloat(5);
            mPOSPrinter.POSDrawLine(e, Color.Black, System.Drawing.Drawing2D.DashStyle.Dot, y, 1, TextAlign.Center, 0);
            y += mPOSPrinter.POSGetFloat(5);
            return y;
        }
        private void LoadData()
        {            
            mBOThuChi = mBOXuliMayIn.GetThuChi(mThuChiID);
            mListChiTiet = mBOXuliMayIn.GetChiTietThuChi(mThuChiID).ToList();
        }
        public void Print()
        {
            LoadData();
            if (mBOThuChi!=null)
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
    }
}
