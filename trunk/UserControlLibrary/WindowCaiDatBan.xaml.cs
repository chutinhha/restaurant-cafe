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
    /// Interaction logic for WindowCaiDatBan.xaml
    /// </summary>
    public partial class WindowCaiDatBan : Window
    {
        Data.Transit mTransit = null;
        public WindowCaiDatBan(Data.Transit transit)
        {
            InitializeComponent();
            BOCaiDatBan = new Data.BOCaiDatBan(transit);
            mTransit = transit;
            btnHinh.SetTransit(transit);
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (txtFontSize.Text != "")
                _Item.TableFontSize = Convert.ToDouble(txtFontSize.Text);
            _Item.TableFontStyle = (int)cbbFontStyles.SelectedValue;
            _Item.TableFontWeights = (int)cbbFontWeight.SelectedValue;

            if (btnHinh.ImageBitmap != null)
            {
                BitmapFrame img = Utilities.ImageHandler.CreateResizedImage(btnHinh.ImageBitmap, 100, 100, 0);
                _Item.TableImage = Utilities.ImageHandler.ImageToByte(img);
            }

            BOCaiDatBan.CapNhat(_Item, mTransit);
            DialogResult = true;
        }

        private Data.CAIDATBAN _Item = null;
        private Data.BOCaiDatBan BOCaiDatBan = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbbFontStyles.ItemsSource = Data.SomeEnum.GetFontStyles();
            if (cbbFontStyles.Items.Count > 0)
                cbbFontStyles.SelectedIndex = 0;

            cbbFontWeight.ItemsSource = Data.SomeEnum.GetFontWeights();
            if (cbbFontStyles.Items.Count > 0)
                cbbFontStyles.SelectedIndex = 0;

            _Item = BOCaiDatBan.GetAll(mTransit);
            if (_Item != null)
            {
                txtFontSize.Text = _Item.TableFontSize.ToString();
                cbbFontStyles.SelectedValue = _Item.TableFontStyle;
                cbbFontWeight.SelectedValue = _Item.TableFontWeights;

                if (_Item.TableImage != null && _Item.TableImage.Length > 0)
                {
                    btnHinh.Image = Utilities.ImageHandler.BitmapImageFromByteArray(_Item.TableImage);
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
