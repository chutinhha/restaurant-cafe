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

namespace Report.BaoCaoDinhLuong
{
    /// <summary>
    /// Interaction logic for WindowBaoCaoDinhLuong.xaml
    /// </summary>
    public partial class WindowBaoCaoDinhLuong : Window
    {
        private bool _isReportViewerLoaded;
        private Data.Transit mTransit = null;

        public WindowBaoCaoDinhLuong(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTileReport.Landscape = true;
            uCTileReport.SetInit(mTransit, _reportViewer, "BaoCaoDinhLuong", false);
            uCTileReport._OnDong += new UCTileReport.OnDong(uCTileReport__OnDong);
            uCTileReport._OnReload += new UCTileReport.OnReload(uCTileReport__OnReload);
            _reportViewer.Load += ReportViewer_Load;
        }

        private void Reload()
        {
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            reportDataSource1.Name = "BAOCAODINHLUONG";
            reportDataSource1.Value = Data.BOBaoCaoDinhLuong.GetQueryNoTracking(mTransit);
            this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this._reportViewer.LocalReport.ReportEmbeddedResource = "Report.BaoCaoDinhLuong.Report.rdlc";
            _reportViewer.RefreshReport();
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
            uCTileReport.ReloadPage();
        }
    }
}
