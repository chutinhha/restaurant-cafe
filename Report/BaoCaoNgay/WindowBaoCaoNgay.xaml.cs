using System;
using System.Windows;

namespace Report.BaoCaoNgay
{
    /// <summary>
    /// Interaction logic for WindowBaoCaoNgay.xaml
    /// </summary>
    public partial class WindowBaoCaoNgay : Window
    {
        private Data.Transit mTransit = null;

        public WindowBaoCaoNgay(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTileReport.SetInit(mTransit, _reportViewer, "BaoCaoNgay", true);
            uCTileReport._OnDong += new UCTileReport.OnDong(uCTileReport__OnDong);
            _reportViewer.Load += ReportViewer_Load;
            uCTileReport.ReloadPage();
        }        

        private void uCTileReport__OnDong()
        {
            this.Close();
        }

        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource1.Name = "BAOCAOLICHSUBANHANG";
                reportDataSource1.Value = Data.FrameworkRepository<Data.BAOCAOLICHSUBANHANG>.QueryNoTracking(mTransit.KaraokeEntities.BAOCAOLICHSUBANHANGs);
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this._reportViewer.LocalReport.ReportEmbeddedResource = "Report.BaoCaoNgay.Report.rdlc";
                _reportViewer.RefreshReport();                                
                _isReportViewerLoaded = true;
            }            
        }
    }
}