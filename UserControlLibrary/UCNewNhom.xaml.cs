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
        private Data.Transit mTransit = null;
        private BitmapImage mBitmapImage = null;
        private Data.BOMenuNhom BOMenuNhom = null;

        public UCNewNhom(Data.Transit transit, Data.BOMenuNhom bOMenuNhom)
        {
            InitializeComponent();
            mTransit = transit;
            BOMenuNhom = bOMenuNhom;
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
                BOMenuNhom.Sua(_Nhom, mTransit);
            }
            else
            {
                GetData();
                BOMenuNhom.Them(_Nhom, mTransit);
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
            }
            if (mBitmapImage != null)
            {
                _Nhom.MenuNhom.Hinh = Utilities.ImageHandler.ImageToByte(mBitmapImage);
            }
            _Nhom.MenuNhom.TenDai = txtTenDai.Text;
            _Nhom.MenuNhom.TenNgan = txtTenNgan.Text;
            _Nhom.MenuNhom.Visual = (bool)ckBan.IsChecked;
            _Nhom.MenuNhom.LoaiNhomID = (int)cbbLoaiNhom.SelectedValue;
            if (txtSapXep.Text == "")
                _Nhom.MenuNhom.SapXep = 0;
            else
                _Nhom.MenuNhom.SapXep = Convert.ToInt32(txtSapXep.Text.Trim());
        }

        public void Xoa()
        {
            BOMenuNhom.Xoa(_Nhom, mTransit);
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
                if (cbbLoaiNhom.Items.Count > 0)
                    cbbLoaiNhom.SelectedIndex = 0;
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

        private void cbbLoaiNhom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbLoaiNhom.SelectedItem != null && _Nhom == null)
            {
                txtSapXep.Text = ((Data.MENULOAINHOM)cbbLoaiNhom.SelectedItem).SapXepNhom.ToString();
            }
        }
    }
}