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
    ///     <MyNamespace:POSButtonPrice/>
    ///
    /// </summary>
    public class POSButtonPrice : Button
    {
        public Data.BOMenuGia _MenuGia { get; set; }
        static POSButtonPrice()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(POSButtonPrice), new FrameworkPropertyMetadata(typeof(POSButtonPrice)));
        }

        public double FontSizePrice
        {
            get { return (double)GetValue(FontSizePriceProperty);}
            set { SetValue(FontSizePriceProperty, value); }
        }

        public static readonly DependencyProperty FontSizePriceProperty =
            DependencyProperty.Register("FontSizePrice", typeof(double), typeof(POSButtonPrice), new PropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(POSButtonPrice), new PropertyMetadata(null));

        public string TextPrice
        {
            get { return (string)GetValue(TextPriceProperty); }
            set { SetValue(TextPriceProperty, value); }
        }

        public static readonly DependencyProperty TextPriceProperty =
            DependencyProperty.Register("TextPrice", typeof(string), typeof(POSButtonPrice), new PropertyMetadata(null));
    }
}
