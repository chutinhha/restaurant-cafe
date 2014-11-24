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
    /// Interaction logic for WindowCaiDatGiaoDienBanHang.xaml
    /// </summary>
    public partial class WindowCaiDatGiaoDienBanHang : Window
    {
        Data.Transit mTransit = null;
        Data.BOGiaoDienChucNangBanHang BOGiaoDienChucNangBanHang = null;
        public WindowCaiDatGiaoDienBanHang(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOGiaoDienChucNangBanHang = new Data.BOGiaoDienChucNangBanHang(transit);
        }
        List<Data.GIAODIENCHUCNANGBANHANG> lsArray = null;
        public void LoadGiaoDienBanHang()
        {
            lsArray = BOGiaoDienChucNangBanHang.GetAll(mTransit).ToList();
            foreach (var item in lsArray)
            {
                var myComboBox = (ComboBox)this.FindName("cbbChucNang" + item.ID);
                var myImages = (ControlLibrary.POSButtonImage)this.FindName("btnHinh" + item.ID);
                myComboBox.Tag = item;
                myImages.Tag = item;
                myComboBox.SelectedValue = item.ChucNangID;
                if (item.Hinh != null && item.Hinh.Length > 0)
                    myImages.Image = Utilities.ImageHandler.BitmapImageFromByteArray(item.Hinh);
            }

        }

        private void LoadCombobox()
        {
            List<Data.CHUCNANG> lsChucNang = Data.BOChucNang.GetAllNoTracking(mTransit).Where(s => s.NhomChucNangID == (int)Data.TypeChucNang.ChucNangChinh.BanHang).ToList();
            lsChucNang.Insert(0, new Data.CHUCNANG() { TenChucNang = "Không sử dụng", ChucNangID = 0 });
            foreach (var ctrl in gridContent.Children)
            {
                if (ctrl is ComboBox)
                {
                    ComboBox cbb = (ComboBox)ctrl;
                    cbb.ItemsSource = lsChucNang;
                    cbb.SelectedIndex = 0;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCombobox();
            LoadGiaoDienBanHang();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            foreach (var ctrl in gridContent.Children)
            {
                if (ctrl is ComboBox)
                {
                    ComboBox cbb = (ComboBox)ctrl;
                    Data.GIAODIENCHUCNANGBANHANG cn = (Data.GIAODIENCHUCNANGBANHANG)cbb.Tag;
                    cn.ChucNangID = (int)cbb.SelectedValue;
                }
                if (ctrl is ControlLibrary.POSButtonImage)
                {
                    ControlLibrary.POSButtonImage img = (ControlLibrary.POSButtonImage)ctrl;
                    Data.GIAODIENCHUCNANGBANHANG cn = (Data.GIAODIENCHUCNANGBANHANG)img.Tag;
                    if (img.ImageBitmap != null)
                    {
                        cn.Hinh = Utilities.ImageHandler.ImageToByte(img.ImageBitmap);
                    }
                }
                BOGiaoDienChucNangBanHang.CapNhat(lsArray, mTransit);
            }

            DialogResult = true;
        }
    }
}
