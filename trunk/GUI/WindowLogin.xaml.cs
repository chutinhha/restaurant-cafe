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
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private Data.Transit mTransit = null;
        Data.BONhanVien BONhanVien = null;
        public WindowLogin()
        {
            InitializeComponent();
            mTransit = new Data.Transit();
            BONhanVien = new Data.BONhanVien(mTransit);
            ucTile.SetTransit(mTransit);           
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {           
            mTransit.NhanVien = Data.BONhanVien.Login(txtUserID.Text.Trim(), Utilities.SecurityKaraoke.GetMd5Hash(txtPassword.Text.Trim(), mTransit.HashMD5), mTransit);
            if (mTransit.NhanVien == null)
            {
                if (mTransit.Admin.TenDangNhap == txtUserID.Text.Trim() && mTransit.Admin.MatKhau == Utilities.SecurityKaraoke.GetMd5Hash(txtPassword.Text.Trim(), mTransit.HashMD5))
                {
                    mTransit.NhanVien = new Data.NHANVIEN();
                    mTransit.NhanVien.LoaiNhanVienID = mTransit.Admin.LoaiNhanVienID;
                    mTransit.NhanVien.TenNhanVien = mTransit.Admin.TenNhanVien;
                    mTransit.NhanVien.NhanVienID = 0;
                }
                else
                {
                    lbStatus.Text = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            else
            {
                BONhanVien.ThemLichSuDangNhap(mTransit.NhanVien.NhanVienID);
                mTransit.LayDanhSachQuyen();
            }
            if (mTransit.NhanVien != null)
            {
                MainWindow win = new MainWindow(mTransit);
                this.Hide();
                win.ShowDialog();
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtPassword.Text = "";
            txtUserID.Text = "";
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process[] lsProcess = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in lsProcess)
            {
                if (process.ProcessName == "GUI")
                    process.Kill();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserID._UCKeyPad = uCKeyPad;
            txtPassword._UCKeyPad = uCKeyPad;            
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
