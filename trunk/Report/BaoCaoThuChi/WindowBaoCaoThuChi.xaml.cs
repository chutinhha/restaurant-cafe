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

namespace Report.BaoCaoThuChi
{
    /// <summary>
    /// Interaction logic for WindowBaoCaoThuChi.xaml
    /// </summary>
    public partial class WindowBaoCaoThuChi : Window
    {
        private bool _isReportViewerLoaded;
        private Data.Transit mTransit = null;
        private Data.BOBaoCaoThuChi BOBaoCaoThuChi = null;

        public WindowBaoCaoThuChi(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOBaoCaoThuChi = new Data.BOBaoCaoThuChi(mTransit);
            uCTileReport.Landscape = false;
            uCTileReport.SetInit(mTransit, _reportViewer, "BaoCaoThuChi", true, true);
            uCTileReport._OnDong += new UCTileReport.OnDong(uCTileReport__OnDong);
            uCTileReport._OnReload += new UCTileReport.OnReload(uCTileReport__OnReload);
            _reportViewer.Load += ReportViewer_Load;
        }

        private void Reload()
        {
            this._reportViewer.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource rdsCaiDatThongTinCongTy = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsCaiDatThongTinCongTy.Name = "CAIDATTHONGTINCONGTY";
            rdsCaiDatThongTinCongTy.Value = BOBaoCaoThuChi.GetCaiDatThongTinCongTy();
            this._reportViewer.LocalReport.DataSources.Add(rdsCaiDatThongTinCongTy);

            Microsoft.Reporting.WinForms.ReportDataSource rdsBaoCaoThuChi = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsBaoCaoThuChi.Name = "BAOCAOTHUCHI";
            rdsBaoCaoThuChi.Value = BOBaoCaoThuChi.GetBOBaoCaoThuChi(uCTileReport.GetDateFrom, uCTileReport.GetDateTo);
            this._reportViewer.LocalReport.DataSources.Add(rdsBaoCaoThuChi);

            Microsoft.Reporting.WinForms.ReportDataSource rdsDateTimeReport = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsDateTimeReport.Name = "DATETIMEREPORT";
            List<Data.DateTimeReport> lsDate = new List<Data.DateTimeReport>();
            lsDate.Add(new Data.DateTimeReport() { DateFrom = uCTileReport.GetDateFrom, DateTo = uCTileReport.GetDateTo });
            rdsDateTimeReport.Value = lsDate;
            this._reportViewer.LocalReport.DataSources.Add(rdsDateTimeReport);



            this._reportViewer.LocalReport.ReportEmbeddedResource = "Report.BaoCaoThuChi.Report.rdlc";

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
