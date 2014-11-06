using System;
using System.Windows;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemThe.xaml
    /// </summary>
    public partial class WindowThemThe : Window
    {
        private Data.Transit mTransit;

        public Data.THE _Item { get; set; }

        public WindowThemThe(Data.Transit transit)
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
                    _Item = new Data.THE();
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
                txtTenThe.Text = "";
                txtChietKhau.Text = "0";
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Thẻ";
            }
            else
            {
                txtTenThe.Text = _Item.TenThe;
                txtChietKhau.Text = _Item.ChietKhau.ToString();
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Thẻ";
            }
        }

        private void GetValues()
        {
            _Item.TenThe = txtTenThe.Text;
            _Item.ChietKhau = System.Convert.ToInt32(txtChietKhau.Text);
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenThe.Text == "")
            {
                lbStatus.Text = "Tên thẻ không được bỏ trống";
                return false;
            }

            if (txtChietKhau.Text == "")
                txtChietKhau.Text = "0";
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