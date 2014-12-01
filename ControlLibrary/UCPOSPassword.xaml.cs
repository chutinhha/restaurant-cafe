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
    /// Interaction logic for UCPOSPassword.xaml
    /// </summary>
    public partial class UCPOSPassword : UserControl
    {
        private bool mIsLockText;
        private POSTextBox mPOSTextBox;
        private TypeKeyPad typeTextBox = TypeKeyPad.None;
        private WindowKeyPad _WindowKeyPad { get; set; }
        private WindowKeyboard _WindowKeyboard { get; set; }
        public double _Height 
        {
            get { return this.Height; }
            set 
            {
                this.Height = value;
                passwordBox1.Height = value;
                passwordBox1.FontSize =0.8*value;
            }
        }
        public double _Width 
        {
            get { return this.Width; }
            set { this.Width = value; }
        }
        public string Text 
        {
            get { return passwordBox1.Password; }
            set { passwordBox1.Password = value; }
        }
        public TypeKeyPad _TypeTextBox
        {
            get { return typeTextBox; }
            set { typeTextBox = value; }
        }

        public UCKeyPad _UCKeyPad { get; set; }
        public UCMoneyKeyPad _UCMoneyKeyPad { get; set; }

        private void IniForcus()
        {
            if (_UCMoneyKeyPad != null)
            {
                _UCMoneyKeyPad._TextBox = mPOSTextBox;
            }
            switch (_TypeTextBox)
            {
                case TypeKeyPad.None:
                    break;

                case TypeKeyPad.Number:
                case TypeKeyPad.Decimal:
                    if (_UCKeyPad != null)
                    {
                        _UCKeyPad._TextBox = mPOSTextBox;
                        _UCKeyPad._TypeKeyPad = _TypeTextBox;
                    }
                    else
                    {
                        //Load Keypad len
                    }
                    break;

                case TypeKeyPad.Text:
                    //Load keyboard len
                    break;

                default:
                    break;
            }
        }
      
        public UCPOSPassword()
        {
            InitializeComponent();
            mPOSTextBox = new POSTextBox();
            mPOSTextBox.TextChanged += new TextChangedEventHandler(mPOSTextBox_TextChanged);
        }

        void mPOSTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!mIsLockText)
            {
                mIsLockText = true;
                passwordBox1.Password = mPOSTextBox.Text;
                mIsLockText = false;
            }
        }

        private void passwordBox1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!mIsLockText)
            {
                mIsLockText = true;
                mPOSTextBox.Text = passwordBox1.Password;
                mIsLockText = false;
            }
        }

        private void passwordBox1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            IniForcus();
            passwordBox1.SelectAll();
            base.OnPreviewMouseDown(e);
        }

        private void passwordBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            switch (typeTextBox)
            {
                case TypeKeyPad.None:
                    break;
                case TypeKeyPad.Number:
                    if (Char.IsNumber(e.Text, e.Text.Length - 1))
                        e.Handled = false;
                    else
                        e.Handled = true;
                    break;
                case TypeKeyPad.Decimal:
                    if (Char.IsDigit(e.Text, e.Text.Length - 1))
                        e.Handled = false;
                    else
                        e.Handled = true;
                    break;
                case TypeKeyPad.Text:
                    break;
                default:
                    break;
            }
        }        
    }
}
