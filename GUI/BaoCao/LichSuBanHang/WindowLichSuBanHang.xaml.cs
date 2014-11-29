using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using System.Globalization;

namespace GUI.BaoCao.LichSuBanHang
{
    /// <summary>
    /// Interaction logic for WindowLichSuBanHang.xaml
    /// </summary>
    public partial class WindowLichSuBanHang : Window
    {
        private Data.Transit mTransit = null;

        public WindowLichSuBanHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.TenChucNang = "Lịch sử bán hàng";
            uCTile.SetTransit(mTransit);
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }

        private void uCTile_OnEventExit()
        {
            DialogResult = false;
        }

        private IList<Stream> m_streams;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        public void Load()
        {
            ReportViewerData.Reset();
            ReportDataSource ds = new ReportDataSource("BaoCaoLichSuBanHang", Data.BOBaoCaoLichSuBanHang.GetNoTracking(mTransit));
            ReportViewerData.LocalReport.DataSources.Add(ds);
            List<Data.CAIDATTHONGTINCONGTY> lsCaiDatThongTinCongTy = new List<Data.CAIDATTHONGTINCONGTY>();
            lsCaiDatThongTinCongTy.Add(Data.BOCaiDatThongTinCongTy.GetQueryNoTracking(mTransit));
            ReportDataSource ds1 = new ReportDataSource("CaiDatThongTinCongTy", lsCaiDatThongTinCongTy);
            ReportViewerData.LocalReport.DataSources.Add(ds1);
            ReportViewerData.LocalReport.ReportEmbeddedResource = "GUI.BaoCao.LichSuBanHang.Report.rdlc";

            //ReportViewerData.PrinterSettings.PrinterName = mOrders.ReadConfig.PrinterTaxinvoiceA4;
            ReportViewerData.PrinterSettings.DefaultPageSettings.Margins.Left = 50;
            ReportViewerData.PrinterSettings.DefaultPageSettings.Margins.Right = 50;
            ReportViewerData.PrinterSettings.DefaultPageSettings.Margins.Top = 50;
            ReportViewerData.PrinterSettings.DefaultPageSettings.Margins.Bottom = 50;
            ReportViewerData.PrinterSettings.DefaultPageSettings.Landscape = true;
            ReportViewerData.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = (int)PaperKind.A4;

            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            pg.Margins.Top = 50;
            pg.Margins.Bottom = 50;
            pg.Margins.Left = 50;
            pg.Margins.Right = 50;
            pg.PaperSize.RawKind = (int)PaperKind.A4;
            pg.Landscape = true;
            ReportViewerData.SetPageSettings(pg);
            ReportViewerData.LocalReport.DisplayName = "Lich_Su_Ban_Hang_" + DateTime.Now.ToString("dd-MM-yyyy HHmmss");
            ReportViewerData.RenderingComplete += new RenderingCompleteEventHandler(ReportViewerData_RenderingComplete);
            ReportViewerData.RefreshReport();
        }

        private void ReportViewerData_RenderingComplete(object sender, RenderingCompleteEventArgs e)
        {
            Export(ReportViewerData.LocalReport);
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.27in</PageWidth>
                <PageHeight>11.69in</PageHeight>
                <MarginTop>0.5in</MarginTop>
                <MarginLeft>0.5in</MarginLeft>
                <MarginRight>0.5in</MarginRight>
                <MarginBottom>0.5in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
    }
}
