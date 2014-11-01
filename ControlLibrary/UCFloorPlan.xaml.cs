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
    /// Interaction logic for UCFloorPlan.xaml
    /// </summary>
    public partial class UCFloorPlan : UserControl
    {
        public delegate void EventFloorPlan(object ob);
        public event EventFloorPlan _OnEventFloorPlan;
        public bool _IsEdit { get; set; }        
        private Data.Transit mTransit;
        public UCFloorPlan()
        {            
            InitializeComponent();
        }
        public void Init(Data.Transit tran)
        {
            mTransit = tran;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {            
        }
        public void LoadTable(Data.KHU khu)
        {
            var listBan = Data.BOBan.GetTablePerArea(mTransit,khu);
            gridFloorPlan.Children.Clear();
            foreach (var ban in listBan)
            {
                POSButtonTable tbl = new POSButtonTable();
                tbl.IsEnabled = true;
                double x = this.RenderSize.Width * (double)ban.LocationX;
                double y = this.RenderSize.Height * (double)ban.LocationY;
                tbl.Width = this.RenderSize.Width * (double)ban.Width;
                tbl.Height = this.RenderSize.Height * (double)ban.Height;
                tbl.Margin = new Thickness(
                    x,
                    y,
                    this.RenderSize.Width - tbl.Width - x,
                    this.RenderSize.Height - tbl.Height - y
                );
                tbl.Content = ban.TenBan;
                tbl._Ban = ban;
                tbl._IsEdit = _IsEdit;
                tbl._UserControlParent = this;
                tbl.Click += new RoutedEventHandler(tbl_Click);                                
                if (ban.Hinh != null && ban.Hinh.Length > 0)
                {
                    tbl.Image = Utilities.ImageHandler.BitmapImageFromByteArray(ban.Hinh);
                }
                else
                {
                    var uriSource = new Uri(@"/ControlLibrary;component/Images/NoImages.jpg", UriKind.Relative);
                    tbl.Image = new BitmapImage(uriSource);
                }                
                //tbl.setText(ban.TenBan);
                //tbl._Ban = ban;                
                gridFloorPlan.Children.Add(tbl);                
            }
        }        

        void tbl_Click(object sender, RoutedEventArgs e)
        {
            POSButtonTable tbl = (POSButtonTable)sender;
            _OnEventFloorPlan(tbl);
        }
    }
}
