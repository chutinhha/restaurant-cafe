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
    /// Interaction logic for WindowBanQuyen.xaml
    /// </summary>
    public partial class WindowBanQuyen : Window
    {
        Data.Transit mTransit;
        public WindowBanQuyen(Data.Transit transit)
        {
            mTransit = transit;
            InitializeComponent();
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {            
            if (Utilities.SecurityKaraoke.CheckLisence(txtBanQuyen.Text,mTransit.HashMD5))
            {
                    Data.Transit.MakeLisence(Convert.ToInt16(txtBanQuyen.Text[0]+""),mTransit);                    
                    mTransit.ThamSo.BanQuyen = txtBanQuyen.Text;                 
                    mTransit.KaraokeEntities.SaveChanges();
                    UserControlLibrary.WindowMessageBox.ShowDialog("Cập nhật thành công");
                    this.DialogResult = true;
            }
            else
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Bản quyền không đúng");
            }            
        }        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            LoadBanQuyen();
            //txtMaSanPham.Text = Utilities.SecurityKaraoke.GetProductID(0, mTransit.HashMD5);
        }
        private void LoadBanQuyen()
        {
            if (Utilities.SecurityKaraoke.CheckLisence(mTransit.ThamSo.BanQuyen, mTransit.HashMD5))
            {
                txtMaSanPham.Text = Utilities.SecurityKaraoke.GetProductID(Convert.ToInt16(mTransit.ThamSo.BanQuyen[1] + "") + 1, mTransit.HashMD5);
            }
            else
            {
                txtMaSanPham.Text = "Bản quyền bị lỗi";
            }
        }
    }
}
