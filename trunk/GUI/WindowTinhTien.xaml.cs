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

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowTinhTien.xaml
    /// </summary>
    public partial class WindowTinhTien : Window
    {
        private Data.Transit mTransit;
        private Data.BOBanHang mBOBanHang;
        private Data.BOBanHang mBOBanHangTam;
        public WindowTinhTien(Data.Transit transit,Data.BOBanHang banhang)
        {
            InitializeComponent();
            mTransit = transit;
            mBOBanHang = banhang;
            mBOBanHangTam = new Data.BOBanHang(mTransit);
        }
        private void ReLoadData()
        {
            txtTongTien.Text ="Tổng Tiền: "+ Utilities.MoneyFormat.ConvertToStringFull(mBOBanHang.TongTien());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSoTien._UCMoneyKeyPad = uCMoneyKeyPad1;
            txtSoTien._UCKeyPad = uCKeyPad1;
            ReLoadData();
        }
    }
}
