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
    /// Interaction logic for WindowThemKhachHang.xaml
    /// </summary>
    public partial class WindowThemKhachHang : Window
    {
        private Data.Transit mTransit;
        public Data.KHACHHANG _Item { get; set; }
        public WindowThemKhachHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void LoadLoaiKhachHang()
        {
            cbbLoaiKhachHang.ItemsSource = Data.BOLoaiKhachHang.GetAll(mTransit);
            if (cbbLoaiKhachHang.Items.Count > 0)
                cbbLoaiKhachHang.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoaiKhachHang();
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
                    _Item = new Data.KHACHHANG();
                    _Item.Visual = true;
                    _Item.Deleted = false;
                    _Item.Edit = false;
                }
                GetValues();
                DialogResult = true;
            }

        }

        private void SetValues()
        {
            if (_Item == null)
            {
                txtTenKhachHang.Text = "";
                txtEmail.Text = "";
                txtFax.Text = "";
                txtSoNha.Text = "";
                txtTenDuong.Text = "";
                txtDienThoaiBan.Text = "";
                txtDienThoaiDong.Text = "";
                txtDuNo.Text = "";
                txtDuNoToiThieu.Text = "";
                if (cbbLoaiKhachHang.Items.Count > 0)
                    cbbLoaiKhachHang.SelectedIndex = 0;
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Khách Hàng";
            }
            else
            {
                txtTenKhachHang.Text = _Item.TenKhachHang;
                txtEmail.Text = _Item.Email;
                txtFax.Text = _Item.Fax;
                txtSoNha.Text = _Item.SoNha;
                txtTenDuong.Text = _Item.TenDuong;
                txtDienThoaiBan.Text = _Item.Phone;
                txtDienThoaiDong.Text = _Item.Phone;
                txtDuNo.Text = _Item.DuNo.ToString();
                txtDuNoToiThieu.Text = _Item.DuNoToiThieu.ToString();
                cbbLoaiKhachHang.SelectedValue = _Item.LoaiKhachHangID;
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Khách Hàng";
            }
        }

        private void GetValues()
        {
            _Item.Visual = true;
            _Item.Deleted = false;
            _Item.TenKhachHang = txtTenKhachHang.Text;
            _Item.Email = txtEmail.Text;
            _Item.Fax = txtFax.Text;
            _Item.SoNha = txtSoNha.Text;
            _Item.TenDuong = txtTenDuong.Text;
            _Item.Mobile = txtDienThoaiBan.Text;
            _Item.Phone = txtDienThoaiDong.Text;
            _Item.LoaiKhachHangID = (int)cbbLoaiKhachHang.SelectedValue;
            _Item.DuNo = System.Convert.ToDecimal(txtDuNo.Text);
            _Item.DuNoToiThieu = System.Convert.ToDecimal(txtDuNoToiThieu.Text);
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenKhachHang.Text == "")
            {
                lbStatus.Text = "Tên khách hàng không được bỏ trống";
                return false;
            }

            if (txtDuNo.Text == "")
                txtDuNo.Text = "0";
            if (txtDuNoToiThieu.Text == "")
                txtDuNoToiThieu.Text = "1000";
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
    }
}
