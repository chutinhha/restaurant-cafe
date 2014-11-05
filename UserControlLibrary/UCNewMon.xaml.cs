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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCNewMon.xaml
    /// </summary>
    public partial class UCNewMon : UserControl
    {
        private int NhomID = 0;
        private Data.Transit mTransit = null;

        public UCNewMon(int nhomID, Data.Transit transit)
        {
            InitializeComponent();
            NhomID = nhomID;
            mTransit = transit;
        }

        public Data.MENUMON _Mon { get; set; }

        public void CapNhat()
        {
            if (_Mon != null)
            {
                GetValues();
                Data.BOMenuMon.CapNhat(_Mon, mTransit);
            }
            else
            {
                GetValues();
                Data.BOMenuMon.Them(_Mon, mTransit);
            }
        }

        private void GetValues()
        {
            if (_Mon == null)
            {
                _Mon = new Data.MENUMON();
                _Mon.Deleted = false;
                _Mon.NhomID = NhomID;
            }
            _Mon.TenDai = txtTenDai.Text;
            _Mon.TenNgan = txtTenNgan.Text;
            if (mBitmapImage != null)
            {
                _Mon.Hinh = Utilities.ImageHandler.ImageToByte(mBitmapImage);
            }
            if (txtSapXep.Text == "")
                _Mon.SapXep = 0;
            else
                _Mon.SapXep = Convert.ToInt32(txtSapXep.Text.Trim());
            _Mon.Visual = ckBan.IsChecked;
        }

        public void Xoa()
        {
            Data.BOMenuMon.Xoa(_Mon.MonID, mTransit);
        }

        private void SetValues()
        {
            if (_Mon != null)
            {
                txtTenDai.Text = _Mon.TenDai;
                txtTenNgan.Text = _Mon.TenNgan;
                txtSapXep.Text = _Mon.SapXep.ToString();
                ckBan.IsChecked = _Mon.Visual;
                if (_Mon.Hinh != null && _Mon.Hinh.Length > 0)
                {
                    btnHinhAnh.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Mon.Hinh);
                }
                btnCaiDatMayIn.Visibility = System.Windows.Visibility.Visible;
                btnDanhSachBan.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                txtTenDai.Text = "";
                txtTenNgan.Text = "";
                txtSapXep.Text = "1";
                ckBan.IsChecked = true;
                btnCaiDatMayIn.Visibility = System.Windows.Visibility.Hidden;
                btnDanhSachBan.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetValues();
        }

        private BitmapImage mBitmapImage = null;

        private void btnMauNen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHinhAnh__OnBitmapImageChanged(object sender)
        {
            mBitmapImage = btnHinhAnh.ImageBitmap;
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
    }
}
