using System;
using System.Windows;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemKho.xaml
    /// </summary>
    public partial class WindowThemKho : Window
    {
        private Data.Transit mTransit;

        public Data.KHO _Item { get; set; }

        public WindowThemKho(Data.Transit transit)
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
                    _Item = new Data.KHO();
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
                txtTenKho.Text = "";
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Kho";
            }
            else
            {
                txtTenKho.Text = _Item.TenKho;
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Kho";
            }
        }

        private void GetValues()
        {
            _Item.TenKho = txtTenKho.Text;
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenKho.Text == "")
            {
                lbStatus.Text = "Loại khách hàng không được bỏ trống";
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

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}