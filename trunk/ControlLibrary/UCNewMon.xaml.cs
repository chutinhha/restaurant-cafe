using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;

namespace ControlLibrary
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
            }
            else
            {
                txtTenDai.Text = "";
                txtTenNgan.Text = "";
                txtSapXep.Text = "1";
                ckBan.IsChecked = true;
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
    }
}