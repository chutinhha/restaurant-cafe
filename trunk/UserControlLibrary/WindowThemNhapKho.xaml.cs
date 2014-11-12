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
    /// Interaction logic for WindowThemNhapKho.xaml
    /// </summary>
    public partial class WindowThemNhapKho : Window
    {
        private Data.Transit mTransit;

        public Data.NHAPKHO _Item { get; set; }

        public WindowThemNhapKho(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void LoadKhoHang()
        {
            cbbKhoHang.ItemsSource = Data.BOKho.GetAll(mTransit);
            if (cbbKhoHang.Items.Count > 0)
                cbbKhoHang.SelectedIndex = 0;
        }

        private void LoadNhaCungCap()
        {
            cbbNhaCungCap.ItemsSource = Data.BONhaCungCap.GetAll(mTransit);
            if (cbbNhaCungCap.Items.Count > 0)
                cbbNhaCungCap.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadKhoHang();
            LoadNhaCungCap();
            SetValues();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValues())
            {
                if (_Item == null)
                {
                    _Item = new Data.NHAPKHO();
                    _Item.Visual = true;
                    _Item.Deleted = false;
                    _Item.Edit = false;
                }
                GetValues();
                Data.BONhapKho.Them(_Item, (List<Data.CHITIETNHAPKHO>)btnDanhSachChiTiet.Tag, mTransit);
                MessageBox.Show(lbTieuDe.Text + " thành công");
                DialogResult = true;
            }
        }

        private void SetValues()
        {
            if (_Item == null)
            {
                if (cbbKhoHang.Items.Count > 0)
                    cbbKhoHang.SelectedIndex = 0;
                if (cbbNhaCungCap.Items.Count > 0)
                    cbbNhaCungCap.SelectedIndex = 0;
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm nhập kho";
            }
            else
            {
                cbbNhaCungCap.SelectedValue = _Item.NhaCungCapID;
                cbbKhoHang.SelectedValue = _Item.KhoID;
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa nhập kho";
            }
        }

        private void GetValues()
        {
            _Item.Visual = true;
            _Item.Deleted = false;
            _Item.KhoID = 0;
            _Item.NhaCungCapID = 0;
            _Item.ThoiGian = DateTime.Now;
            if (cbbKhoHang.Items.Count > 0)
                _Item.KhoID = (int)cbbKhoHang.SelectedValue;
            if (cbbNhaCungCap.Items.Count > 0)
                _Item.NhaCungCapID = (int)cbbNhaCungCap.SelectedValue;
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            return true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnLuu_Click(null, null);
                return;
            }

            if (e.Key == System.Windows.Input.Key.Escape)
            {
                btnHuy_Click(null, null);
                return;
            }
        }

        private void btnDanhSachChiTiet_Click(object sender, RoutedEventArgs e)
        {
            WindowDanhSachNhapKhoChiTiet win = new WindowDanhSachNhapKhoChiTiet();
            win.Init(_Item, mTransit);
            if (win.ShowDialog() == true)
            {
                btnDanhSachChiTiet.Tag = win.lsArray;
            }

        }
    }
}
