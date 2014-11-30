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
using System.Windows.Shapes;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowBanHangNhieuGia.xaml
    /// </summary>
    public partial class WindowBanHangNhieuGia : Window
    {
        public Data.BOMenuGia _MenuGia { get; set; }
        private List<Data.BOMenuGia> mListMenuLoaiGia;
        public WindowBanHangNhieuGia(List<Data.BOMenuGia> danhsach)
        {
            mListMenuLoaiGia = danhsach;                    
            InitializeComponent();            
        }
        private void LoadData()
        {
            double pading=10;
            double size = gridContent.RenderSize.Height-pading*2;            
            int maxCount = mListMenuLoaiGia.Count>4?4:mListMenuLoaiGia.Count;
            double x =(gridContent.RenderSize.Width- size*maxCount-pading*(maxCount-1))/2;
            for (int i = 0; i < maxCount; i++)
            {
                var item = mListMenuLoaiGia[i];
                ControlLibrary.POSButtonPrice btn = new ControlLibrary.POSButtonPrice();
                btn.Width = size;
                btn.Height = size;
                btn.Margin = new Thickness(x, pading, 0, 0);
                btn.Text = item.TenLoaiGia;
                btn.TextPrice = Utilities.MoneyFormat.ConvertToStringFull(item.Gia);
                btn.FontSize = 14;
                btn.FontSizePrice = 18;
                btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                btn.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                btn.Background = Brushes.White;
                btn.Click += new RoutedEventHandler(btn_Click);
                btn._MenuGia = item;
                gridContent.Children.Add(btn);
                x += size + pading;
            }            
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            ControlLibrary.POSButtonPrice btn = (ControlLibrary.POSButtonPrice)sender;
            _MenuGia = btn._MenuGia;
            this.DialogResult = true;
        }        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
