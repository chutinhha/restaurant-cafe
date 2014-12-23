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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace License
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string HashMD5 = "KTr";
        public MainWindow()
        {            
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime(2015, 01, 20);
            if (DateTime.Now.CompareTo(dt) >= 0)
            {
                Application.Current.Shutdown();
            }  
            cboLoaiBanQuyen.ItemsSource = LicenseType.GetListSecurityType();
            if (cboLoaiBanQuyen.Items.Count>0)
            {
                cboLoaiBanQuyen.SelectedIndex = 0;
            }
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if (cboLoaiBanQuyen.SelectedItem!=null)
            {
                if (txtMatKhau.Password=="Vu@998877")
                {
                    if (Utilities.SecurityKaraoke.CheckProductID(txtMaSanPham.Text,HashMD5))
                    {
                        LicenseType license = cboLoaiBanQuyen.SelectedItem as LicenseType;
                        txtBanQuyen.Text = Utilities.SecurityKaraoke.GetKey(license.ID, txtMaSanPham.Text, HashMD5);                        
                    }
                    else
                    {
                        MessageBox.Show("Mã sản phẩm không đúng không đúng");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu không đúng");
                }
            }
            else
            {
                MessageBox.Show("Chọn loại bản quyền");
            }
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
