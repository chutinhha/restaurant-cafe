using System.Windows;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemNhaCungCap.xaml
    /// </summary>
    public partial class WindowThemNhaCungCap : Window
    {
        private Data.Transit mTransit;

        public Data.NHACUNGCAP _Item { get; set; }

        public WindowThemNhaCungCap(Data.Transit transit)
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
                    _Item = new Data.NHACUNGCAP();
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
                txtTenNhaCungCap.Text = "";
                txtEmail.Text = "";
                txtFax.Text = "";
                txtSoNha.Text = "";
                txtTenDuong.Text = "";
                txtDienThoaiBan.Text = "";
                txtDienThoaiDong.Text = "";
                txtMaSoThue.Text = "";
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Nhà Cung Cấp";
            }
            else
            {
                txtTenNhaCungCap.Text = _Item.TenNhaCungCap;
                txtEmail.Text = _Item.Email;
                txtFax.Text = _Item.Fax;
                txtSoNha.Text = _Item.SoNha;
                txtTenDuong.Text = _Item.TenDuong;
                txtDienThoaiBan.Text = _Item.Phone;
                txtDienThoaiDong.Text = _Item.Phone;
                txtMaSoThue.Text = _Item.MaSoThue;
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Nhà Cung Cấp";
            }
        }

        private void GetValues()
        {
            _Item.Visual = true;
            _Item.Deleted = false;
            _Item.TenNhaCungCap = txtTenNhaCungCap.Text;
            _Item.Email = txtEmail.Text;
            _Item.Fax = txtFax.Text;
            _Item.SoNha = txtSoNha.Text;
            _Item.TenDuong = txtTenDuong.Text;
            _Item.Mobile = txtDienThoaiBan.Text;
            _Item.Phone = txtDienThoaiDong.Text;
            _Item.MaSoThue = txtMaSoThue.Text;
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenNhaCungCap.Text == "")
            {
                lbStatus.Text = "Tên nhà cung cấp không được bỏ trống";
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