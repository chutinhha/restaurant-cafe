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
    /// Interaction logic for WindowThongTinPhanMem.xaml
    /// </summary>
    public partial class WindowThongTinPhanMem : Window
    {
        Data.Transit mTransit;
        public WindowThongTinPhanMem(Data.Transit transit)
        {
            mTransit = transit;
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnOK_Click(null, null);
                return;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBanQuyen();
        }

        private void btnBanQuyen_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowBanQuyen win = new UserControlLibrary.WindowBanQuyen(mTransit);
            if (win.ShowDialog()==true)
            {
                LoadBanQuyen();

            }
        }
        private void LoadBanQuyen()
        {
            if (Utilities.SecurityKaraoke.CheckLisence(mTransit.ThamSo.BanQuyen, mTransit.HashMD5))
            {
                if (mTransit.ThamSo.BanQuyen.Substring(0, 1) == "3")
                {
                    btnBanQuyen.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    lblDay.Content = Utilities.DateTimeConverter.ConvertToDateStringDMY(mTransit.ThamSo.NgayKetThuc.Value);
                }
            }
        }
    }
}
