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
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLyLoaiGia.xaml
    /// </summary>
    public partial class WindowQuanLyLoaiGia : Window
    {
        private Data.MENULOAIGIA mLoaiGia = null;
        public WindowQuanLyLoaiGia()
        {
            InitializeComponent();            
        }

        private void btnTaoMoi_Click(object sender, RoutedEventArgs e)
        {
            txtDienGiai.Text = "";
            txtLoaiGia.Text = "";
            mLoaiGia = null;
            btnThemMoi.Content = "Thêm mới";
        }

        private void btnThemMoi_Click(object sender, RoutedEventArgs e)
        {
            if (mLoaiGia != null)
            {
                GetValues();
                Data.BOMenuLoaiGia.CapNhat(mLoaiGia);
                GetData();
            }
            else
            {
                GetValues();
                Data.BOMenuLoaiGia.Them(mLoaiGia);
                btnTaoMoi_Click(null, null);
                GetData();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (mLoaiGia != null)
            {
                Data.BOMenuLoaiGia.Xoa(mLoaiGia.LoaiGiaID);
                btnTaoMoi_Click(null, null);
                GetData();
            }
        }

        private void GetData()
        {            
            lvLoaiGia.ItemsSource = Data.BOMenuLoaiGia.GetAll();
        }

        private void GetValues()
        {
            if (mLoaiGia == null)
            {
                mLoaiGia = new Data.MENULOAIGIA();
            }
            mLoaiGia.DienGiai = txtDienGiai.Text;
            mLoaiGia.Ten = txtLoaiGia.Text;
            mLoaiGia.Deleted = false;
            mLoaiGia.Visual = true;
        }

        private void SetValues()
        {
            if (mLoaiGia != null)
            {
                txtDienGiai.Text = mLoaiGia.DienGiai;
                txtLoaiGia.Text = mLoaiGia.Ten;
                btnThemMoi.Content = "Cập nhật";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvLoaiGia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                mLoaiGia = (item as Data.MENULOAIGIA);
                SetValues();
            }
        }
    }
}
