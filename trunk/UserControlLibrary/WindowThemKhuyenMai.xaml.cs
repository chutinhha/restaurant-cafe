using System.Windows;
using System;
using System.Collections.Generic;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemKhuyenMai.xaml
    /// </summary>
    public partial class WindowThemKhuyenMai : Window
    {
        public Data.BOMenuKichThuocMon _Item = null;
        private Data.BOMenuKhuyenMai BOMenuKhuyenMai = null;
        private List<Data.BOMenuKichThuocMon> lsArrayDeleted = null;
        Data.Transit mTransit = null;
        public WindowThemKhuyenMai(Data.BOMenuKichThuocMon item, Data.Transit transit, Data.BOMenuKhuyenMai bOMenuKhuyenMai)
        {
            InitializeComponent();
            mTransit = transit;
            BOMenuKhuyenMai = bOMenuKhuyenMai;
            _Item = item;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetValues();
        }

        private void SetValues()
        {
            if (_Item != null)
            {
                txtTenMonChinh.Text = _Item.TenMon;
                LoadDanhSach();
            }
            else
            {
                btnChonMonChinh_Click(null, null);
            }
        }

        private void btnChonMonChinh_Click(object sender, RoutedEventArgs e)
        {
            WindowChonMon win = new WindowChonMon(mTransit, false);
            if (win.ShowDialog() == true)
            {
                _Item = win._ItemKichThuocMon;
                SetValues();
            }
            else
            {
                btnHuy_Click(sender, e);
            }
        }

        private void LoadDanhSach()
        {
            BOMenuKhuyenMai.GetAll(_Item, mTransit);
            lvData.ItemsSource = _Item.DanhSachKhuyenMai;
        }

        private void btnThemMonPhu_Click(object sender, RoutedEventArgs e)
        {
            WindowChonMon win = new WindowChonMon(mTransit, false);
            if (win.ShowDialog() == true)
            {
                if (_Item == null)
                    _Item = new Data.BOMenuKichThuocMon();
                Data.BOMenuKhuyenMai km = new Data.BOMenuKhuyenMai();
                km.KichThuocMonTang = win._ItemKichThuocMon.MenuKichThuocMon;
                _Item.DanhSachKhuyenMai.Add(km);

            }
            else
            {
                btnHuy_Click(sender, e);
            }
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