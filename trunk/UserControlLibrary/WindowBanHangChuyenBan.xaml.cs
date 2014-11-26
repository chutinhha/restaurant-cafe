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
    /// Interaction logic for WindowChuyenBan.xaml
    /// </summary>
    public partial class WindowBanHangChuyenBan : Window
    {        
        private Data.Transit mTransit;
        private Data.BOChuyenBan mBOChuyenBan;
        public WindowBanHangChuyenBan(Data.Transit tran)
        {
            mTransit = tran;
            mBOChuyenBan = new Data.BOChuyenBan(mTransit);
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadKhu();
        }
        private void LoadKhu()
        {
            cboKhu1.ItemsSource = cboKhu2.ItemsSource = mBOChuyenBan.GetAllVisual();
            cboBan1.ItemsSource = null;
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if (cboBan1.SelectedItem==null)
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng chọn bàn cần chuyển");
                return;
            }
            if (cboBan2.SelectedItem==null)
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng chọn bàn chuyển đến");
                return;
            }
            if (cboBan1.SelectedItem!=null && cboBan2.SelectedItem!=null)
            {
                Data.BAN ban=(Data.BAN)cboBan2.SelectedItem;
                mBOChuyenBan.ChuyenBan(ban);
                LoadKhu();
                UserControlLibrary.WindowMessageBox.ShowDialog("Đã chuyển thành công !");
            }
        }

        private void cboKhu1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboKhu1.SelectedItem!=null)
            {
                cboKhu2.SelectedItem = cboKhu1.SelectedItem;
                cboBan1.ItemsSource = mBOChuyenBan.GetAllTableInOrderPerArea((Data.KHU)cboKhu1.SelectedItem);
            }
        }

        private void cboKhu2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboKhu2.SelectedItem!=null)
            {
                cboBan2.ItemsSource=mBOChuyenBan.GetAllTableNotInOrderPerArea((Data.KHU)cboKhu2.SelectedItem);
            }
        }

        private void cboBan1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboBan1.SelectedItem!=null)
            {                
                Data.BAN ban = (Data.BAN)cboBan1.SelectedItem;
                mBOChuyenBan.LoadBanHang(ban);
                lvData.ItemsSource = mBOChuyenBan._BanHang._ListChiTietBanHang;                
            }
            else
            {
                lvData.ItemsSource = null;
            }
        }
    }
}
