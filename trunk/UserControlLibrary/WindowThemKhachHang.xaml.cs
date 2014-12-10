using System.Windows;
using System.Windows.Input;
using System;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemKhachHang.xaml
    /// </summary>
    public partial class WindowThemKhachHang : Window
    {
        private Data.Transit mTransit;
        public string _TenKhachHang { get; set; }
        public string _SoDienThoai { get; set; }
        public Data.BOKhachHang _Item { get; set; }
        public Data.BOKhachHang BOKhachHang = null;

        public WindowThemKhachHang(Data.Transit transit, Data.BOKhachHang bOKhachHang)
        {
            InitializeComponent();
            BOKhachHang = bOKhachHang;
            mTransit = transit;
            _TenKhachHang = "";
            _SoDienThoai = "";
        }

        private void LoadLoaiKhachHang()
        {
            cbbLoaiKhachHang.ItemsSource = BOKhachHang.GetLoaiKhachHang();
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
                    _Item = new Data.BOKhachHang();
                    _Item.KhachHang.Visual = true;
                    _Item.KhachHang.Deleted = false;
                    _Item.KhachHang.Edit = false;
                }
                GetValues();
                DialogResult = true;
            }
        }

        private void SetValues()
        {
            if (_Item == null)
            {
                txtTenKhachHang.Text = _TenKhachHang;
                txtEmail.Text = "";
                txtFax.Text = "";
                txtDiaChi.Text = "";
                txtDienThoaiBan.Text = "";
                txtDienThoaiDong.Text = _SoDienThoai;
                txtDuNo.Text = "";
                txtDuNoToiThieu.Text = "";
                if (cbbLoaiKhachHang.Items.Count > 0)
                    cbbLoaiKhachHang.SelectedIndex = 0;
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Khách Hàng";
            }
            else
            {
                txtTenKhachHang.Text = _Item.KhachHang.TenKhachHang;
                txtEmail.Text = _Item.KhachHang.Email;
                txtFax.Text = _Item.KhachHang.Fax;
                txtDiaChi.Text = _Item.KhachHang.DiaChi;
                txtDienThoaiBan.Text = _Item.KhachHang.Phone;
                txtDienThoaiDong.Text = _Item.KhachHang.Mobile;
                txtDuNo.Text = _Item.KhachHang.DuNo.ToString();
                txtDuNoToiThieu.Text = _Item.KhachHang.DuNoToiThieu.ToString();
                cbbLoaiKhachHang.SelectedValue = _Item.KhachHang.LoaiKhachHangID;
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Khách Hàng";
            }
        }

        private void GetValues()
        {
            _Item.KhachHang.Visual = true;
            _Item.KhachHang.Deleted = false;
            _Item.KhachHang.TenKhachHang = txtTenKhachHang.Text;
            _Item.KhachHang.Email = txtEmail.Text;
            _Item.KhachHang.Fax = txtFax.Text;
            _Item.KhachHang.DiaChi = txtDiaChi.Text;
            _Item.KhachHang.Mobile = txtDienThoaiDong.Text;
            _Item.KhachHang.Phone = txtDienThoaiBan.Text;
            if (cbbLoaiKhachHang.SelectedValue != null)
            {
                _Item.KhachHang.LoaiKhachHangID = (int)cbbLoaiKhachHang.SelectedValue;
                _Item.LoaiKhachHang = (Data.LOAIKHACHHANG)cbbLoaiKhachHang.SelectedItem;
            }
            _Item.KhachHang.DuNo = System.Convert.ToDecimal(txtDuNo.Text);
            _Item.KhachHang.DuNoToiThieu = System.Convert.ToDecimal(txtDuNoToiThieu.Text);
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

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}