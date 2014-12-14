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

namespace Report.BaoCaoNhanVien
{
    /// <summary>
    /// Interaction logic for WindowBaoCaoNhanVien.xaml
    /// </summary>
    public partial class WindowBaoCaoNhanVien : Window
    {
        private bool _isReportViewerLoaded;
        private Data.Transit mTransit = null;
        private Data.BOBaoCaoNhanVien BOBaoCaoNhanVien = null;

        public WindowBaoCaoNhanVien(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOBaoCaoNhanVien = new Data.BOBaoCaoNhanVien(mTransit);
            uCTileReport.Landscape = false;
            uCTileReport.SetInit(mTransit, _reportViewer, "BaoCaoNhanVien", true, true);
            uCTileReport._OnDong += new UCTileReport.OnDong(uCTileReport__OnDong);
            uCTileReport._OnReload += new UCTileReport.OnReload(uCTileReport__OnReload);
            _reportViewer.Load += ReportViewer_Load;
        }

        private void Reload()
        {
            this._reportViewer.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource rdsCaiDatThongTinCongTy = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsCaiDatThongTinCongTy.Name = "CAIDATTHONGTINCONGTY";
            rdsCaiDatThongTinCongTy.Value = BOBaoCaoNhanVien.GetCaiDatThongTinCongTy();
            this._reportViewer.LocalReport.DataSources.Add(rdsCaiDatThongTinCongTy);

            Microsoft.Reporting.WinForms.ReportDataSource rdsBaoCaoNhanVien = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsBaoCaoNhanVien.Name = "BAOCAONHANVIEN";
            rdsBaoCaoNhanVien.Value = BOBaoCaoNhanVien.GetBaoCaoNhanVien(uCTileReport.GetDateFrom, uCTileReport.GetDateTo);
            this._reportViewer.LocalReport.DataSources.Add(rdsBaoCaoNhanVien);


            this._reportViewer.LocalReport.ReportEmbeddedResource = "Report.BaoCaoNhanVien.Report.rdlc";

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
