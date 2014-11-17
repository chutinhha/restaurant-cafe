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
    /// Interaction logic for WindowQuanLyDinhLuong.xaml
    /// </summary>
    public partial class WindowQuanLyDinhLuong : Window
    {
        private Data.Transit mTransit = null;
        public WindowQuanLyDinhLuong(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;            
            uCTile.TenChucNang = "Quản lý định lượng";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
            ucDinhLuong.Init(mTransit);
        }

        void uCTile_OnEventExit()
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ucDinhLuong.Window_KeyDown(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
