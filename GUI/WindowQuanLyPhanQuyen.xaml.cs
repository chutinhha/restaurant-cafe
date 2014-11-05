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
            uCTile.TenChucNang = "Quản Lý Phân quyên";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }

        void uCTile_OnEventExit()
        {
            this.Close();
        }

        private UserControlLibrary.UCPhanQuyen ucPhanQuyen = null;
        private UserControlLibrary.UCQuyen ucQuyen = null;

        private void btnPhanQuyen_Click(object sender, RoutedEventArgs e)
        {
            if (ucPhanQuyen == null)
            {
                ucPhanQuyen = new UserControlLibrary.UCPhanQuyen(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucPhanQuyen);
        }

        private void btnQuyen_Click(object sender, RoutedEventArgs e)
        {
            if (ucQuyen == null)
            {
                ucQuyen = new UserControlLibrary.UCQuyen(mTransit);
            }
            spNoiDung.Children.Clear();
            spNoiDung.Children.Add(ucQuyen);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if (spNoiDung.Children[0] is UserControlLibrary.UCPhanQuyen)
            //    ucPhanQuyen.Window_KeyDown(sender, e);
            if (spNoiDung.Children[0] is UserControlLibrary.UCQuyen)
                ucQuyen.Window_KeyDown(sender, e);
        }
    }
}
