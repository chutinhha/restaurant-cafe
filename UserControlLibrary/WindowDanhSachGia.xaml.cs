using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowDanhSachGia.xaml
    /// </summary>
    public partial class WindowDanhSachGia : Window
    {
        private Data.Transit mTransit = null;
        private Data.BOMenuKichThuocMon mKichThuocMon = null;
        private Data.BOMenuGia BOMenuGia = null;

        public WindowDanhSachGia(Data.BOMenuKichThuocMon kichthuocmon, Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOMenuGia = new Data.BOMenuGia(transit);
            mKichThuocMon = kichthuocmon;
            PhanQuyen();
        }

        Data.BOChiTietQuyen mPhanQuyen = null;

        private void PhanQuyen()
        {
            mPhanQuyen = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.Gia.DanhSachGia);
            if (!mPhanQuyen.ChiTietQuyen.Them && !mPhanQuyen.ChiTietQuyen.Xoa && !mPhanQuyen.ChiTietQuyen.Sua)
                btnLuu.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSachGiaBan();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            List<Data.BOMenuGia> lsArray = new List<Data.BOMenuGia>();
            foreach (Data.BOMenuGia li in lvData.Items)
            {
                lsArray.Add(li);
            }
            BOMenuGia.Luu(lsArray, mTransit);
            UserControlLibrary.WindowMessageBox messageBox = new UserControlLibrary.WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            messageBox.ShowDialog();
            LoadDanhSachGiaBan();
        }

        private void btnGiaMacDinh_Click(object sender, RoutedEventArgs e)
        {
            Data.BOMenuGia item = ((Button)sender).DataContext as Data.BOMenuGia;
            item.MenuGia.Gia = mKichThuocMon.MenuKichThuocMon.GiaBanMacDinh;
            lvData.Items.Refresh();
        }

        private void LoadDanhSachGiaBan()
        {
            if (mKichThuocMon != null)
            {
                lvData.ItemsSource = BOMenuGia.GetAll(mKichThuocMon.MenuKichThuocMon.KichThuocMonID, mTransit);
            }
        }

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((mPhanQuyen.ChiTietQuyen.Them || mPhanQuyen.ChiTietQuyen.Xoa || mPhanQuyen.ChiTietQuyen.Sua) && e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
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
    }
}