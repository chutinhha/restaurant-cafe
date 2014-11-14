using System.Windows;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemNhanVien.xaml
    /// </summary>
    public partial class WindowThemNhanVien : Window
    {
        private Data.Transit mTransit;


        public Data.BONhanVien _Item { get; set; }

        public WindowThemNhanVien(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoaiNhanVien();
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
                    _Item = new Data.BONhanVien();
                    _Item.NhanVien.Visual = true;
                    _Item.NhanVien.Deleted = false;
                    _Item.NhanVien.Edit = false;
                    _Item.NhanVien.NhanVienID = 0;
                }

                GetValues();
                DialogResult = true;
            }
        }

        private void LoadLoaiNhanVien()
        {
            cbbLoaiNhanVien.ItemsSource = Data.BOLoaiNhanVien.GetAllNoTracking(mTransit);
            if (cbbLoaiNhanVien.Items.Count > 0)
                cbbLoaiNhanVien.SelectedIndex = 0;
        }

        private void SetValues()
        {
            if (_Item == null)
            {
                if (cbbLoaiNhanVien.Items.Count > 0)
                {
                    cbbLoaiNhanVien.SelectedIndex = 0;
                }
                txtTenNhanVien.Text = "";
                txtTenDangNhap.Text = "";
                txtMatKhau.Password = "";
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Nhân Viên";
            }
            else
            {
                if (cbbLoaiNhanVien.Items.Count > 0)
                {
                    cbbLoaiNhanVien.SelectedValue = _Item.NhanVien.LoaiNhanVienID;
                }
                txtTenNhanVien.Text = _Item.NhanVien.TenNhanVien;
                txtTenDangNhap.Text = _Item.NhanVien.TenDangNhap;
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Nhân Viên";
                txtMatKhau.Password = null;
            }
        }

        private void GetValues()
        {
            _Item.NhanVien.TenNhanVien = txtTenNhanVien.Text;
            _Item.NhanVien.TenDangNhap = txtTenDangNhap.Text;
            _Item.NhanVien.LoaiNhanVienID = (int)cbbLoaiNhanVien.SelectedValue;
            if (txtMatKhau.Password != "")
            {
                _Item.NhanVien.MatKhau = Utilities.SecurityKaraoke.GetMd5Hash(txtMatKhau.Password, mTransit.HashMD5);
            }
            Data.LOAINHANVIEN lnv = (Data.LOAINHANVIEN)cbbLoaiNhanVien.SelectedItem;
            _Item.LoaiNhanVien.TenLoaiNhanVien = lnv.TenLoaiNhanVien;            
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenNhanVien.Text == "")
            {
                lbStatus.Text = "Tên nhân viên không được bỏ trống";
                return false;
            }
            else if (txtTenDangNhap.Text == "")
            {
                lbStatus.Text = "Tên đăng nhập không được bỏ trống";
                return false;
            }
            else if (txtTenDangNhap.Text.Length < 4)
            {
                lbStatus.Text = "Tên đăng nhập không được nhỏ hơn 4 ký tự";
                return false;
            }
            else if (_Item == null && txtMatKhau.Password == "")
            {
                lbStatus.Text = "Mật khẩu không được bỏ trống";
                return false;
            }
            else if (_Item == null && txtMatKhau.Password.Length < 4)
            {
                lbStatus.Text = "Mật khẩu không được nhỏ hơn 4 ký tự";
                return false;
            }

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