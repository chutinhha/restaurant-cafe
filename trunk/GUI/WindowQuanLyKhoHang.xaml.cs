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
    /// Interaction logic for WindowQuanLyKhoHang.xaml
    /// </summary>
    public partial class WindowQuanLyKhoHang : Window
    {
        private Data.Transit mTransit = null;
        public WindowQuanLyKhoHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnKho_Click(sender, e);
            uCTile.TenChucNang = "Quản Lý kho hàng";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }

        void uCTile_OnEventExit()
        {
            this.Close();
        }

        private UserControlLibrary.UCKho ucKho = null;
        private UserControlLibrary.UCNhaCungCap ucNhaCungCap = null;

        private void btnKho_Click(object sender, RoutedEventArgs e)
        {
            if (ucKho == null)
            {
                ucKho = new UserControlLibrary.UCKho(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucKho);
        }

        private void btnNhaCungCap_Click(object sender, RoutedEventArgs e)
        {
            if (ucNhaCungCap == null)
            {
                ucNhaCungCap = new UserControlLibrary.UCNhaCungCap(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucNhaCungCap);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (spNoiDung.Children[0] is UserControlLibrary.UCKho)
                ucKho.Window_KeyDown(sender, e);
            if (spNoiDung.Children[0] is UserControlLibrary.UCNhaCungCap)
                ucNhaCungCap.Window_KeyDown(sender, e);
        }
    }
}
