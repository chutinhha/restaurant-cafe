using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowDanhSachGia.xaml
    /// </summary>
    public partial class WindowDanhSachGia : Window
    {
        private Data.Transit mTransit = null;
        private Data.MENUKICHTHUOCMON mKichThuocMon = null;

        public WindowDanhSachGia(Data.MENUKICHTHUOCMON kichthuocmon, Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            mTransit.KaraokeEntities = new Data.KaraokeEntities();
            mTransit.KaraokeEntities.MENUGIAs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            mKichThuocMon = kichthuocmon;
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
            List<Data.MENUGIA> lsArray = new List<Data.MENUGIA>();
            foreach (Data.MENUGIA li in lvData.Items)
            {
                lsArray.Add(li);
            }
            Data.BOMenuGia.Luu(lsArray, mTransit);
            MessageBox.Show("Lưu thành công");
            LoadDanhSachGiaBan();
        }

        private void btnGiaMacDinh_Click(object sender, RoutedEventArgs e)
        {
            Data.MENUGIA item = ((Button)sender).DataContext as Data.MENUGIA;
            item.Gia = mKichThuocMon.GiaBanMacDinh;
            lvData.Items.Refresh();
        }

        private void LoadDanhSachGiaBan()
        {
            if (mKichThuocMon != null)
            {
                lvData.ItemsSource = Data.BOMenuGia.GetAll(mKichThuocMon.KichThuocMonID, mTransit);
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