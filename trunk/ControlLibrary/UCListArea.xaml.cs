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
    /// Interaction logic for UCListArea.xaml
    /// </summary>
    public partial class UCListArea : UserControl
    {
        public UCFloorPlan _UCFloorPlan { get; set; }
        private POSButtonTableArea mPOSButtonTableArea;
        private Data.Transit mTransit;        
        private Data.BOKhu mBOKhu;
        private List<Data.KHU> mListKhu;
        private static int NUM_OF_KHU = 5;
        private int mCurrentPage;
        public UCListArea()
        {
            InitializeComponent();
        }
        public void Init(Data.Transit transit)        
        {
            mTransit=transit;
            mBOKhu = new Data.BOKhu(mTransit);
        }
        private void LoadKhu()
        {
            int i = 0;
            bool isUpdow = mListKhu.Count > (NUM_OF_KHU + 2) ? true : false;
            int numOfItem=mListKhu.Count;
            if (isUpdow)
            {
                numOfItem = mListKhu.Count - mCurrentPage;
                numOfItem= numOfItem < NUM_OF_KHU ? numOfItem : NUM_OF_KHU;
            }            
            gridListArea.Children.Clear();
            if (isUpdow)
            {
                POSButtonTableArea btnUp = new POSButtonTableArea();
                btnUp.Content = "Lên";
                Grid.SetRow(btnUp, i);
                btnUp.Image = new BitmapImage(new Uri(@"/SystemImages;component/Images/Up.png", UriKind.Relative));
                gridListArea.Children.Add(btnUp);
                btnUp.Click += new RoutedEventHandler(btnUp_Click);
                i++;
            }
            for (int j=0; j < numOfItem; j++)
            {
                Data.KHU item = mListKhu[mCurrentPage+j];
                POSButtonTableArea btn = new POSButtonTableArea();
                btn.Content = item.TenKhu;
                Grid.SetRow(btn, i);
                btn.SetTableClicked((bool)item.MacDinhSoDoBan);                
                if ((bool)item.MacDinhSoDoBan)
                {
                    mPOSButtonTableArea = btn;
                    if (_UCFloorPlan != null)
                    {
                        _UCFloorPlan.LoadTable(item);
                    }
                }
                btn._Khu = item;
                btn.Margin = new Thickness(5, 5, 5, 5);
                btn.Image = Utilities.ImageHandler.BitmapImageFromByteArray(item.Hinh);
                btn.Click += new RoutedEventHandler(btn_Click);
                gridListArea.Children.Add(btn);
                i++;
            }
            if (isUpdow)
            {
                POSButtonTableArea btnDow = new POSButtonTableArea();
                btnDow.Content = "Xuống";
                Grid.SetRow(btnDow, 6);
                btnDow.Image = new BitmapImage(new Uri(@"/SystemImages;component/Images/Down.png", UriKind.Relative));
                btnDow.Click += new RoutedEventHandler(btnDow_Click);
                gridListArea.Children.Add(btnDow);
            }
        }

        void btnDow_Click(object sender, RoutedEventArgs e)
        {
            if ((mCurrentPage+NUM_OF_KHU)<mListKhu.Count)
            {
                mCurrentPage += NUM_OF_KHU;
                LoadKhu();
            }
        }

        void btnUp_Click(object sender, RoutedEventArgs e)
        {
            if ((mCurrentPage-NUM_OF_KHU)>=0)
            {
                mCurrentPage -= NUM_OF_KHU;
                LoadKhu();
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {            
            try
            {                
                mListKhu = Data.BOKhu.GetAllVisual(mTransit).ToList();
                mCurrentPage = 0;
                LoadKhu();
            }
            catch (Exception)
            {                            
            }
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            if (mPOSButtonTableArea!=null)
            {
                mPOSButtonTableArea._Khu.MacDinhSoDoBan = false;
                mPOSButtonTableArea.SetTableClicked(false);
            }
            POSButtonTableArea btn = (POSButtonTableArea)sender;
            mPOSButtonTableArea = btn;
            btn.SetTableClicked(true);
            if (_UCFloorPlan!=null)
            {
                _UCFloorPlan.LoadTable(btn._Khu);              
            }
        }
    }
}
