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
    ///     <MyNamespace:POSButtonTable/>
    ///
    /// </summary>
    public class POSButtonTable : Button
    {
        public Data.BAN _Ban { get; set; }
        private bool mIsMoseDown;
        private Point mPointMoseDown;
        private Thickness mThicknessMouseDown;
        public bool _IsEdit { get; set; }
        public UserControl _UserControlParent { get; set; }

        static POSButtonTable()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(POSButtonTable), new FrameworkPropertyMetadata(typeof(POSButtonTable)));            
        }
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (_IsEdit)
            {
                mIsMoseDown = true;
                if (_UserControlParent!=null)
                {
                    mPointMoseDown = e.GetPosition(_UserControlParent);
                    mThicknessMouseDown = this.Margin;
                }
            }
            base.OnPreviewMouseDown(e);
        }
        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if (_IsEdit)
            {
                mIsMoseDown = false;
            }
            base.OnPreviewMouseUp(e);
        }
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (_IsEdit)
            {
                if (mIsMoseDown)
                {
                    if (mPointMoseDown!=null && _UserControlParent!=null)
                    {
                        Point newPoint = e.GetPosition(_UserControlParent);
                        double dx = newPoint.X - mPointMoseDown.X;
                        double dy = newPoint.Y - mPointMoseDown.Y;
                        if (
                            (mThicknessMouseDown.Left + dx)>=_UserControlParent.Margin.Left&&
                            (mThicknessMouseDown.Top + dy)>=_UserControlParent.Margin.Top&&
                            (mThicknessMouseDown.Right - dx)>=_UserControlParent.Margin.Right&&
                            (mThicknessMouseDown.Bottom - dy)>=_UserControlParent.Margin.Bottom
                        )
                        {
                            this.Margin = new Thickness(mThicknessMouseDown.Left + dx, mThicknessMouseDown.Top + dy, mThicknessMouseDown.Right - dx, mThicknessMouseDown.Bottom - dy);                                                                
                        }                    
                    }
                }
            }
            base.OnPreviewMouseMove(e);
        }
        public void MoveToPoint(Point point)
        {            
        }
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(POSButtonTable), new PropertyMetadata(null));
    }
}
