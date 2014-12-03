using System.Windows;
using System.Windows.Controls;
using System;

namespace ControlLibrary
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ControlLibrary"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ControlLibrary;assembly=ControlLibrary"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:POSTextBox/>
    ///
    /// </summary>
    public class POSTextBox : TextBox
    {
        private bool mIsLockText;
        private TypeKeyPad typeTextBox = TypeKeyPad.None;
        public int _MaxValue { get; set; }
        private WindowKeyPad _WindowKeyPad { get; set; }
        private WindowKeyboard _WindowKeyboard { get; set; }
        static POSTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(POSTextBox), new FrameworkPropertyMetadata(typeof(POSTextBox)));
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
                _UCMoneyKeyPad._TextBox = this;
            }
            switch (_TypeTextBox)
            {
                case TypeKeyPad.None:
                    break;

                case TypeKeyPad.Number:
                case TypeKeyPad.Decimal:
                    if (_UCKeyPad != null)
                    {
                        _UCKeyPad._TextBox = this;
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
        protected override void OnPreviewMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            this.SelectAll();
            this.Focus();
            base.OnPreviewMouseUp(e);
        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!mIsLockText)
            {
                if (typeTextBox==TypeKeyPad.Decimal)
                {
                    mIsLockText = true;
                    this.Text = Utilities.MoneyFormat.ConvertToString(this.Text);
                    mIsLockText = false;
                }
                base.OnTextChanged(e);
            }
        }

        protected override void OnPreviewMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            IniForcus();
            this.SelectAll();
            base.OnPreviewMouseDown(e);
        }

        protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
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
                    int data = Utilities.MoneyFormat.ConvertToInt(this.Text + e.Text);
                    if ((data < 0 || data > _MaxValue)&&_MaxValue>0)
                    {
                        e.Handled = true;
                    }
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