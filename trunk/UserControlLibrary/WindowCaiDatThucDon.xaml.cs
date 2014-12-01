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
    /// Interaction logic for WindowCaiDatThucDon.xaml
    /// </summary>
    public partial class WindowCaiDatThucDon : Window
    {
        Data.Transit mTransit = null;
        public WindowCaiDatThucDon(Data.Transit transit)
        {
            InitializeComponent();
            BOCaiDatThucDon = new Data.BOCaiDatThucDon(transit);
            mTransit = transit;
            btnHinhLoaiNhomNuocUong.SetTransit(mTransit);
            btnHinhLoaiNhomTatCa.SetTransit(mTransit);
            btnHinhLoaiNhomThucAn.SetTransit(mTransit);
            btnHinhMon.SetTransit(mTransit);
            btnHinhNhom.SetTransit(mTransit);
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (txtLoaiNhomTextFontSize.Text != "")
                _Item.LoaiNhomTextFontSize = Convert.ToDouble(txtLoaiNhomTextFontSize.Text);
            if (txtNhomTextFontSize.Text != "")
                _Item.NhomTextFontSize = Convert.ToDouble(txtNhomTextFontSize.Text);
            if (txtMonTextFontSize.Text != "")
                _Item.MonTextFontSize = Convert.ToDouble(txtMonTextFontSize.Text);

            _Item.LoaiNhomTextFontStyle = (int)cbbLoaiNhomTextFontStyle.SelectedValue;
            _Item.NhomTextFontStyle = (int)cbbNhomTextFontStyle.SelectedValue;
            _Item.MonTextFontStyle = (int)cbbMonTextFontStyle.SelectedValue;

            _Item.LoaiNhomTextFontWeights = (int)cbbLoaiNhomTextFontWeights.SelectedValue;
            _Item.NhomTextFontWeights = (int)cbbNhomTextFontWeights.SelectedValue;
            _Item.MonTextFontWeights = (int)cbbMonTextFontWeights.SelectedValue;

            if (btnHinhNhom.ImageBitmap != null)
            {
                BitmapFrame img = Utilities.ImageHandler.CreateResizedImage(btnHinhNhom.ImageBitmap, 120, 90, 0);
                _Item.NhomImages = Utilities.ImageHandler.ImageToByte(img);
            }
            if (btnHinhMon.ImageBitmap != null)
            {
                BitmapFrame img = Utilities.ImageHandler.CreateResizedImage(btnHinhMon.ImageBitmap, 120, 90, 0);
                _Item.MonImages = Utilities.ImageHandler.ImageToByte(img);
            }
            if (btnHinhLoaiNhomTatCa.ImageBitmap != null)
            {
                BitmapFrame img = Utilities.ImageHandler.CreateResizedImage(btnHinhLoaiNhomTatCa.ImageBitmap, 120, 90, 0);
                _Item.LoaiNhomThucTatCaImages = Utilities.ImageHandler.ImageToByte(img);
            }
            if (btnHinhLoaiNhomThucAn.ImageBitmap != null)
            {
                BitmapFrame img = Utilities.ImageHandler.CreateResizedImage(btnHinhLoaiNhomThucAn.ImageBitmap, 120, 90, 0);
                _Item.LoaiNhomThucAnImages = Utilities.ImageHandler.ImageToByte(img);
            }
            if (btnHinhLoaiNhomNuocUong.ImageBitmap != null)
            {
                BitmapFrame img = Utilities.ImageHandler.CreateResizedImage(btnHinhLoaiNhomNuocUong.ImageBitmap, 120, 90, 0);
                _Item.LoaiNhomNuocImages = Utilities.ImageHandler.ImageToByte(img);
            }

            BOCaiDatThucDon.CapNhat(_Item, mTransit);
            DialogResult = true;
        }

        private Data.CAIDATTHUCDON _Item = null;
        private Data.BOCaiDatThucDon BOCaiDatThucDon = null;

        private void LoadCombobox(ComboBox cbb, List<Data.SomeEnum> lsArray)
        {
            cbb.ItemsSource = lsArray;
            if (cbb.Items.Count > 0)
                cbb.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCombobox(cbbLoaiNhomTextFontStyle, Data.SomeEnum.GetFontStyles());
            LoadCombobox(cbbNhomTextFontStyle, Data.SomeEnum.GetFontStyles());
            LoadCombobox(cbbMonTextFontStyle, Data.SomeEnum.GetFontStyles());

            LoadCombobox(cbbLoaiNhomTextFontWeights, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbNhomTextFontWeights, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbMonTextFontWeights, Data.SomeEnum.GetFontWeights());

            _Item = BOCaiDatThucDon.GetAll(mTransit);
            if (_Item != null)
            {
                txtLoaiNhomTextFontSize.Text = _Item.LoaiNhomTextFontSize.ToString();
                txtNhomTextFontSize.Text = _Item.NhomTextFontSize.ToString();
                txtMonTextFontSize.Text = _Item.MonTextFontSize.ToString();

                cbbLoaiNhomTextFontStyle.SelectedValue = (int)_Item.LoaiNhomTextFontStyle;
                cbbNhomTextFontStyle.SelectedValue = (int)_Item.NhomTextFontStyle;
                cbbMonTextFontStyle.SelectedValue = (int)_Item.MonTextFontStyle;


                cbbLoaiNhomTextFontWeights.SelectedValue = (int)_Item.LoaiNhomTextFontWeights;
                cbbNhomTextFontWeights.SelectedValue = (int)_Item.NhomTextFontWeights;
                cbbMonTextFontWeights.SelectedValue = (int)_Item.MonTextFontWeights;

                if (_Item.NhomImages != null && _Item.NhomImages.Length > 0)
                {
                    btnHinhNhom.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Item.NhomImages);
                }

                if (_Item.MonImages != null && _Item.MonImages.Length > 0)
                {
                    btnHinhMon.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Item.MonImages);
                }

                if (_Item.LoaiNhomThucTatCaImages != null && _Item.LoaiNhomThucTatCaImages.Length > 0)
                {
                    btnHinhLoaiNhomTatCa.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Item.LoaiNhomThucTatCaImages);
                }

                if (_Item.LoaiNhomThucAnImages != null && _Item.LoaiNhomThucAnImages.Length > 0)
                {
                    btnHinhLoaiNhomThucAn.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Item.LoaiNhomThucAnImages);
                }

                if (_Item.LoaiNhomNuocImages != null && _Item.LoaiNhomNuocImages.Length > 0)
                {
                    btnHinhLoaiNhomNuocUong.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Item.LoaiNhomNuocImages);
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnLuu_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                btnHuy_Click(null, null);
                return;
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
