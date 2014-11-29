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
    /// Interaction logic for WindowLoginDialog.xaml
    /// </summary>
    public partial class WindowLoginDialog : Window
    {
        Data.Transit mTransit = null;
        public WindowLoginDialog(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        bool ThayDoiNhanVien = false;

        public WindowLoginDialog(Data.Transit transit, bool thayDoiNhanVien)
        {
            InitializeComponent();
            mTransit = transit;
            ThayDoiNhanVien = thayDoiNhanVien;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mTransit.NhanVien != null && !ThayDoiNhanVien)
            {
                txtUserID.Text = mTransit.NhanVien.TenDangNhap;
                txtUserID.IsEnabled = false;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            lbStatus.Text = "";
            Data.NHANVIEN NV = Data.BONhanVien.Login(txtUserID.Text.Trim(), Utilities.SecurityKaraoke.GetMd5Hash(txtPassword.Text.Trim(), mTransit.HashMD5), mTransit);
            if (NV != null)
            {
                if (ThayDoiNhanVien)
                    mTransit.NhanVien = NV;
                //else
                //    mTransit.NhanVien = NV;
                DialogResult = true;
            }
            else
                lbStatus.Text = mTransit.StringButton.DangNhapKhongDung;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnEnter_Click(null, null);
                return;
            }

            if (e.Key == System.Windows.Input.Key.Escape)
            {
                btnExit_Click(null, null);
                return;
            }
        }
    }
}
