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
    /// Interaction logic for WindowQuanLyThe.xaml
    /// </summary>
    public partial class WindowQuanLyThe : Window
    {
        private Data.Transit mTransit = null;

        public WindowQuanLyThe(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
            uCTile.TenChucNang = "Quản lý thẻ";
        }

        private void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uCThe.Init(mTransit);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            uCThe.Window_KeyDown(sender, e);
        }
    }
}
