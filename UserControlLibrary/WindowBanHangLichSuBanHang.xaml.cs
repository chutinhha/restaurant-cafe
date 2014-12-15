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
    /// Interaction logic for WindowBanHangLichSuBanHang.xaml
    /// </summary>
    public partial class WindowBanHangLichSuBanHang : Window
    {
        private Data.KaraokeEntities mKaraokeEntities;
        private PrinterServer.ProcessPrinter mProcessPrinter;
        private Data.Transit mTransit;
        public WindowBanHangLichSuBanHang(Data.Transit transit)
        {
            mTransit = transit;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mKaraokeEntities = new Data.KaraokeEntities();
            datePicker1.SelectedDate = DateTime.Now;
            mProcessPrinter = new PrinterServer.ProcessPrinter(mTransit);
            LoadData();
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            var list = Data.BOBanHang.GetAllCompleted(mKaraokeEntities,datePicker1.SelectedDate.Value);
            lvData1.ItemsSource = list;
            lvData1.Items.Refresh();
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void lvData1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData1.SelectedItem!=null)
            {
                Data.BOBanHang bh = (Data.BOBanHang)lvData1.SelectedItem;
                bh.LoadChiTiet();
                lvData2.ItemsSource = bh._ListChiTietBanHang;
            }
        }

        private void btnInLai_Click(object sender, RoutedEventArgs e)
        {
            if (lvData1.SelectedItem!=null)
            {
                Data.BOBanHang bh = (Data.BOBanHang)lvData1.SelectedItem;
                mProcessPrinter.InBill(PrinterServer.PrinterBillOrder.PrinterBillOrderType.InLai, bh.BANHANG.BanHangID);
                UserControlLibrary.WindowMessageBox.ShowDialog("Đã in thành công");
            }
            else
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Vui lòng chọn hóa đơn cần in lại");
            }
        }
    }
}
