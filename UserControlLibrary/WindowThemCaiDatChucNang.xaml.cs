using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemCaiDatChucNang.xaml
    /// </summary>
    public partial class WindowThemCaiDatChucNang : Window
    {
        private Data.Transit mTransit;
        private Data.QUYEN mQuyen = null;
        private Data.BOChiTietQuyen BOChiTietQuyen = null;

        public WindowThemCaiDatChucNang(Data.QUYEN quyen, Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOChiTietQuyen = new Data.BOChiTietQuyen(mTransit);
            mQuyen = quyen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTenQuyen.Text = mQuyen.TenQuyen;
            LoadDanhSach();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            List<Data.BOChiTietQuyen> lsArray = new List<Data.BOChiTietQuyen>();
            foreach (Data.BOChiTietQuyen item in lvData.Items)
            {
                lsArray.Add(item);
            }
            BOChiTietQuyen.Luu(lsArray, mTransit);
            LoadDanhSach();
            UserControlLibrary.WindowMessageBox messageBox = new UserControlLibrary.WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            messageBox.ShowDialog();
            DialogResult = true;
        }

        private void LoadDanhSach()
        {
            IQueryable<Data.CHUCNANG> lsChucNang = Data.BOChucNang.GetAllNoTracking(mTransit);
            IQueryable<Data.BOChiTietQuyen> lsChiTietQuyen = BOChiTietQuyen.GetAll(mQuyen.MaQuyen, mTransit);
            List<Data.BOChiTietQuyen> lsShowData = new List<Data.BOChiTietQuyen>();
            foreach (Data.CHUCNANG cn in lsChucNang)
            {
                Data.BOChiTietQuyen item = null;
                if (lsChiTietQuyen.Where(s => s.ChiTietQuyen.ChucNangID == cn.ChucNangID).Count() > 0)
                {
                    item = lsChiTietQuyen.Where(s => s.ChiTietQuyen.ChucNangID == cn.ChucNangID).FirstOrDefault();
                    item.IsChoPhep = cn.ChoPhep ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                    item.IsDangNhap = cn.DangNhap ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                    item.IsThem = cn.Them ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                    item.IsXoa = cn.Xoa ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                    item.IsSua = cn.Sua ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                }
                else
                {
                    item = new Data.BOChiTietQuyen();
                    item.ChiTietQuyen.NhomChucNangID = (int)cn.NhomChucNangID;
                    item.ChucNang = cn;
                    item.ChiTietQuyen.ChucNangID = cn.ChucNangID;
                    item.ChiTietQuyen.QuyenID = mQuyen.MaQuyen;
                    item.ChiTietQuyen.Deleted = false;
                    item.ChiTietQuyen.Edit = false;
                    item.ChiTietQuyen.Visual = true;
                    item.ChiTietQuyen.ChoPhep = false;
                    item.ChiTietQuyen.DangNhap = false;
                    item.ChiTietQuyen.Them = false;
                    item.ChiTietQuyen.Xoa = false;
                    item.ChiTietQuyen.Sua = false;
                    item.IsChoPhep = cn.ChoPhep ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                    item.IsDangNhap = cn.DangNhap ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                    item.IsThem = cn.Them ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                    item.IsXoa = cn.Xoa ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                    item.IsSua = cn.Sua ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                }
                lsShowData.Add(item);
            }
            lvData.ItemsSource = lsShowData;
        }

        public Visibility GetVisibility(bool value)
        {
            if (value)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Collapsed;
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

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}