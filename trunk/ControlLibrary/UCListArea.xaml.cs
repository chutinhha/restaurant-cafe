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
        public UCListArea()
        {
            InitializeComponent();
        }
        public void Init(Data.Transit transit)        
        {
            mTransit=transit;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {            
            try
            {
                var list = Data.BOKhu.GetAllVisual(mTransit);
                int count = 0;
                foreach (var item in list)
                {
                    POSButtonTableArea btn = new POSButtonTableArea();
                    btn.Content = item.TenKhu;                    
                    Grid.SetRow(btn, count);
                    btn.SetTableClicked((bool)item.MacDinhSoDoBan);
                    //Grid.SetColumn(btn, 0);                    
                    if ((bool)item.MacDinhSoDoBan)
                    {
                        mPOSButtonTableArea = btn;                        
                        if (_UCFloorPlan!=null)
                        {
                            _UCFloorPlan.LoadTable(item);
                        }
                    }                    
                    btn._Khu = item;
                    btn.Click += new RoutedEventHandler(btn_Click);
                    gridListArea.Children.Add(btn);                    
                    count++;
                }
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
