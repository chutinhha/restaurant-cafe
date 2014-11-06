using System;
using System.Windows;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemLoaiKhachHang.xaml
    /// </summary>
    public partial class WindowThemLoaiKhachHang : Window
    {
        private Data.Transit mTransit;

        public Data.LOAIKHACHHANG _Item { get; set; }

        public WindowThemLoaiKhachHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                    _Item = new Data.LOAIKHACHHANG();
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
                txtLoaiKhachHang.Text = "";
                txtPhanTramGiam.Text = "0";
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Loại Khách Hàng";
            }
            else
            {
                txtLoaiKhachHang.Text = _Item.TenLoaiKhachHang;
                txtPhanTramGiam.Text = _Item.PhanTramGiamGia.ToString();
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Loại Khách Hàng";
            }
        }

        private void GetValues()
        {
            _Item.TenLoaiKhachHang = txtLoaiKhachHang.Text;
            _Item.PhanTramGiamGia = System.Convert.ToInt32(txtPhanTramGiam.Text);
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtLoaiKhachHang.Text == "")
            {
                lbStatus.Text = "Loại khách hàng không được bỏ trống";
                return false;
            }

            if (txtPhanTramGiam.Text == "")
                txtPhanTramGiam.Text = "0";
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