using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PrinterServer
{
    public class PrinterReportDaily
    {
        private POSPrinter mPOSPrinter;
        private DateTime mDateTimeFrom;
        private DateTime mDateTimeTo;
        private Data.BOMayIn mBOMayIn;
        private Data.BOXuliMayIn mBOXuliMayIn;
        private Data.BOPrinterReportDaily mBOPrinterReportDaily;

        private PrinterFont mPrinterFont;


        public PrinterReportDaily(DateTime dtFrom,DateTime dtTo,Data.BOMayIn mayin,Data.BOXuliMayIn xuli)
        {
            mDateTimeFrom = dtFrom;
            mDateTimeTo = dtTo;
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
            y = mPOSPrinter.POSDrawString(mBOPrinterReportDaily.CAIDATTHONGTINCONGTY.TenCongTy, e,mPrinterFont.FontSum, Color.Black, y, TextAlign.Center, 0);
            y = mPOSPrinter.POSDrawString(mBOPrinterReportDaily.CAIDATTHONGTINCONGTY.DiaChi, e, mPrinterFont.FontInfo, Color.Black, y, TextAlign.Center, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0}{1}", mBOPrinterReportDaily.CAIDATTHONGTINCONGTY.DienThoaiBan, mBOPrinterReportDaily.CAIDATTHONGTINCONGTY.DienThoaiDiDong), e, mPrinterFont.FontInfo, Color.Black, y, TextAlign.Center, 0);

            y += mPOSPrinter.POSGetFloat(50);

            mPOSPrinter.POSDrawLine(e, Color.Black, System.Drawing.Drawing2D.DashStyle.Solid, y, 1, TextAlign.Center, 0);
            y += mPOSPrinter.POSGetFloat(10);

            y = mPOSPrinter.POSDrawString("BÁO CÁO BÁN HÀNG", e, mPrinterFont.FontTitle, Color.Black, y, TextAlign.Center, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0} - {1}", Utilities.DateTimeConverter.ConvertToDateString(mDateTimeFrom), Utilities.DateTimeConverter.ConvertToDateString(mDateTimeTo)), e, mPrinterFont.FontInfo, Color.Black, y, TextAlign.Center, 0);
                        
            y += mPOSPrinter.POSGetFloat(10);

            mPOSPrinter.POSDrawString("Tiền mặt: ", e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0}", Utilities.MoneyFormat.ConvertToString(mBOPrinterReportDaily.BAOCAONGAYTONG.TienMat.Value)), e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Right, 0);
            y = DrawLine(y,e);

            mPOSPrinter.POSDrawString("Tiền thẻ: ", e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0}", Utilities.MoneyFormat.ConvertToString(mBOPrinterReportDaily.BAOCAONGAYTONG.TienThe.Value)), e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Right, 0);
            y = DrawLine(y, e);

            mPOSPrinter.POSDrawString("Tiền khách hàng: ", e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0}", Utilities.MoneyFormat.ConvertToString(mBOPrinterReportDaily.BAOCAONGAYTONG.TienKhacHang.Value)), e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Right, 0);
            y = DrawLine(y, e);

            mPOSPrinter.POSDrawString("Tiền trả lại: ", e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0}", Utilities.MoneyFormat.ConvertToString(mBOPrinterReportDaily.BAOCAONGAYTONG.TienTraLai.Value)), e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Right, 0);
            y = DrawLine(y, e);

            mPOSPrinter.POSDrawString("Tiền giảm giá: ", e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0}", Utilities.MoneyFormat.ConvertToString(mBOPrinterReportDaily.BAOCAONGAYTONG.GiamGia.Value)), e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Right, 0);
            y = DrawLine(y, e);

            mPOSPrinter.POSDrawString("Tiền chiết khấu: ", e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0}", Utilities.MoneyFormat.ConvertToString(mBOPrinterReportDaily.BAOCAONGAYTONG.ChietKhau.Value)), e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Right, 0);
            y = DrawLine(y, e);

            mPOSPrinter.POSDrawString("Tiền bo: ", e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0}", Utilities.MoneyFormat.ConvertToString(mBOPrinterReportDaily.BAOCAONGAYTONG.TienBo.Value)), e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Right, 0);
            y = DrawLine(y, e);

            mPOSPrinter.POSDrawString("Phí dịch vụ: ", e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0}", Utilities.MoneyFormat.ConvertToString(mBOPrinterReportDaily.BAOCAONGAYTONG.PhiDichVu.Value)), e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Right, 0);
            y = DrawLine(y, e);

            mPOSPrinter.POSDrawString("Số hóa đơn: ", e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0}", mBOPrinterReportDaily.BAOCAONGAYTONG.SoHoaDon.Value), e, mPrinterFont.FontItemBody, Color.Black, y, TextAlign.Right, 0);
            y = DrawLine(y, e);

            mPOSPrinter.POSDrawString("Tổng: ", e, mPrinterFont.FontSum, Color.Black, y, TextAlign.Left, 0);
            y = mPOSPrinter.POSDrawString(String.Format("{0}", Utilities.MoneyFormat.ConvertToString(mBOPrinterReportDaily.BAOCAONGAYTONG.TongTien.Value)), e, mPrinterFont.FontSum, Color.Black, y, TextAlign.Right, 0);
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
            mBOPrinterReportDaily = mBOXuliMayIn.GetBaoCaoNgayTong(mDateTimeFrom, mDateTimeTo);
        }
        public void Print()
        {
            LoadData();
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
