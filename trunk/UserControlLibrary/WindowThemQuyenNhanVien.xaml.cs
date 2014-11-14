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
            List<Data.BOQuyenNhanVien> lsQuyenNhanVien = new List<Data.BOQuyenNhanVien>();
            List<Data.BOQuyenNhanVien> lsQuyenNhanVienDeleted = new List<Data.BOQuyenNhanVien>();
            foreach (ShowData item in lvData.Items)
            {
                if (item.Values)
                {
                    Data.QUYENNHANVIEN qnv = null;
                    if (item.QuyenNhanVien.ID > 0)
                        qnv = item.QuyenNhanVien;
                    else
                    {
                        qnv = new Data.QUYENNHANVIEN();
                        qnv.QuyenID = mQuyen.MaQuyen;
                        qnv.NhanVienID = item.NhanVien.NhanVienID;
                        qnv.Deleted = false;
                        qnv.Edit = false;
                        qnv.Visual = true;
                    }

                    lsQuyenNhanVien.Add(new Data.BOQuyenNhanVien()
                        {
                            QuyenNhanVien = qnv
                        });
                }
                else
                {
                    if (item.QuyenNhanVien.ID > 0)
                        lsQuyenNhanVienDeleted.Add(new Data.BOQuyenNhanVien() { QuyenNhanVien = item.QuyenNhanVien });
                }
            }
            Data.BOQuyenNhanVien.Luu(lsQuyenNhanVien, lsQuyenNhanVienDeleted, mTransit);
            LoadDanhSach();
            MessageBox.Show("Lưu thành công");
        }

        private void LoadDanhSach()
        {
            IQueryable<Data.NHANVIEN> lsNhanVien = Data.BONhanVien.GetAllNoTracking(mTransit);
            List<Data.BOQuyenNhanVien> lsQuyenNhanVien = Data.BOQuyenNhanVien.GetAll(mQuyen.MaQuyen, mTransit);
            List<ShowData> lsShowData = new List<ShowData>();
            foreach (Data.NHANVIEN mi in lsNhanVien)
            {
                ShowData item = new ShowData();
                item.NhanVien = mi;
                if (lsQuyenNhanVien.Exists(s => s.QuyenNhanVien.NhanVienID == mi.NhanVienID))
                {
                    item.QuyenNhanVien = lsQuyenNhanVien.Find(s => s.QuyenNhanVien.NhanVienID == mi.NhanVienID).QuyenNhanVien;
                    item.Values = true;
                }
                lsShowData.Add(item);
            }
            lvData.ItemsSource = lsShowData;
        }

        private class ShowData
        {
            public Data.NHANVIEN NhanVien { get; set; }
            public Data.QUYENNHANVIEN QuyenNhanVien { get; set; }
            public bool Values { get; set; }

            public ShowData()
            {
                Values = false;
                NhanVien = new Data.NHANVIEN();
                QuyenNhanVien = new Data.QUYENNHANVIEN();
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