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
                GetData();
                Data.BOMenuMon.CapNhat(_Mon, mTransit);
            }
            else
            {
                GetData();
                Data.BOMenuMon.Them(_Mon, mTransit);
            }
        }

        public void GetData()
        {
            if (_Mon == null)
            {
                _Mon = new Data.MENUMON();
            }
            _Mon.TenDai = txtTenDai.Text;
            _Mon.TenNgan = txtTenNgan.Text;
            if (mBitmapImage != null)
            {
                _Mon.Hinh = Utilities.ImageHandler.ImageToByte(btnHinhAnh.ImageBitmap); ;
            }
            if (txtSapXep.Text == "")
                _Mon.SapXep = 0;
            else
                _Mon.SapXep = Convert.ToInt32(txtSapXep.Text.Trim());
        }

        public void Xoa()
        {
            Data.BOMenuMon.Xoa(_Mon.MonID, mTransit);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Mon != null)
            {
                txtTenDai.Text = _Mon.TenDai;
                txtTenNgan.Text = _Mon.TenNgan;
                txtSapXep.Text = _Mon.SapXep.ToString();
                if (_Mon.Hinh != null && _Mon.Hinh.Length > 0)
                {
                    mBitmapImage = Utilities.ImageHandler.BitmapImageFromByteArray(_Mon.Hinh);
                    btnHinhAnh.Image = mBitmapImage;
                }
            }
        }

        private BitmapImage mBitmapImage = null;
        private void btnHinhAnh_Click(object sender, RoutedEventArgs e)
        {
            Stream checkStream = null;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "All Image Files | *.*";
            if ((bool)openFileDialog.ShowDialog())
            {

                if ((checkStream = openFileDialog.OpenFile()) != null)
                {
                    Stream fs = File.OpenRead(openFileDialog.FileName);
                    mBitmapImage = new BitmapImage();
                    mBitmapImage.BeginInit();
                    mBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    mBitmapImage.StreamSource = fs;
                    mBitmapImage.EndInit();
                }
            }
        }

        private void btnMauNen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}