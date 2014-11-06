using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ControlLibrary;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCNewNhom.xaml
    /// </summary>
    public partial class UCNewNhom : UserControl
    {
        private int LoaiNhomID = 0;
        private Data.Transit mTransit = null;
        private BitmapImage mBitmapImage = null;

        public UCNewNhom(int loaiNhomID, Data.Transit transit)
        {
            InitializeComponent();
            LoaiNhomID = loaiNhomID;
            mTransit = transit;
            btnHinhAnh._OnBitmapImageChanged += new POSButtonImage.EventBitmapImage(btnHinhAnh__OnBitmapImageChanged);
        }

        private void btnHinhAnh__OnBitmapImageChanged(object sender)
        {
            mBitmapImage = btnHinhAnh.ImageBitmap;
        }

        public Data.MENUNHOM _Nhom { get; set; }

        public void CapNhat()
        {
            if (_Nhom != null)
            {
                GetData();
                Data.BOMenuNhom.CapNhat(_Nhom, mTransit);
            }
            else
            {
                GetData();
                Data.BOMenuNhom.Them(_Nhom, mTransit);
            }
        }

        public void GetData()
        {
            if (_Nhom == null)
            {
                _Nhom = new Data.MENUNHOM();
                _Nhom.Deleted = false;
                _Nhom.MayIn = 0;
                _Nhom.GiamGia = 0;
                _Nhom.LoaiNhomID = LoaiNhomID;
            }
            if (mBitmapImage != null)
            {
                _Nhom.Hinh = Utilities.ImageHandler.ImageToByte(mBitmapImage);
            }
            _Nhom.TenDai = txtTenDai.Text;
            _Nhom.TenNgan = txtTenNgan.Text;
            _Nhom.Visual = ckBan.IsChecked;
            if (txtSapXep.Text == "")
                _Nhom.SapXep = 0;
            else
                _Nhom.SapXep = Convert.ToInt32(txtSapXep.Text.Trim());
        }

        public void Xoa()
        {
            Data.BOMenuNhom.Xoa(_Nhom.NhomID, mTransit);
        }

        private void SetValues()
        {
            if (_Nhom != null)
            {
                txtTenDai.Text = _Nhom.TenDai;
                txtTenNgan.Text = _Nhom.TenNgan;
                txtSapXep.Text = _Nhom.SapXep.ToString();
                ckBan.IsChecked = _Nhom.Visual;
                if (_Nhom.Hinh != null && _Nhom.Hinh.Length > 0)
                {
                    btnHinhAnh.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Nhom.Hinh);
                }
            }
            else
            {
                txtTenDai.Text = "";
                txtTenNgan.Text = "";
                txtSapXep.Text = "";
                ckBan.IsChecked = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetValues();
        }

        private void btnMauNen_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}