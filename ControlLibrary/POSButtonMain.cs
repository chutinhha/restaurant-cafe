﻿using System;
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
    ///     <MyNamespace:POSButtonMain/>
    ///
    /// </summary>
    public class POSButtonMain : Button
    {
        static POSButtonMain()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(POSButtonMain), new FrameworkPropertyMetadata(typeof(POSButtonMain)));
        }

        #region Properties

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(POSButtonMain), new PropertyMetadata(null));

        public string TextNho
        {
            get { return (string)GetValue(TextNhoProperty); }
            set { SetValue(TextNhoProperty, value); }
        }

        public static readonly DependencyProperty TextNhoProperty =
            DependencyProperty.Register("TextNho", typeof(string), typeof(POSButtonMain), new PropertyMetadata(null));

        public string TextTo
        {
            get { return (string)GetValue(TextToProperty); }
            set { SetValue(TextToProperty, value); }
        }

        public static readonly DependencyProperty TextToProperty =
            DependencyProperty.Register("TextTo", typeof(string), typeof(POSButtonMain), new PropertyMetadata(null));

        #endregion Properties
    }
}
