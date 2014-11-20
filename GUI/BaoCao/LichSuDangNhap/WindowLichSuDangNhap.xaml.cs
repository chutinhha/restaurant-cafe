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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReportViewerData.Reset();
            ReportDataSource ds = new ReportDataSource("DataSetKaraoke", Data.BOLichSuDangNhap.GetNoTracking(mTransit));
            ReportViewerData.LocalReport.DataSources.Add(ds);
            ReportViewerData.LocalReport.ReportEmbeddedResource = "GUI.BaoCao.LichSuDangNhap.Report.rdlc";
            ReportViewerData.RefreshReport();
        }
    }
}
