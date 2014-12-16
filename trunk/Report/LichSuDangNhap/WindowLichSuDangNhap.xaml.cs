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

namespace Report.LichSuDangNhap
{
    /// <summary>
    /// Interaction logic for WindowLichSuDangNhap.xaml
    /// </summary>
    public partial class WindowLichSuDangNhap : Window
    {
        private bool _isReportViewerLoaded;
        private Data.Transit mTransit = null;
        private Data.BOBaoCaoLichSuDangNhap BOBaoCaoLichSuDangNhap = null;

        public WindowLichSuDangNhap(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOBaoCaoLichSuDangNhap = new Data.BOBaoCaoLichSuDangNhap();
            uCTileReport.Landscape = false;
            uCTileReport.SetInit(mTransit, _reportViewer, "BaoCaoDinhLuong", true, false);
            uCTileReport._OnDong += new UCTileReport.OnDong(uCTileReport__OnDong);
            uCTileReport._OnReload += new UCTileReport.OnReload(uCTileReport__OnReload);
            _reportViewer.Load += ReportViewer_Load;
        }

        private void Reload()
        {
            this._reportViewer.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource rdsCaiDatThongTinCongTy = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsCaiDatThongTinCongTy.Name = "CAIDATTHONGTINCONGTY";
            rdsCaiDatThongTinCongTy.Value = BOBaoCaoLichSuDangNhap.GetCaiDatThongTinCongTy();
            this._reportViewer.LocalReport.DataSources.Add(rdsCaiDatThongTinCongTy);

            Microsoft.Reporting.WinForms.ReportDataSource rdsBaoCaoLichSuDangNhap = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsBaoCaoLichSuDangNhap.Name = "BAOCAOLICHDANGNHAP";
            rdsBaoCaoLichSuDangNhap.Value = BOBaoCaoLichSuDangNhap.GetBaoCaoLichSuDangNhap(uCTileReport.GetDateFrom);
            this._reportViewer.LocalReport.DataSources.Add(rdsBaoCaoLichSuDangNhap);


            this._reportViewer.LocalReport.ReportEmbeddedResource = "Report.LichSuDangNhap.Report.rdlc";

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
