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
    /// Interaction logic for WindowQuanLyPhanQuyen.xaml
    /// </summary>
    public partial class WindowQuanLyPhanQuyen : Window
    {
        private Data.Transit mTransit = null;
        public WindowQuanLyPhanQuyen(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnPhanQuyen_Click(sender, e);
        }

        private UserControlLibrary.UCPhanQuyen ucPhanQuyen = null;
        private UserControlLibrary.UCNhomQuyen ucNhomQuyen = null;
        private UserControlLibrary.UCNhomQuyenNhanVien ucNhomQuyenNhanVien = null;

        private void btnPhanQuyen_Click(object sender, RoutedEventArgs e)
        {
            if (ucPhanQuyen == null)
            {
                ucPhanQuyen = new UserControlLibrary.UCPhanQuyen(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucPhanQuyen);
        }

        private void btnNhomQuyen_Click(object sender, RoutedEventArgs e)
        {
            if (ucNhomQuyen == null)
            {
                ucNhomQuyen = new UserControlLibrary.UCNhomQuyen(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucNhomQuyen);
        }

        private void btnNhomQuyenNhanVien_Click(object sender, RoutedEventArgs e)
        {
            if (ucNhomQuyenNhanVien == null)
            {
                ucNhomQuyenNhanVien = new UserControlLibrary.UCNhomQuyenNhanVien(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucNhomQuyenNhanVien);
        }
    }
}
