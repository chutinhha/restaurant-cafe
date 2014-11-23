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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCSoDoBan.xaml
    /// </summary>
    public partial class UCSoDoBan : UserControl
    {
        private Data.Transit mTransit = null;
        private ControlLibrary.POSButtonTable mTableButton;
        public UCSoDoBan(Data.Transit transit)
        {
            mTransit = transit;
            InitializeComponent();

        }       

        private void LoadKhuVuc()
        {
            cboKhuVuc.ItemsSource = Data.BOKhu.GetAllNoTracking(mTransit);
            if (cboKhuVuc.Items.Count > 0)
            {
                cboKhuVuc.SelectedItem = cboKhuVuc.Items[0];
            }
        }

        private void cboKhuVuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboKhuVuc.SelectedItem != null)
            {
                Data.KHU khu = (Data.KHU)cboKhuVuc.SelectedItem;
                LoadChiTietKhuVuc(khu);
            }
        }
        private void LoadChiTietKhuVuc(Data.KHU khu)
        {
            uCFloorPlan1.LoadTable(khu);
            if (khu.Hinh != null && khu.Hinh.Length > 0)
            {
                btnHinhSoDoBan.Image = Utilities.ImageHandler.BitmapImageFromByteArray(khu.Hinh);
            }
        }
        private void uCFloorPlan1__OnEventFloorPlan(ControlLibrary.POSButtonTable tbl)
        {
            mTableButton = tbl;
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
            if (cboKhuVuc.SelectedIndex >= 0)
            {
                Data.BAN ban = new Data.BAN();
                ban.BanID = 0;
                ban.TenBan = "Ban Moi";
                ban.KhuID = (int)cboKhuVuc.SelectedValue;
                ban.LocationX = 0;
                ban.LocationY = 0;
                ban.Width = mTransit.ThamSo.BanChieuNgang;
                ban.Height = mTransit.ThamSo.BanChieuCao;
                ban.Hinh = null;
                uCFloorPlan1.addTable(ban);
            }
        }



        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            uCFloorPlan1.SaveChange();
        }

        private void txtTenBan_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mTableButton != null)
            {
                mTableButton._Ban.TenBan = txtTenBan.Text;
                mTableButton.TableDraw();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (mTableButton != null)
            {
                txtTenBan.Text = "";
                btnHinhDaiDien.DefaultImage();
                uCFloorPlan1.removeTable(mTableButton);
            }
        }

        private void btnHinhDaiDien__OnBitmapImageChanged(object sender)
        {
            if (mTableButton != null)
            {
                mTableButton._Ban.Hinh = Utilities.ImageHandler.ImageToByte(btnHinhDaiDien.ImageBitmap);
                mTableButton.Image = btnHinhDaiDien.ImageBitmap;
                //mTableButton.TableDraw();
            }
        }
        private void btnHinhSoDoBan__OnBitmapImageChanged(object sender)
        {
            uCFloorPlan1.LoadBackgroundImage(btnHinhSoDoBan.ImageBitmap);
            uCFloorPlan1._Khu.Hinh = Utilities.ImageHandler.ImageToByte(btnHinhSoDoBan.ImageBitmap);
        }
        private void btnHuyThayDoi_Click(object sender, RoutedEventArgs e)
        {
            uCFloorPlan1.LoadTable();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            uCFloorPlan1.Init(mTransit);
            LoadKhuVuc();
        }
    }
}
