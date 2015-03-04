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
    /// Interaction logic for UCChuyenKho.xaml
    /// </summary>
    public partial class UCChuyenKho : UserControl
    {
        private Data.Transit mTransit;
        private Data.KaraokeEntities mKaraokeEntities;
        private Data.BOChuyenKho mBOChuyenKho;
        public UCChuyenKho(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            mKaraokeEntities = new Data.KaraokeEntities();
            mBOChuyenKho = new Data.BOChuyenKho(mTransit,mKaraokeEntities);
            mBOChuyenKho.NhanVienID = mTransit.NhanVien.NhanVienID;
            dtpThoiGian.SelectedDate = DateTime.Now;            
        }

        private void LoadDanhSach()
        {
            lvData.ItemsSource = mBOChuyenKho.GetAllByDate(dtpThoiGian.SelectedDate.Value);
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowChuyenKho win = new UserControlLibrary.WindowChuyenKho(mTransit,mBOChuyenKho,mKaraokeEntities);
            win.ShowDialog();
            LoadDanhSach();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
                   
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {            
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {         
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                //btnLuu_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnThem_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnDanhSach_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.F2)
            {
                //btnSua_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                //btnXoa_Click(null, null);
                return;
            }
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {         
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }

        private void dtpThoiGian_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {            
               LoadDanhSach();
        }
    }
}
