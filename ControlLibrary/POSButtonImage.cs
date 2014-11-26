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
using System.IO;

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
    ///     <MyNamespace:POSButtonImage/>
    ///
    /// </summary>
    public class POSButtonImage : Button
    {
        static POSButtonImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(POSButtonImage), new FrameworkPropertyMetadata(typeof(POSButtonImage)));
        }
        public delegate void EventBitmapImage(object sender);
        public event EventBitmapImage _OnBitmapImageChanged;
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        public BitmapImage ImageBitmap { get; set; }
        public void DefaultImage()
        {            
            var uriSource = new Uri(@"/ControlLibrary;component/Images/AddNewImage.png", UriKind.Relative);
            this.Image = new BitmapImage(uriSource);
            this.ImageBitmap = null;
        }
        public override void OnApplyTemplate()
        {            
            base.OnApplyTemplate();
            if (this.Image == null)
            {
                var uriSource = new Uri(@"/ControlLibrary;component/Images/AddNewImage.png", UriKind.Relative);
                this.Image = new BitmapImage(uriSource);
            }
        }                
        protected override void OnClick()
        {
            Stream checkStream = null;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "All Image Files | *.*";
            if ((bool)openFileDialog.ShowDialog())
            {
                if ((checkStream = openFileDialog.OpenFile()) != null)
                {

                    Stream fs = File.OpenRead(openFileDialog.FileName);
                    BitmapImage mBitmapImage = new BitmapImage();
                    mBitmapImage.BeginInit();
                    mBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    mBitmapImage.StreamSource = fs;
                    mBitmapImage.EndInit();
                    //this.ImageBitmap = Utilities.ImageHandler.BitmapImageCopy(mBitmapImage);                    
                    //this.ImageBitmap = Utilities.ImageHandler.ImageToByte(mBitmapImage);
                    this.Image = mBitmapImage;
                    this.ImageBitmap = mBitmapImage;                    
                    if (_OnBitmapImageChanged!=null)
                    {
                        _OnBitmapImageChanged(this);
                    }
                }
            }
            base.OnClick();
        }
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(POSButtonImage), new PropertyMetadata(null));
    }
}
