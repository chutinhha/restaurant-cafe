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
        private Data.BOBaoCaoNgay BOBaoCaoNgay = null;
        private PrinterServer.ProcessPrinter mProcessPrinter;
        public WindowBaoCaoNgay(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOBaoCaoNgay = new Data.BOBaoCaoNgay(mTransit);
            mProcessPrinter = new PrinterServer.ProcessPrinter(mTransit);
            uCTileReport.Landscape = false;
            uCTileReport.SetInit(mTransit, _reportViewer, "BaoCaoNgay", true, true);
            uCTileReport._OnDong += new UCTileReport.OnDong(uCTileReport__OnDong);
            uCTileReport._OnReload += new UCTileReport.OnReload(uCTileReport__OnReload);
            uCTileReport._OnPrint += new UCTileReport.OnPrint(uCTileReport__OnPrint);
            _reportViewer.Load += ReportViewer_Load;
        }

        void uCTileReport__OnPrint(DateTime dtFrom, DateTime dtTo)
        {
            //DateTime now = DateTime.Now;
            //if (dtFrom==null)
            //{
            //    dtFrom = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            //}
            //if (dtTo==null)
            //{
            //    dtTo = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            //}
            mProcessPrinter.InReport(dtFrom, dtTo);
        }
       

        private void Reload()
        {
            this._reportViewer.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource rdsCaiDatThongTinCongTy = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsCaiDatThongTinCongTy.Name = "CAIDATTHONGTINCONGTY";
            rdsCaiDatThongTinCongTy.Value = BOBaoCaoNgay.GetCaiDatThongTinCongTy();
            this._reportViewer.LocalReport.DataSources.Add(rdsCaiDatThongTinCongTy);

            Microsoft.Reporting.WinForms.ReportDataSource rdsBaoCaoNgayTong = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsBaoCaoNgayTong.Name = "BAOCAONGAYTONG";
            rdsBaoCaoNgayTong.Value = BOBaoCaoNgay.GetBaoCaoNgayTong(uCTileReport.GetDateFrom, uCTileReport.GetDateTo);
            this._reportViewer.LocalReport.DataSources.Add(rdsBaoCaoNgayTong);

            Microsoft.Reporting.WinForms.ReportDataSource rdsBaoCaoNgayNhom = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsBaoCaoNgayNhom.Name = "BAOCAONGAYNHOM";
            rdsBaoCaoNgayNhom.Value = BOBaoCaoNgay.GetBaoCaoNgayNhom(uCTileReport.GetDateFrom, uCTileReport.GetDateTo);
            this._reportViewer.LocalReport.DataSources.Add(rdsBaoCaoNgayNhom);

            Microsoft.Reporting.WinForms.ReportDataSource rdsBaoCaoNgayMon = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsBaoCaoNgayMon.Name = "BAOCAONGAYMON";
            rdsBaoCaoNgayMon.Value = BOBaoCaoNgay.GetBaoCaoNgayMon(uCTileReport.GetDateFrom, uCTileReport.GetDateTo);
            this._reportViewer.LocalReport.DataSources.Add(rdsBaoCaoNgayMon);

            Microsoft.Reporting.WinForms.ReportDataSource rdsBaoCaoNgayThe = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsBaoCaoNgayThe.Name = "BAOCAONGAYTHE";
            rdsBaoCaoNgayThe.Value = BOBaoCaoNgay.GetBaoCaoThe(uCTileReport.GetDateFrom, uCTileReport.GetDateTo);
            this._reportViewer.LocalReport.DataSources.Add(rdsBaoCaoNgayThe);

            Microsoft.Reporting.WinForms.ReportDataSource rdsBaoCaoNgayKhachHang = new Microsoft.Reporting.WinForms.ReportDataSource();
            rdsBaoCaoNgayKhachHang.Name = "BAOCAONGAYKHACHHANG";
            rdsBaoCaoNgayKhachHang.Value = BOBaoCaoNgay.GetBaoCaoNgayKhachHang(uCTileReport.GetDateFrom, uCTileReport.GetDateTo);
            this._reportViewer.LocalReport.DataSources.Add(rdsBaoCaoNgayKhachHang);

            this._reportViewer.LocalReport.ReportEmbeddedResource = "Report.BaoCaoNgay.Report.rdlc";

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