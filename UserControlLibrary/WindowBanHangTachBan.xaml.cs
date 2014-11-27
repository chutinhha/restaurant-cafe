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
    /// Interaction logic for WindowBanHangTachBan.xaml
    /// </summary>
    public partial class WindowBanHangTachBan : Window
    {
        private Data.Transit mTransit;
        private Data.BOTachGopBan mBOTachGopBan;
        public WindowBanHangTachBan(Data.Transit transit, Data.BOTachGopBan tachban)
        {
            mTransit = transit;
            mBOTachGopBan = tachban;            
            InitializeComponent();
        }
        private void LoadKhu()
        {
            cboKhu1.ItemsSource = mBOTachGopBan.GetAllKhuVisual();
            cboBan1.ItemsSource = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblBanTach.Content = mBOTachGopBan.BanHang.TenBan;
            lvData1.ItemsSource = mBOTachGopBan.BanHang._ListChiTietBanHang;
            LoadKhu();
        }
        private void cboKhu1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboKhu1.SelectedItem != null)
            {
                cboBan1.ItemsSource = mBOTachGopBan.GetAllTableNotInOrderPerArea((Data.KHU)cboKhu1.SelectedItem);
            }
        }

        private void cboBan1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboBan1.SelectedItem != null)
            {
                Data.BAN ban = (Data.BAN)cboBan1.SelectedItem;
                Data.BOBanHang banhang = mBOTachGopBan.GetBanHang(ban);
                lvData1.ItemsSource = banhang._ListChiTietBanHang;
                lvData1.Tag = banhang;
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
            if (mBOTachGopBan.KiemTra())
            {
                mBOTachGopBan.XuliGopBan();
                this.DialogResult = true;
                UserControlLibrary.WindowMessageBox.ShowDialog("Gộp bàn thành công !");
            }
            else
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng chọn bàn gộp !");
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (cboBan1.SelectedItem != null)
            {
                if (lvData1.Tag != null)
                {
                    Data.BOBanHang bh = (Data.BOBanHang)lvData1.Tag;
                    mBOTachGopBan.AddBanHang(bh);
                    lvData2.ItemsSource = mBOTachGopBan._ListBan;
                    lvData2.Items.Refresh();
                }
            }
        }
        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData2.SelectedItem != null)
            {
                Data.BOBanHang bh = (Data.BOBanHang)lvData2.SelectedItem;
                mBOTachGopBan.XoaBanHang(bh);
                lvData2.Items.Refresh();
                //lvData2.Items.Remove(li);
            }
        }
    }
}

