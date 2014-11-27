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

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowBaoCaoLichSuBanHang.xaml
    /// </summary>
    public partial class WindowBaoCaoLichSuBanHang : Window
    {
        private Data.Transit mTransit = null;


        public WindowBaoCaoLichSuBanHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.SetTransit(mTransit);
            uCTile.TenChucNang = "Báo cáo lịch sử bán hàng";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbData.ItemsSource = Data.BOBaoCaoLichSuBanHang.GetNoTracking(mTransit);
        }
    }
}
