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

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowTimKhachHang.xaml
    /// </summary>
    public partial class WindowTimKhachHang : Window
    {
        private Data.Transit mTranSit;
        private Data.BOKhachHang mBOKhachHang;
        public Data.KHACHHANG _KhachHang { get; set; }
        public WindowTimKhachHang(Data.Transit transit)
        {
            mTranSit = transit;
            mBOKhachHang = new Data.BOKhachHang(mTranSit);
            InitializeComponent();
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            var list = mBOKhachHang.TimKhachHang(txtTenKhachHang.Text, txtSoDienThoai.Text).ToList();
            lvData.Items.Clear();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    AddList(item);
                }
            }
            else
            {
                ThemMoi();
            }
        }
        private void AddList(Data.BOKhachHang item)
        {
            ListViewItem li = new ListViewItem();
            li.Content = item;
            li.Tag = item;
            lvData.Items.Add(li);
        }
        private void ThemMoi()
        {
            UserControlLibrary.WindowThemKhachHang win = new UserControlLibrary.WindowThemKhachHang(mTranSit, null);
            win._TenKhachHang = txtTenKhachHang.Text;
            win._SoDienThoai = txtSoDienThoai.Text;
            if (win.ShowDialog() == true)
            {
                //AddList(win._Item);
                //mBOKhachHang.Them(win._Item);
                mBOKhachHang.Commit();
                _KhachHang = win._Item.KhachHang;
                this.DialogResult = true;
            }
        }
        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnChon_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItem != null)
            {
                ListViewItem lv = (ListViewItem)lvData.SelectedItem;
                Data.BOKhachHang kh = (Data.BOKhachHang)lv.Tag;
                _KhachHang = kh.KhachHang;
                this.DialogResult = true;
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            ThemMoi();
        }
    }
}
