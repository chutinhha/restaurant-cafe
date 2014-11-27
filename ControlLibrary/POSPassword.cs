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
    ///     <MyNamespace:POSPassword/>
    ///
    /// </summary>
    public class POSPassword : TextBox
    {
        private TypeKeyPad typeTextBox = TypeKeyPad.None;

        private WindowKeyPad _WindowKeyPad { get; set; }

        private WindowKeyboard _WindowKeyboard { get; set; }

        //static POSPassword()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(POSPassword), new FrameworkPropertyMetadata(typeof(POSPassword)));
        //}

        public TypeKeyPad _TypeTextBox
        {
            get { return typeTextBox; }
            set { typeTextBox = value; }
        }

        public UCKeyPad _UCKeyPad { get; set; }

        private void IniForcus()
        {
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

        protected override void OnPreviewMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            IniForcus();
            this.SelectAll();
            base.OnPreviewMouseDown(e);
        }


    }
}
