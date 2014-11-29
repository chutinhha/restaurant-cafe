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
    /// Interaction logic for WindowCaiDatMayInHoaDon.xaml
    /// </summary>
    public partial class WindowCaiDatMayInHoaDon : Window
    {
        Data.Transit mTransit = null;
        public WindowCaiDatMayInHoaDon(Data.Transit transit)
        {
            InitializeComponent();
            BOCaiDatMayInHoaDon = new Data.BOCaiDatMayInHoaDon(transit);
            mTransit = transit;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (txtHeaderTextFontSize1.Text != "")
                _Item.HeaderTextFontSize1 = Convert.ToDouble(txtHeaderTextFontSize3.Text);
            if (txtHeaderTextFontSize2.Text != "")
                _Item.HeaderTextFontSize2 = Convert.ToDouble(txtHeaderTextFontSize3.Text);
            if (txtHeaderTextFontSize3.Text != "")
                _Item.HeaderTextFontSize2 = Convert.ToDouble(txtHeaderTextFontSize3.Text);
            if (txtHeaderTextFontSize3.Text != "")
                _Item.HeaderTextFontSize2 = Convert.ToDouble(txtHeaderTextFontSize3.Text);

            _Item.HeaderTextFontStyle1 = (int)cbbHeaderTextFontStyle1.SelectedValue;
            _Item.HeaderTextFontStyle2 = (int)cbbHeaderTextFontStyle2.SelectedValue;
            _Item.HeaderTextFontStyle3 = (int)cbbHeaderTextFontStyle3.SelectedValue;
            _Item.HeaderTextFontStyle4 = (int)cbbHeaderTextFontStyle4.SelectedValue;

            _Item.HeaderTextFontWeights1 = (int)cbbHeaderTextFontWeights1.SelectedValue;
            _Item.HeaderTextFontWeights2 = (int)cbbHeaderTextFontWeights2.SelectedValue;
            _Item.HeaderTextFontWeights3 = (int)cbbHeaderTextFontWeights3.SelectedValue;
            _Item.HeaderTextFontWeights4 = (int)cbbHeaderTextFontWeights4.SelectedValue;

            if (txtFooterTextFontSize1.Text != "")
                _Item.FooterTextFontSize1 = Convert.ToDouble(txtFooterTextFontSize3.Text);
            if (txtFooterTextFontSize2.Text != "")
                _Item.FooterTextFontSize2 = Convert.ToDouble(txtFooterTextFontSize3.Text);
            if (txtFooterTextFontSize3.Text != "")
                _Item.FooterTextFontSize2 = Convert.ToDouble(txtFooterTextFontSize3.Text);
            if (txtFooterTextFontSize3.Text != "")
                _Item.FooterTextFontSize2 = Convert.ToDouble(txtFooterTextFontSize3.Text);

            _Item.FooterTextFontStyle1 = (int)cbbFooterTextFontStyle1.SelectedValue;
            _Item.FooterTextFontStyle2 = (int)cbbFooterTextFontStyle2.SelectedValue;
            _Item.FooterTextFontStyle3 = (int)cbbFooterTextFontStyle3.SelectedValue;
            _Item.FooterTextFontStyle4 = (int)cbbFooterTextFontStyle4.SelectedValue;

            _Item.FooterTextFontWeights1 = (int)cbbFooterTextFontWeights1.SelectedValue;
            _Item.FooterTextFontWeights2 = (int)cbbFooterTextFontWeights2.SelectedValue;
            _Item.FooterTextFontWeights3 = (int)cbbFooterTextFontWeights3.SelectedValue;
            _Item.FooterTextFontWeights4 = (int)cbbFooterTextFontWeights4.SelectedValue;

            if (txtSumanyFontSize.Text != "")
                _Item.SumanyFontSize = Convert.ToDouble(txtSumanyFontSize.Text);
            _Item.SumanyFontStyle = (int)cbbSumanyFontStyle.SelectedValue;
            _Item.SumanyFontWeights = (int)cbbSumanyFontWeights.SelectedValue;

            if (txtSumanyBigFontSize.Text != "")
                _Item.SumanyFontSizeBig = Convert.ToDouble(txtSumanyBigFontSize.Text);
            _Item.SumanyFontStyleBig = (int)cbbSumanyBigFontStyle.SelectedValue;
            _Item.SumanyFontWeightsBig = (int)cbbSumanyBigFontWeights.SelectedValue;

            if (txtTitleFontSize.Text != "")
                _Item.TitleTextFontSize = Convert.ToDouble(txtTitleFontSize.Text);
            _Item.TitleTextFontStyle = (int)cbbTitleFontStyle.SelectedValue;
            _Item.TitleTextFontWeights = (int)cbbTitleFontWeights.SelectedValue;

            if (txtInfoFontSize.Text != "")
                _Item.InfoTextFontSize = Convert.ToDouble(txtInfoFontSize.Text);
            _Item.InfoTextFontStyle = (int)cbbItemFontStyle.SelectedValue;
            _Item.InfoTextFontWeights = (int)cbbInfoFontWeights.SelectedValue;


            if (txtItemFontSize.Text != "")
                _Item.ItemFontSize = Convert.ToDouble(txtItemFontSize.Text);
            _Item.ItemTextFontStyle = (int)cbbItemFontStyle.SelectedValue;
            _Item.ItemTextFontWeights = (int)cbbItemFontWeights.SelectedValue;


            BOCaiDatMayInHoaDon.CapNhat(_Item, mTransit);
            DialogResult = true;
        }

        private Data.CAIDATMAYINHOADON _Item = null;
        private Data.BOCaiDatMayInHoaDon BOCaiDatMayInHoaDon = null;

        private void LoadCombobox(ComboBox cbb, List<Data.SomeEnum> lsArray)
        {
            cbb.ItemsSource = lsArray;
            if (cbb.Items.Count > 0)
                cbb.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCombobox(cbbHeaderTextFontStyle1, Data.SomeEnum.GetFontStyles());
            LoadCombobox(cbbHeaderTextFontStyle2, Data.SomeEnum.GetFontStyles());
            LoadCombobox(cbbHeaderTextFontStyle3, Data.SomeEnum.GetFontStyles());
            LoadCombobox(cbbHeaderTextFontStyle4, Data.SomeEnum.GetFontStyles());

            LoadCombobox(cbbHeaderTextFontWeights1, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbHeaderTextFontWeights2, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbHeaderTextFontWeights3, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbHeaderTextFontWeights4, Data.SomeEnum.GetFontWeights());

            LoadCombobox(cbbFooterTextFontStyle1, Data.SomeEnum.GetFontStyles());
            LoadCombobox(cbbFooterTextFontStyle2, Data.SomeEnum.GetFontStyles());
            LoadCombobox(cbbFooterTextFontStyle3, Data.SomeEnum.GetFontStyles());
            LoadCombobox(cbbFooterTextFontStyle4, Data.SomeEnum.GetFontStyles());

            LoadCombobox(cbbFooterTextFontWeights1, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbFooterTextFontWeights2, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbFooterTextFontWeights3, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbFooterTextFontWeights4, Data.SomeEnum.GetFontWeights());

            LoadCombobox(cbbSumanyFontStyle, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbSumanyBigFontStyle, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbTitleFontStyle, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbInfoFontStyle, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbItemFontStyle, Data.SomeEnum.GetFontWeights());

            LoadCombobox(cbbSumanyFontWeights, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbSumanyBigFontWeights, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbTitleFontWeights, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbInfoFontWeights, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbItemFontWeights, Data.SomeEnum.GetFontWeights());

            _Item = BOCaiDatMayInHoaDon.GetAll(mTransit);
            if (_Item != null)
            {
                txtHeaderTextFontSize1.Text = _Item.HeaderTextFontSize1.ToString();
                txtHeaderTextFontSize2.Text = _Item.HeaderTextFontSize2.ToString();
                txtHeaderTextFontSize3.Text = _Item.HeaderTextFontSize3.ToString();
                txtHeaderTextFontSize4.Text = _Item.HeaderTextFontSize4.ToString();

                cbbHeaderTextFontStyle1.SelectedValue = _Item.HeaderTextFontStyle1;
                cbbHeaderTextFontStyle2.SelectedValue = _Item.HeaderTextFontStyle2;
                cbbHeaderTextFontStyle3.SelectedValue = _Item.HeaderTextFontStyle3;
                cbbHeaderTextFontStyle4.SelectedValue = _Item.HeaderTextFontStyle4;

                cbbHeaderTextFontWeights1.SelectedValue = _Item.HeaderTextFontWeights1;
                cbbHeaderTextFontWeights2.SelectedValue = _Item.HeaderTextFontWeights2;
                cbbHeaderTextFontWeights3.SelectedValue = _Item.HeaderTextFontWeights3;
                cbbHeaderTextFontWeights4.SelectedValue = _Item.HeaderTextFontWeights4;

                txtFooterTextFontSize1.Text = _Item.FooterTextFontSize1.ToString();
                txtFooterTextFontSize2.Text = _Item.FooterTextFontSize2.ToString();
                txtFooterTextFontSize3.Text = _Item.FooterTextFontSize3.ToString();
                txtFooterTextFontSize4.Text = _Item.FooterTextFontSize4.ToString();

                cbbFooterTextFontStyle1.SelectedValue = _Item.FooterTextFontStyle1;
                cbbFooterTextFontStyle2.SelectedValue = _Item.FooterTextFontStyle2;
                cbbFooterTextFontStyle3.SelectedValue = _Item.FooterTextFontStyle3;
                cbbFooterTextFontStyle4.SelectedValue = _Item.FooterTextFontStyle4;

                cbbFooterTextFontWeights1.SelectedValue = _Item.FooterTextFontWeights1;
                cbbFooterTextFontWeights2.SelectedValue = _Item.FooterTextFontWeights2;
                cbbFooterTextFontWeights3.SelectedValue = _Item.FooterTextFontWeights3;
                cbbFooterTextFontWeights4.SelectedValue = _Item.FooterTextFontWeights4;

                txtSumanyFontSize.Text = _Item.SumanyFontSize.ToString();
                cbbSumanyFontStyle.SelectedValue = _Item.SumanyFontStyle;
                cbbSumanyFontWeights.SelectedValue = _Item.SumanyFontWeights;

                txtSumanyBigFontSize.Text = _Item.SumanyFontSizeBig.ToString();
                cbbSumanyBigFontStyle.SelectedValue = _Item.SumanyFontStyleBig;
                cbbSumanyBigFontWeights.SelectedValue = _Item.SumanyFontWeightsBig;

                txtTitleFontSize.Text = _Item.TitleTextFontSize.ToString();
                cbbTitleFontStyle.SelectedValue = _Item.TitleTextFontStyle;
                cbbTitleFontWeights.SelectedValue = _Item.TitleTextFontWeights;

                txtInfoFontSize.Text = _Item.InfoTextFontSize.ToString();
                cbbInfoFontStyle.SelectedValue = _Item.InfoTextFontStyle;
                cbbInfoFontWeights.SelectedValue = _Item.InfoTextFontWeights;

                txtItemFontSize.Text = _Item.ItemFontSize.ToString();
                cbbItemFontStyle.SelectedValue = _Item.ItemTextFontStyle;
                cbbItemFontWeights.SelectedValue = _Item.ItemTextFontWeights;
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
