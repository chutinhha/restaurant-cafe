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
        private bool mIsLockText;
        public UCSoDoBan(Data.Transit transit)
        {
            mTransit = transit;
            InitializeComponent();
            btnHinhDaiDien.SetTransit(transit);
            btnHinhSoDoBan.SetTransit(transit);
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
            if (mTableButton != null)
            {
                mTableButton._ButtonTableStatusColor = ControlLibrary.POSButtonTable.POSButtonTableStatusColor.None;
            }
            tbl._ButtonTableStatusColor = ControlLibrary.POSButtonTable.POSButtonTableStatusColor.Ordered;
            mTableButton = tbl;
            txtTenBan.Text = tbl._Ban.TenBan;
            mIsLockText = true;
            sliderNgang.Value = (int)(uCFloorPlan1._CAIDATBAN.TableWidth > 0 ? tbl._Ban.Width / uCFloorPlan1._CAIDATBAN.TableWidth * 100 : 0);
            sliderCao.Value = (int)(uCFloorPlan1._CAIDATBAN.TableHeight > 0 ? tbl._Ban.Height / uCFloorPlan1._CAIDATBAN.TableHeight * 100 : 0);
            mIsLockText = false;
            if (tbl._Ban.Hinh != null && tbl._Ban.Hinh.Length > 0)
            {
                btnHinhDaiDien.Image = Utilities.ImageHandler.BitmapImageFromByteArray(tbl._Ban.Hinh);
            }
            else
            {
                if (uCFloorPlan1._CAIDATBAN.TableImage != null && uCFloorPlan1._CAIDATBAN.TableImage.Length > 0)
                {
                    btnHinhDaiDien.Image = Utilities.ImageHandler.BitmapImageFromByteArray(uCFloorPlan1._CAIDATBAN.TableImage);
                }
                else
                {
                    var uriSource = new Uri(@"/ControlLibrary;component/Images/NoImages.jpg", UriKind.Relative);
                    btnHinhDaiDien.Image = new BitmapImage(uriSource);
                }
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
                ban.Width = uCFloorPlan1._CAIDATBAN.TableWidth;
                ban.Height = uCFloorPlan1._CAIDATBAN.TableHeight;
                ban.Visual = true;
                ban.Deleted = false;
                ban.Hinh = null;
                uCFloorPlan1.AddTable(ban);
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
                uCFloorPlan1.DrawTable(mTableButton);
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (mTableButton != null)
            {
                txtTenBan.Text = "";
                btnHinhDaiDien.DefaultImage();
                uCFloorPlan1.RemoveTable(mTableButton);
            }
        }

        private void btnXoaTatCa_Click(object sender, RoutedEventArgs e)
        {
            txtTenBan.Text = "";
            btnHinhDaiDien.DefaultImage();
            uCFloorPlan1.RemoveAllTable();
        }

        private void btnHinhDaiDien__OnBitmapImageChanged(object sender)
        {
            if (mTableButton != null)
            {
                BitmapFrame img = Utilities.ImageHandler.CreateResizedImage(btnHinhDaiDien.ImageBitmap, 100, 100, 0);
                mTableButton._Ban.Hinh = Utilities.ImageHandler.ImageToByte(img);
                mTableButton.Image = img;
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

        private void sliderNgang_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mIsLockText)
            {
                return;
            }
            if (mTableButton != null)
            {
                mTableButton._Ban.Width = (decimal)sliderNgang.Value * uCFloorPlan1._CAIDATBAN.TableWidth / 100;
                uCFloorPlan1.DrawTable(mTableButton);
            }
        }

        private void sliderCao_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mIsLockText)
            {
                return;
            }
            if (mTableButton != null)
            {
                mTableButton._Ban.Height = (decimal)sliderCao.Value * uCFloorPlan1._CAIDATBAN.TableHeight / 100;
                uCFloorPlan1.DrawTable(mTableButton);
            }
        }

        private void btnThemSoNhieu_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowSoDoBanThemSoNhieu win = new WindowSoDoBanThemSoNhieu(uCFloorPlan1);
            win.ShowDialog();
        }
    }
}
