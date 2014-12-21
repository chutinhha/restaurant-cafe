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

namespace Report.BaoCaoTonKho
{
    /// <summary>
    /// Interaction logic for WindowBaoCaoTonKho.xaml
    /// </summary>
    public partial class WindowBaoCaoTonKho : Window
    {
        private bool _isReportViewerLoaded;
        private Data.Transit mTransit = null;
        private Data.BOBaoCaoTonKho BOBaoCaoTonKho = null;

        public WindowBaoCaoTonKho(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOBaoCaoTonKho = new Data.BOBaoCaoTonKho();
            uCTileReport.Landscape = false;
            uCTileReport.SetInit(mTransit, _reportViewer, "BaoCaoTonKho", true, false);
            uCTileReport._OnDong += new UCTileReport.OnDong(uCTileReport__OnDong);
            uCTileReport._OnReload += new UCTileReport.OnReload(uCTileReport__OnReload);
            _reportViewer.Load += ReportViewer_Load;
        }

        private void Reload()
        {
            this._reportViewer.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource rdsCaiDatThongTinCongTy = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsCaiDatThongTinCongTy.Name = "CAIDATTHONGTINCONGTY";
            rdsCaiDatThongTinCongTy.Value = BOBaoCaoTonKho.GetCaiDatThongTinCongTy();
            this._reportViewer.LocalReport.DataSources.Add(rdsCaiDatThongTinCongTy);

            Microsoft.Reporting.WinForms.ReportDataSource rdsBaoCaoTonKho = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsBaoCaoTonKho.Name = "BAOCAOTONKHO";
            rdsBaoCaoTonKho.Value = BOBaoCaoTonKho.GetBaoCaoTonKho(uCTileReport.GetDateFrom);
            this._reportViewer.LocalReport.DataSources.Add(rdsBaoCaoTonKho);


            this._reportViewer.LocalReport.ReportEmbeddedResource = "Report.BaoCaoTonKho.Report.rdlc";

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
