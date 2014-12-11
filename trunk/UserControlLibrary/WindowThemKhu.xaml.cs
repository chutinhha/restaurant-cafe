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
        private byte[] mHinh = null;
        private Data.Transit mTransit;

        public WindowThemKhu(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            btnHinhAnh.SetTransit(mTransit);
        }

        public Data.KHU _Item { get; set; }
        private void btnHinhAnh__OnBitmapImageChanged(object sender)
        {
            BitmapFrame img = Utilities.ImageHandler.CreateResizedImage(btnHinhAnh.ImageBitmap, 1024, 768, 0);
            mHinh = Utilities.ImageHandler.ImageToByte(img);
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
                    _Item.Deleted = false;
                    _Item.Edit = false;
                }
                GetValues();
                DialogResult = true;
            }
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

        private void GetValues()
        {
            _Item.TenKhu = txtTenKhu.Text;
            _Item.MacDinhSoDoBan = (bool)ckMacDinh.IsChecked;
            _Item.Visual = (bool)ckChoPhepHienThi.IsChecked;
            _Item.Hinh = mHinh;
        }

        private void SetValues()
        {
            if (_Item == null)
            {
                txtTenKhu.Text = "";
                ckMacDinh.IsChecked = false;
                ckChoPhepHienThi.IsChecked = true;
                btnLuu.Content = mTransit.StringButton.Them;
                lbTieuDe.Text = "Thêm Khu";
            }
            else
            {
                txtTenKhu.Text = _Item.TenKhu;
                ckMacDinh.IsChecked = _Item.MacDinhSoDoBan;
                ckChoPhepHienThi.IsChecked = _Item.Visual;
                if (_Item.Hinh != null && _Item.Hinh.Length > 0)
                {
                    btnHinhAnh.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Item.Hinh);
                }
                btnLuu.Content = mTransit.StringButton.Luu;
                lbTieuDe.Text = "Sửa Khu";
            }
        }

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetValues();
        }
    }
}