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

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCNhapKho.xaml
    /// </summary>
    public partial class UCNhapKho : UserControl
    {
        private Data.Transit mTransit;
        private Data.KaraokeEntities mKaraokeEntities;

        public UCNhapKho(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            mKaraokeEntities = new Data.KaraokeEntities();
            dtpThoiGian.SelectedDate = DateTime.Now;            
        }

        private void LoadDanhSach()
        {            
            lvData.ItemsSource = Data.BONhapKho.GetAllByDate(mKaraokeEntities,dtpThoiGian.SelectedDate.Value);
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (lvData.SelectedItems.Count > 0)
            //{
            //    mItem = (Data.BONhapKho)lvData.SelectedItems[0];
            //}
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowNhapKho win = new UserControlLibrary.WindowNhapKho(mTransit);
            if (win.ShowDialog() == true)
            {
                LoadDanhSach();
            }
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {            
            if (e.Key == System.Windows.Input.Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnThem_Click(null, null);
                return;
            }                                    
        }
        

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }

        private void dtpThoiGian_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.ItemsSource != null)
                LoadDanhSach();
        }
    }
}
