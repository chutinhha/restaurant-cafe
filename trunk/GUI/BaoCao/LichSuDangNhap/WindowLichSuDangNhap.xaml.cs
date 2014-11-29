using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Reporting.WinForms;

namespace GUI.BaoCao.LichSuDangNhap
{
    /// <summary>
    /// Interaction logic for WindowLichSuDangNhap.xaml
    /// </summary>
    public partial class WindowLichSuDangNhap : Window
    {
        private Data.Transit mTransit = null;

        public WindowLichSuDangNhap(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.TenChucNang = "Lịch sử đăng nhập";
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
            ReportViewerData.Reset();
            ReportDataSource ds = new ReportDataSource("DataSetKaraoke", Data.BOLichSuDangNhap.GetNoTracking(mTransit));
            ReportViewerData.LocalReport.DataSources.Add(ds);
            ReportDataSource ds1 = new ReportDataSource("CaiDatThongTinCongTy", Data.BOCaiDatThongTinCongTy.GetQueryNoTracking(mTransit));
            ReportViewerData.LocalReport.DataSources.Add(ds1);
            ReportViewerData.LocalReport.ReportEmbeddedResource = "GUI.BaoCao.LichSuDangNhap.Report.rdlc";

            //ReportViewerData.PrinterSettings.PrinterName = mOrders.ReadConfig.PrinterTaxinvoiceA4;
            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            pg.Margins.Top = 50;
            pg.Margins.Bottom = 50;
            pg.Margins.Left = 50;
            pg.Margins.Right = 50;
            pg.PaperSize.RawKind = (int)PaperKind.A4;
            ReportViewerData.SetPageSettings(pg);
            ReportViewerData.LocalReport.DisplayName = "LichSuDangNhap";
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