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
    public partial class WindowBanHangChonBan : Window
    {
        private Data.BOTachGopBan mBOTachGopBan;
        public Data.BOTachGopBan _TachGopBan 
        {
            get { return mBOTachGopBan; }
        }
        private bool mIsTachBan;
        private Data.Transit mTransit;
        public WindowBanHangChonBan(Data.Transit transit,bool isTachBan)
        {
            mIsTachBan = isTachBan;
            mTransit = transit;
            mBOTachGopBan = new Data.BOTachGopBan(mTransit);
            InitializeComponent();
        }
        private void LoadKhu()
        {
            cboKhu1.ItemsSource = mBOTachGopBan.GetAllKhuVisual();
            cboBan1.ItemsSource = null;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mIsTachBan)
            {
                lbTieuDe.Text = "Tách bàn";
                lblChiDan.Content = "Chọn bàn cần tách";
            }
            else
            {
                lbTieuDe.Text = "Gộp bàn";
                lblChiDan.Content = "Chọn bàn cần gộp";
            }
            LoadKhu();            
        }
        private void cboKhu1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboKhu1.SelectedItem != null)
            {
                if (mIsTachBan)
                {
                    cboBan1.ItemsSource = mBOTachGopBan.GetAllTableInOrderPerArea((Data.KHU)cboKhu1.SelectedItem,0);
                }
                else
                {
                    cboBan1.ItemsSource = mBOTachGopBan.GetVisualTablePerArea((Data.KHU)cboKhu1.SelectedItem);
                }
            }
        }
        private void cboBan1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboBan1.SelectedItem != null)
            {
                Data.BAN ban = (Data.BAN)cboBan1.SelectedItem;
                Data.BOBanHang banhang = mBOTachGopBan.GetBanHang(ban);
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
                mBOTachGopBan.BanHang = (Data.BOBanHang)cboBan1.Tag;
                this.DialogResult = true;
            }
            else
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng chọn bàn !");                
            }
        }
    }
}
