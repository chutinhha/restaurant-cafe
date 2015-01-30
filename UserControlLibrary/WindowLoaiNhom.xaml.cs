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
    /// Interaction logic for WindowLoaiNhom.xaml
    /// </summary>
    public partial class WindowLoaiNhom : Window
    {
        private Data.Transit mTransit;
        public WindowLoaiNhom(Data.Transit transit)
        {            
            InitializeComponent();
            mTransit = transit;
            uCTile.TenChucNang = "Cài đặt thông tin";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
        }
        void uCTile_OnEventExit()
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uCLoaiNhom1.Init(mTransit);
            uCLoaiNhom1.UserControl_Loaded();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            uCLoaiNhom1.Window_KeyDown(sender, e);
        }
    }
}
