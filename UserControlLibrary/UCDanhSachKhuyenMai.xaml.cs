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

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCDanhSachKhuyenMai.xaml
    /// </summary>
    public partial class UCDanhSachKhuyenMai : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.BOMenuKhuyenMai BOMenuKhuyenMai = null;

        public UCDanhSachKhuyenMai(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOMenuKhuyenMai = new Data.BOMenuKhuyenMai(mTransit);
        }

        private void LoadDanhSach()
        {
            lvData.ItemsSource = BOMenuKhuyenMai.GetDanhSachKichThuocMon(mTransit);
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemKhuyenMai win = new UserControlLibrary.WindowThemKhuyenMai(null, mTransit, BOMenuKhuyenMai);
            if (win.ShowDialog() == true)
            {
                LoadDanhSach();
            }

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnLuu_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnThem_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnDanhSach_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.F2)
            {
                btnSua_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                btnXoa_Click(null, null);
                return;
            }
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }
    }
}
