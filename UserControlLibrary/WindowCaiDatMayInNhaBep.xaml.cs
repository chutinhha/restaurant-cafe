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
    /// Interaction logic for WindowCaiDatMayInNhaBep.xaml
    /// </summary>
    public partial class WindowCaiDatMayInNhaBep : Window
    {
        Data.Transit mTransit = null;
        public WindowCaiDatMayInNhaBep(Data.Transit transit)
        {
            InitializeComponent();
            BOCaiDatMayInBep = new Data.BOCaiDatMayInBep(transit);
            mTransit = transit;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitleTextFontSize.Text != "")
                _Item.TitleTextFontSize = Convert.ToDouble(txtTitleTextFontSize.Text);
            if (txtInfoTextFontSize.Text != "")
                _Item.InfoTextFontSize = Convert.ToDouble(txtInfoTextFontSize.Text);
            if (txtItemTextFontSize.Text != "")
                _Item.ItemTextFontSize = Convert.ToDouble(txtItemTextFontSize.Text);
            if (txtSumTextFontSize.Text != "")
                _Item.SumTextFontSize = Convert.ToDouble(txtSumTextFontSize.Text);

            _Item.TitleTextFontStyle = (int)cbbTitleTextFontStyle.SelectedValue;
            _Item.InfoTextFontStyle = (int)cbbInfoTextFontStyle.SelectedValue;
            _Item.ItemTextFontStyle = (int)cbbItemTextFontStyle.SelectedValue;
            _Item.SumTextFontStyle = (int)cbbSumTextFontStyle.SelectedValue;

            _Item.TitleTextFontWeights = (int)cbbTitleTextFontWeights.SelectedValue;
            _Item.InfoTextFontWeights = (int)cbbInfoTextFontWeights.SelectedValue;
            _Item.ItemTextFontWeights = (int)cbbItemTextFontWeights.SelectedValue;
            _Item.SumTextFontWeights = (int)cbbSumTextFontWeights.SelectedValue;

            BOCaiDatMayInBep.CapNhat(_Item, mTransit);
            DialogResult = true;
        }

        private Data.CAIDATMAYINBEP _Item = null;
        private Data.BOCaiDatMayInBep BOCaiDatMayInBep = null;

        private void LoadCombobox(ComboBox cbb, List<Data.SomeEnum> lsArray)
        {
            cbb.ItemsSource = lsArray;
            if (cbb.Items.Count > 0)
                cbb.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCombobox(cbbTitleTextFontStyle, Data.SomeEnum.GetFontStyles());
            LoadCombobox(cbbInfoTextFontStyle, Data.SomeEnum.GetFontStyles());
            LoadCombobox(cbbItemTextFontStyle, Data.SomeEnum.GetFontStyles());
            LoadCombobox(cbbSumTextFontStyle, Data.SomeEnum.GetFontStyles());

            LoadCombobox(cbbTitleTextFontWeights, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbInfoTextFontWeights, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbItemTextFontWeights, Data.SomeEnum.GetFontWeights());
            LoadCombobox(cbbSumTextFontWeights, Data.SomeEnum.GetFontWeights());

            _Item = BOCaiDatMayInBep.GetAll(mTransit);
            if (_Item != null)
            {
                txtTitleTextFontSize.Text = _Item.TitleTextFontSize.ToString();
                txtInfoTextFontSize.Text = _Item.InfoTextFontSize.ToString();
                txtItemTextFontSize.Text = _Item.ItemTextFontSize.ToString();
                txtSumTextFontSize.Text = _Item.SumTextFontSize.ToString();

                cbbTitleTextFontStyle.SelectedValue = (int)_Item.TitleTextFontStyle;
                cbbInfoTextFontStyle.SelectedValue = (int)_Item.InfoTextFontStyle;
                cbbItemTextFontStyle.SelectedValue = (int)_Item.ItemTextFontStyle;
                cbbSumTextFontStyle.SelectedValue = (int)_Item.SumTextFontStyle;


                cbbTitleTextFontWeights.SelectedValue = (int)_Item.TitleTextFontWeights;
                cbbInfoTextFontWeights.SelectedValue = (int)_Item.InfoTextFontWeights;
                cbbItemTextFontWeights.SelectedValue = (int)_Item.ItemTextFontWeights;
                cbbSumTextFontWeights.SelectedValue = (int)_Item.SumTextFontWeights;
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
