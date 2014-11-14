using System.Windows;
using System;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemKhuyenMai.xaml
    /// </summary>
    public partial class WindowThemKhuyenMai : Window
    {
        public Data.MENUKHUYENMAI _Item = null;
        Data.Transit mTransit = null;
        public WindowThemKhuyenMai(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetValues();
        }

        private void SetValues()
        {
            if (_Item != null)
            {
                LoadDanhSach();
                txtTenMonChinh.Text = _Item.MENUKICHTHUOCMON.TenLoaiBan;
            }
        }

        private void btnChonMonChinh_Click(object sender, RoutedEventArgs e)
        {
            WindowChonMon win = new WindowChonMon(mTransit, false);
            if (win.ShowDialog() == true)
            {
                _Item.MENUKICHTHUOCMON = win._ItemKichThuocMon.MenuKichThuocMon;
                SetValues();
            }
            else
            {
                btnHuy_Click(sender, e);
            }
        }

        private void LoadDanhSach()
        {
            lvData.ItemsSource = Data.BOMenuKhuyenMai.GetAll((int)_Item.KichThuocMonID, mTransit);
        }

        private void btnThemMonPhu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}