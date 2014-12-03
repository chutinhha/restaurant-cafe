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


namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCKeyPad : UserControl
    {
        public UCKeyPad()
        {
            _TextBox = null;
            InitializeComponent();
            _TypeKeyPad = TypeKeyPad.None;
        }
        public POSTextBox _TextBox { get; set; }
        public TypeKeyPad _TypeKeyPad { get; set; }
        public bool _Decimal { get; set; }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (_TextBox != null)
            {
                if (_TextBox.SelectedText.Length>0)
                {
                    _TextBox.Text = _TextBox.Text.Replace(_TextBox.SelectedText, "");
                }
                Button button = sender as Button;
                switch (button.CommandParameter.ToString())
                {
                    case "ESC":
                        break;

                    case "RETURN":
                        break;

                    case "BACK":
                        if (_TextBox.Text.Length > 0)
                            _TextBox.Text = _TextBox.Text.Remove(_TextBox.Text.Length - 1);
                        break;
                    case "DECIMAL":
                        _TextBox.Text += button.Content.ToString();
                        break;
                    default:
                        if (_TextBox._TypeTextBox==TypeKeyPad.Number)
                        {
                            int data = Utilities.MoneyFormat.ConvertToInt(_TextBox.Text + button.Content.ToString());
                            if ((data < 0 || data > _TextBox._MaxValue) && _TextBox._MaxValue > 0)
                            {
                                return;
                            }
                        }
                        _TextBox.Text += button.Content.ToString();
                        break;
                }
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Decimal == false)
            {
                gDecimal.Visibility = System.Windows.Visibility.Hidden;
                gNumpad0.SetValue(Grid.ColumnSpanProperty, 2);
            }
        }        
    }
}
