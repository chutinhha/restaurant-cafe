using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemQuyenNhanVien.xaml
    /// </summary>
    public partial class WindowThemQuyenNhanVien : Window
    {
        private Data.Transit mTransit;
        private Data.QUYEN mQuyen = null;

        public WindowThemQuyenNhanVien(Data.QUYEN quyen, Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
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
            List<Data.QUYENNHANVIEN> lsQuyenNhanVien = new List<Data.QUYENNHANVIEN>();
            foreach (ShowData item in lvData.Items)
            {
                lsQuyenNhanVien.Add(new Data.QUYENNHANVIEN() { QuyenID = item.QuyenID, NhanVienID = item.NhanVienID, Deleted = !item.Values });
            }
            Data.BOQuyenNhanVien.Luu(lsQuyenNhanVien, mTransit);
            LoadDanhSach();
            MessageBox.Show("Lưu thành công");
        }

        private void LoadDanhSach()
        {
            List<Data.NHANVIEN> lsNhanVien = Data.BONhanVien.GetAll(mTransit);
            List<Data.QUYENNHANVIEN> lsQuyenNhanVien = Data.BOQuyenNhanVien.GetAll(mQuyen.MaQuyen, mTransit);
            List<ShowData> lsShowData = new List<ShowData>();
            foreach (Data.NHANVIEN mi in lsNhanVien)
            {
                ShowData item = new ShowData();
                item.TenNhanVien = mi.TenNhanVien;
                item.QuyenID = mQuyen.MaQuyen;
                item.NhanVienID = mi.NhanVienID;
                if (lsQuyenNhanVien.Exists(s => s.NhanVienID == mi.NhanVienID))
                {
                    item.Values = true;
                }
                lsShowData.Add(item);
            }
            lvData.ItemsSource = lsShowData;
        }

        private class ShowData
        {
            public string TenNhanVien { get; set; }

            public int NhanVienID { get; set; }

            public bool Values { get; set; }

            public int QuyenID { get; set; }

            public ShowData()
            {
                Values = false;
                NhanVienID = 0;
                QuyenID = 0;
                TenNhanVien = "";
            }
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