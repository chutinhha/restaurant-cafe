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

        public Data.BOMenuNhom _Nhom { get; set; }

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
                _Nhom = new Data.BOMenuNhom();
                _Nhom.MenuNhom.Deleted = false;
                _Nhom.MenuNhom.MayIn = 0;
                _Nhom.MenuNhom.GiamGia = 0;
                _Nhom.MenuNhom.LoaiNhomID = LoaiNhomID;
            }
            if (mBitmapImage != null)
            {
                _Nhom.MenuNhom.Hinh = Utilities.ImageHandler.ImageToByte(mBitmapImage);
            }
            _Nhom.MenuNhom.TenDai = txtTenDai.Text;
            _Nhom.MenuNhom.TenNgan = txtTenNgan.Text;
            _Nhom.MenuNhom.Visual = ckBan.IsChecked;
            _Nhom.MenuNhom.LoaiNhomID = (int)cbbLoaiNhom.SelectedValue;
            if (txtSapXep.Text == "")
                _Nhom.MenuNhom.SapXep = 0;
            else
                _Nhom.MenuNhom.SapXep = Convert.ToInt32(txtSapXep.Text.Trim());
        }

        public void Xoa()
        {
            Data.BOMenuNhom.Xoa(_Nhom, mTransit);
        }

        private void SetValues()
        {
            if (_Nhom != null)
            {
                txtTenDai.Text = _Nhom.MenuNhom.TenDai;
                txtTenNgan.Text = _Nhom.MenuNhom.TenNgan;
                txtSapXep.Text = _Nhom.MenuNhom.SapXep.ToString();
                ckBan.IsChecked = _Nhom.MenuNhom.Visual;
                cbbLoaiNhom.SelectedValue = _Nhom.MenuNhom.LoaiNhomID;
                if (_Nhom.MenuNhom.Hinh != null && _Nhom.MenuNhom.Hinh.Length > 0)
                {
                    btnHinhAnh.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Nhom.MenuNhom.Hinh);
                }
            }
            else
            {
                txtTenDai.Text = "";
                txtTenNgan.Text = "";
                txtSapXep.Text = "";
                cbbLoaiNhom.SelectedValue = LoaiNhomID;
                ckBan.IsChecked = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoaiNhom();
            SetValues();

        }

        private void LoadLoaiNhom()
        {
            cbbLoaiNhom.ItemsSource = Data.BOLoaiNhom.GetAll(mTransit);
        }
    }
}