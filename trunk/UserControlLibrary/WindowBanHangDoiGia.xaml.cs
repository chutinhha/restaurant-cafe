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
    /// Interaction logic for WindowBanHangDoiGia.xaml
    /// </summary>
    public partial class WindowBanHangDoiGia : Window
    {
        public Data.BOChiTietBanHang _BOChiTietBanHang { get; set; }
        private Data.Transit mTransit;
        public WindowBanHangDoiGia(Data.Transit transit,Data.BOChiTietBanHang chitiet)
        {
            mTransit = transit;
            _BOChiTietBanHang = chitiet;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtGia._UCKeyPad = uCKeyPad1;
            txtGia._TypeTextBox = ControlLibrary.TypeKeyPad.Decimal;
            LoadData();
        }
        private void LoadData()
        {
            if (_BOChiTietBanHang!=null)
            {
                lbTieuDe.Text = _BOChiTietBanHang.TenMon;                
            }
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if (txtGia.Text=="")
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng nhập giá tiền");
            }
            else
            {
                if (_BOChiTietBanHang!=null)
                {
                    _BOChiTietBanHang.ChangePriceChiTietBanHang(Utilities.MoneyFormat.ConvertToDecimal(txtGia.Text));
                }
                this.DialogResult = true;
            }
        }
    }
}
