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
    /// Interaction logic for WindowBanHangTheoGia.xaml
    /// </summary>
    public partial class WindowBanHangTheoGia : Window
    {
        private Data.BOMenuGia mBOMenuGia;
        private Data.Transit mTransit;
        private Data.MENUKICHTHUOCMON mMENUKICHTHUOCMON;
        public Data.BOMenuGia _MenuGia { get; set; }
        public WindowBanHangTheoGia(Data.Transit transit,Data.MENUKICHTHUOCMON ktm)
        {
            mTransit = transit;
            mMENUKICHTHUOCMON = ktm;
            mBOMenuGia = new Data.BOMenuGia(mTransit);
            InitializeComponent();
        }
        private void LoadData()
        {
            cboGiaBan.ItemsSource = mBOMenuGia.GetAllByKichThuocMon(mMENUKICHTHUOCMON);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if (cboGiaBan.SelectedItem!= null)
            {
                _MenuGia = (Data.BOMenuGia)cboGiaBan.SelectedItem;
                this.DialogResult = true;
            }
            else
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng chọn giá bán");                                
            }
        }
    }
}
