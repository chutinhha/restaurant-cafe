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
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Printing;

namespace GUI.BaoCao.LichSuDangNhap
{
    /// <summary>
    /// Interaction logic for WindowLichSuDangNhap.xaml
    /// </summary>
    public partial class WindowLichSuDangNhap : Window
    {
        Data.Transit mTransit = null;
        public WindowLichSuDangNhap(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.TenChucNang = "Lịch sử đăng nhập";
            uCTile.SetTransit(mTransit);
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }

        void uCTile_OnEventExit()
        {
            DialogResult = false;
        }
        private IList<Stream> m_streams;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReportViewerData.Reset();
            ReportDataSource ds = new ReportDataSource("DataSetKaraoke", Data.BOLichSuDangNhap.GetNoTracking(mTransit));
            ReportViewerData.LocalReport.DataSources.Add(ds);
            ReportDataSource ds1 = new ReportDataSource("CaiDatThongTinCongTy", Data.BOCaiDatThongTinCongTy.GetNoTracking(mTransit));
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
            ReportViewerData.LocalReport.DisplayName="LichSuDangNhap";
            ReportViewerData.RenderingComplete += new RenderingCompleteEventHandler(ReportViewerData_RenderingComplete);
            ReportViewerData.RefreshReport();
        }

        void ReportViewerData_RenderingComplete(object sender, RenderingCompleteEventArgs e)
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
