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
using System.IO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowQuanLySoDoBan.xaml
    /// </summary>
    public partial class WindowQuanLySoDoBan : Window
    {
        private Data.Transit mTransit = null;
        private Data.BAN mBan;
        public WindowQuanLySoDoBan(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uCFloorPlan1.Init(mTransit);
            LoadKhuVuc();            
        }

        private void LoadKhuVuc()
        {            
            var listKhu = Data.BOKhu.GetAll();
            cboKhuVuc.ItemsSource = Data.BOKhu.GetAll();
            if (cboKhuVuc.Items.Count > 0)
            {
                cboKhuVuc.SelectedItem=cboKhuVuc.Items[0];
                uCFloorPlan1.LoadTable((Data.KHU)cboKhuVuc.Items[0]);
            }            
        }

        private void cboKhuVuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Data.KHU khu = (Data.KHU)cboKhuVuc.SelectedItem;            
            uCFloorPlan1.LoadTable(khu);
        }

        private void uCFloorPlan1__OnEventFloorPlan(object ob)
        {
            ControlLibrary.POSButtonTable tbl = (ControlLibrary.POSButtonTable)ob;
            txtTenBan.Text = tbl._Ban.TenBan;
            if (tbl._Ban.Hinh != null && tbl._Ban.Hinh.Length > 0)
            {
                btnHinhDaiDien.Image = Utilities.ImageHandler.BitmapImageFromByteArray(tbl._Ban.Hinh);
            }
            else
            {
                var uriSource = new Uri(@"/ControlLibrary;component/Images/NoImages.jpg", UriKind.Relative);
                btnHinhDaiDien.Image = new BitmapImage(uriSource);
            }
        }        

        private void btnThemMoi_Click(object sender, RoutedEventArgs e)
        {
            mBan = null;
            txtTenBan.Text = "";
            btnHinhDaiDien.DefaultImage();
        }

        private void pOSButtonIconHorizontal1_Click(object sender, RoutedEventArgs e)
        {
            if (mBan==null)
            {
                mBan = new Data.BAN();
                mBan.TenBan = "";
                mBan.Hinh = null;
            }
        }
        private void  GetValue()
        {
            if (cboKhuVuc.SelectedIndex>0 && mBan!=null)
            {
                mBan.TenBan = txtTenBan.Text;
                mBan.KhuID = (int)cboKhuVuc.SelectedValue;
                mBan.Hinh = Utilities.ImageHandler.ImageToByte(btnHinhDaiDien.ImageBitmap);
            }

        }
    }
}
