using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowThemQuyenNhanVien.xaml
    /// </summary>
    public partial class WindowThemQuyenNhanVien : Window
    {
        private Data.Transit mTransit;
        private Data.QUYEN mQuyen = null;
        private Data.BOQuyenNhanVien BOQuyenNhanVien = null;

        public WindowThemQuyenNhanVien(Data.QUYEN quyen, Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOQuyenNhanVien = new Data.BOQuyenNhanVien(transit);
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
            List<Data.BOQuyenNhanVien> lsArray = new List<Data.BOQuyenNhanVien>();
            List<Data.BOQuyenNhanVien> lsArrayDeleted = new List<Data.BOQuyenNhanVien>();
            foreach (ShowData item in lvData.Items)
            {
                if (item.Values == true)
                {
                    if (item.QuyenNhanVien.QuyenNhanVien.NhanVienID == 0 || item.QuyenNhanVien.QuyenNhanVien.NhanVienID == null)
                    {
                        item.QuyenNhanVien.QuyenNhanVien.NhanVienID = item.QuyenNhanVien.NhanVien.NhanVienID;
                        item.QuyenNhanVien.QuyenNhanVien.QuyenID = mQuyen.MaQuyen;
                        item.QuyenNhanVien.QuyenNhanVien.Deleted = false;
                        item.QuyenNhanVien.QuyenNhanVien.Visual = true;
                        lsArray.Add(item.QuyenNhanVien);
                    }
                }
                else
                {
                    if (item.QuyenNhanVien.QuyenNhanVien.NhanVienID > 0)
                    {
                        lsArrayDeleted.Add(item.QuyenNhanVien);
                    }
                }
            }
            BOQuyenNhanVien.Luu(lsArray, lsArrayDeleted, mTransit);
            LoadDanhSach();
            MessageBox.Show("Lưu thành công");
        }

        private void LoadDanhSach()
        {
            IQueryable<Data.NHANVIEN> lsMayIn = Data.BONhanVien.GetAllNoTracking(mTransit);
            IQueryable<Data.BOQuyenNhanVien> lsQuyenNhanVien = BOQuyenNhanVien.GetAll(mQuyen.MaQuyen, mTransit);
            List<ShowData> lsShowData = new List<ShowData>();
            foreach (Data.NHANVIEN mi in lsMayIn)
            {
                ShowData item = null;
                if (lsQuyenNhanVien.Where(s => s.NhanVien.NhanVienID == mi.NhanVienID).Count() > 0)
                {
                    item = new ShowData(lsQuyenNhanVien.Where(s => s.NhanVien.NhanVienID == mi.NhanVienID).FirstOrDefault(), true);
                }
                else
                {
                    item = new ShowData();
                    item.QuyenNhanVien.NhanVien = mi;
                }
                lsShowData.Add(item);
            }
            lvData.ItemsSource = lsShowData;
        }

        private class ShowData
        {
            public Data.BOQuyenNhanVien QuyenNhanVien { get; set; }
            public bool Values { get; set; }

            public ShowData(Data.BOQuyenNhanVien quyenNhanVien, bool values)
            {
                Values = values;
                QuyenNhanVien = quyenNhanVien;
            }

            public ShowData()
            {
                Values = false;
                QuyenNhanVien = new Data.BOQuyenNhanVien();
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