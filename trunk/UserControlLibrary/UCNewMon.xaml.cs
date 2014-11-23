using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCNewMon.xaml
    /// </summary>
    public partial class UCNewMon : UserControl
    {
        private BitmapImage mBitmapImage = null;
        private Data.Transit mTransit = null;
        private Data.MENUNHOM mMenuNhom = null;
        public Data.BOMenuMon BOMenuMon = null;

        public UCNewMon(Data.MENUNHOM menuNhom, Data.Transit transit, Data.BOMenuMon bOMenuMon)
        {
            InitializeComponent();
            mMenuNhom = menuNhom;
            mTransit = transit;
            BOMenuMon = bOMenuMon;
        }

        public Data.BOMenuMon _Mon { get; set; }

        public void CapNhat()
        {
            if (_Mon != null)
            {
                GetValues();
                BOMenuMon.Sua(_Mon, mTransit);
            }
            else
            {
                GetValues();
                BOMenuMon.Them(_Mon, mTransit);
            }
        }

        public void Xoa()
        {
            BOMenuMon.Xoa(_Mon, mTransit);
        }

        private void btnCaiDatMayIn_Click(object sender, RoutedEventArgs e)
        {
            WindowMenuSetMayIn win = new WindowMenuSetMayIn(_Mon, mTransit);
            win.ShowDialog();
        }

        private void btnDanhSachBan_Click(object sender, RoutedEventArgs e)
        {
            WindowDanhSachBan win = new WindowDanhSachBan(_Mon, mTransit);
            win.ShowDialog();
        }

        private void btnHinhAnh__OnBitmapImageChanged(object sender)
        {
            mBitmapImage = btnHinhAnh.ImageBitmap;
        }

        private void btnMauNen_Click(object sender, RoutedEventArgs e)
        {
        }

        private void GetValues()
        {
            if (_Mon == null)
            {
                _Mon = new Data.BOMenuMon();
                _Mon.MenuMon.Deleted = false;
                _Mon.MenuMon.NhomID = mMenuNhom.NhomID;
            }
            _Mon.MenuMon.TenDai = txtTenDai.Text;
            _Mon.MenuMon.TenNgan = txtTenNgan.Text;
            if (mBitmapImage != null)
            {
                _Mon.MenuMon.Hinh = Utilities.ImageHandler.ImageToByte(mBitmapImage);
            }
            if (txtSapXep.Text == "")
                _Mon.MenuMon.SapXep = 0;
            else
                _Mon.MenuMon.SapXep = Convert.ToInt32(txtSapXep.Text.Trim());
            _Mon.MenuMon.Visual = (bool)ckBan.IsChecked;
        }

        private void SetValues()
        {
            if (_Mon != null)
            {
                txtTenDai.Text = _Mon.MenuMon.TenDai;
                txtTenNgan.Text = _Mon.MenuMon.TenNgan;
                txtSapXep.Text = _Mon.MenuMon.SapXep.ToString();
                ckBan.IsChecked = _Mon.MenuMon.Visual;
                if (_Mon.MenuMon.Hinh != null && _Mon.MenuMon.Hinh.Length > 0)
                {
                    btnHinhAnh.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Mon.MenuMon.Hinh);
                }
                btnCaiDatMayIn.Visibility = System.Windows.Visibility.Visible;
                btnDanhSachBan.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                txtTenDai.Text = "";
                txtTenNgan.Text = "";
                txtSapXep.Text = mMenuNhom.SapXepMon.ToString();
                ckBan.IsChecked = true;
                btnCaiDatMayIn.Visibility = System.Windows.Visibility.Hidden;
                btnDanhSachBan.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetValues();
        }
    }
}