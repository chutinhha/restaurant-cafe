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
    /// Interaction logic for WindowChonBanGop.xaml
    /// </summary>
    public partial class WindowBanHangChonBanGop : Window
    {
        private Data.BOTachGopBan mBOGopBan;
        public Data.BOTachGopBan _GopBan 
        {
            get { return mBOGopBan; }
        }
        private Data.Transit mTransit;
        public WindowBanHangChonBanGop(Data.Transit transit)
        {
            mTransit = transit;
            mBOGopBan = new Data.BOTachGopBan(mTransit);
            InitializeComponent();
        }
        private void LoadKhu()
        {
            cboKhu1.ItemsSource = mBOGopBan.GetAllKhuVisual();
            cboBan1.ItemsSource = null;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadKhu();
        }
        private void cboKhu1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboKhu1.SelectedItem != null)
            {                
                cboBan1.ItemsSource = mBOGopBan.GetVisualTablePerArea((Data.KHU)cboKhu1.SelectedItem);
            }
        }
        private void cboBan1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboBan1.SelectedItem != null)
            {
                Data.BAN ban = (Data.BAN)cboBan1.SelectedItem;
                Data.BOBanHang banhang = mBOGopBan.GetBanHang(ban);
                lvData1.ItemsSource = banhang._ListChiTietBanHang;                
                cboBan1.Tag = banhang;
            }
            else
            {
                lvData1.ItemsSource = null;
            }
        }
        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if (cboBan1.SelectedItem != null)
            {
                mBOGopBan.BanHang = (Data.BOBanHang)cboBan1.Tag;
                this.DialogResult = true;
            }
            else
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng chọn bàn !");                
            }
        }
    }
}
