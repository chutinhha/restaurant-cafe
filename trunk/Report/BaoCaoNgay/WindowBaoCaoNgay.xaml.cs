using System;
using System.Windows;
using System.Linq;

namespace Report.BaoCaoNgay
{
    /// <summary>
    /// Interaction logic for WindowBaoCaoNgay.xaml
    /// </summary>
    public partial class WindowBaoCaoNgay : Window
    {
        private bool _isReportViewerLoaded;
        private Data.Transit mTransit = null;

        public WindowBaoCaoNgay(Data.Transit transit)
        {
            try
            {                
                 InitializeComponent();                 
                mTransit = transit;
                uCTileReport.Landscape = true;                
                uCTileReport.SetInit(mTransit, _reportViewer, "BaoCaoNgay", true);                
                uCTileReport._OnDong += new UCTileReport.OnDong(uCTileReport__OnDong);                
                uCTileReport._OnReload += new UCTileReport.OnReload(uCTileReport__OnReload);                
                _reportViewer.Load += ReportViewer_Load;
            }
            catch (Exception ex)
            {
                MessageBox.Show("WindowBaoCaoNgay::" + ex.Message);
            }
        }

        private void Reload()
        {
            try
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource1.Name = "BAOCAOLICHSUBANHANG";
                reportDataSource1.Value = Data.BOBaoCaoLichSuBanHang.GetNoTracking(mTransit, uCTileReport.GetDate);
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this._reportViewer.LocalReport.ReportEmbeddedResource = "Report.BaoCaoNgay.Report.rdlc";
                _reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reload::"+ex.Message);
            }
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Reload();
                _isReportViewerLoaded = true;
            }
        }

        private void uCTileReport__OnDong()
        {
            this.Close();
        }

        private void uCTileReport__OnReload()
        {
            Reload();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                uCTileReport.ReloadPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}