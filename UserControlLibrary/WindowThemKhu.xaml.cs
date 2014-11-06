using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemKhu.xaml
    /// </summary>
    public partial class WindowThemKhu : Window
    {
        private BitmapImage mBitmapImage = null;
        private Data.Transit mTransit;

        public Data.KHU _Item { get; set; }

        public WindowThemKhu(Data.Transit transit)
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
                    _Item = new Data.KHU();
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
                txtTenKhu.Text = "";
                ckMacDinh.IsChecked = false;
                if (cbbLoaiGia.Items.Count > 0)
                {
                    cbbLoaiGia.SelectedIndex = 0;
                }
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Khu";
            }
            else
            {
                txtTenKhu.Text = _Item.TenKhu;
                ckMacDinh.IsChecked = _Item.MacDinhSoDoBan;
                cbbLoaiGia.SelectedValue = _Item.LoaiGiaID;
                if (_Item.Hinh != null && _Item.Hinh.Length > 0)
                {
                    btnHinhAnh.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Item.Hinh);
                }
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Khu";
            }
        }

        private void GetValues()
        {
            _Item.TenKhu = txtTenKhu.Text;
            _Item.MacDinhSoDoBan = ckMacDinh.IsChecked;
            _Item.LoaiGiaID = (int)cbbLoaiGia.SelectedValue;
            if (mBitmapImage != null)
            {
                _Item.Hinh = Utilities.ImageHandler.ImageToByte(mBitmapImage);
            }
        }

        private void btnHinhAnh__OnBitmapImageChanged(object sender)
        {
            mBitmapImage = btnHinhAnh.ImageBitmap;
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenKhu.Text == "")
            {
                lbStatus.Text = "Tên khu không được bỏ trống";
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