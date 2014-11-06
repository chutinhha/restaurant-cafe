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

        public WindowThemCaiDatChucNang(Data.QUYEN quyen, Data.Transit transit)
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
            List<Data.CHITIETQUYEN> lsChiTietQuyen = new List<Data.CHITIETQUYEN>();
            foreach (ShowData item in lvData.Items)
            {
                bool deleted = true;
                if (item.Xem || item.Them || item.Sua || item.Xoa || item.DangNhap)
                    deleted = false;
                lsChiTietQuyen.Add(new Data.CHITIETQUYEN() { QuyenID = item.QuyenID, ChucNangID = item.ChucNangID, Xem = item.Xem, Them = item.Them, Sua = item.Sua, Xoa = item.Xoa, DangNhap = item.DangNhap, Deleted = deleted });
            }
            Data.BOChiTietQuyen.Luu(lsChiTietQuyen, mTransit);
            LoadDanhSach();
            MessageBox.Show("Lưu thành công");
        }

        private void LoadDanhSach()
        {
            List<Data.CHUCNANG> lsChucNang = Data.BOChucNang.GetAll(mTransit);
            List<Data.CHITIETQUYEN> lsChiTietQuyen = Data.BOChiTietQuyen.GetAll(mQuyen.MaQuyen, mTransit);
            List<ShowData> lsShowData = new List<ShowData>();
            foreach (Data.CHUCNANG mi in lsChucNang)
            {
                ShowData item = new ShowData();
                item.TenChucNang = mi.TenChucNang;
                item.QuyenID = mQuyen.MaQuyen;
                item.ChucNangID = mi.ChucNangID;
                if (lsChiTietQuyen.Exists(s => s.ChucNangID == mi.ChucNangID))
                {
                    Data.CHITIETQUYEN ctq = lsChiTietQuyen.Where(s => s.ChucNangID == mi.ChucNangID).First();
                    item.Xem = (bool)ctq.Xem;
                    item.Them = (bool)ctq.Them;
                    item.Sua = (bool)ctq.Sua;
                    item.Xoa = (bool)ctq.Xoa;
                    item.DangNhap = (bool)ctq.DangNhap;
                }

                lsShowData.Add(item);
            }
            lvData.ItemsSource = lsShowData;
        }

        private class ShowData
        {
            public string TenChucNang { get; set; }

            public int ChucNangID { get; set; }

            public int QuyenID { get; set; }

            public bool Xem { get; set; }

            public bool Xoa { get; set; }

            public bool Sua { get; set; }

            public bool Them { get; set; }

            public bool DangNhap { get; set; }

            public ShowData()
            {
                ChucNangID = 0;
                QuyenID = 0;
                TenChucNang = "";
                Xem = false;
                Xoa = false;
                Sua = false;
                Them = false;
                DangNhap = false;
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