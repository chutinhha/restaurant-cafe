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

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowBaoCaoThongKe.xaml
    /// </summary>
    public partial class WindowBaoCaoThongKe : Window
    {
        private Data.Transit mTransit = null;
        public WindowBaoCaoThongKe(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.TenChucNang = "Báo cáo thống kê";
            uCTile.OnEventExit += new ControlLibrary.UCTile.OnExit(uCTile_OnEventExit);
            if (mTransit.NhanVien.NhanVienID != 0)
            {
                SetTagButton();
                PhanQuyen();
            }
        }

        void uCTile_OnEventExit()
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnLichSuDangNhap_Click(object sender, RoutedEventArgs e)
        {
            Report.LichSuDangNhap.WindowLichSuDangNhap win = new Report.LichSuDangNhap.WindowLichSuDangNhap(mTransit);
            win.ShowDialog();
        }

        private void btnBaoCaoLichSuBanHang_Click(object sender, RoutedEventArgs e)
        {
            Report.LichSuBanHang.WindowLichSuBanHang win = new Report.LichSuBanHang.WindowLichSuBanHang(mTransit);
            win.ShowDialog();
        }

        private void btnBaoCaoTonKho_Click(object sender, RoutedEventArgs e)
        {
            Report.BaoCaoTonKho.WindowBaoCaoTonKho win = new Report.BaoCaoTonKho.WindowBaoCaoTonKho(mTransit);
            win.ShowDialog();
        }

        private void btnBaoCaoDinhLuong_Click(object sender, RoutedEventArgs e)
        {
            Report.BaoCaoDinhLuong.WindowBaoCaoDinhLuong win = new Report.BaoCaoDinhLuong.WindowBaoCaoDinhLuong(mTransit);
            win.ShowDialog();
        }

        private void SetTagButton()
        {
            btnBaoCaoDinhLuong.Tag = Data.TypeChucNang.Baocao.BaoCaoDinhLuong;
            btnBaoCaoTonKho.Tag = Data.TypeChucNang.Baocao.BaoCaoTonKho;
            btnBaoCaoLichSuBanHang.Tag = Data.TypeChucNang.Baocao.BaoCaoLichSuBanHang;
            btnBaoCaoNgay.Tag = Data.TypeChucNang.Baocao.BaoCaoNgay;
            btnBaoCaoNhanVien.Tag = Data.TypeChucNang.Baocao.BaoCaoNhanVien;
        }

        private void PhanQuyen()
        {
            int i = 1, j = 0;
            foreach (var item in gridButtonMain.Children)
            {
                if (item is ControlLibrary.POSButtonMain)
                {
                    ControlLibrary.POSButtonMain btn = (ControlLibrary.POSButtonMain)item;
                    if (btn.Tag != null && btn.Tag is Data.TypeChucNang.Baocao)
                    {
                        Data.TypeChucNang.Baocao type = (Data.TypeChucNang.Baocao)btn.Tag;
                        if (type != Data.TypeChucNang.Baocao.None)
                        {
                            Data.BOChiTietQuyen ctq = mTransit.BOChiTietQuyen.KiemTraNhomChucNang((int)type);
                            btn.Tag = ctq;
                            if (mTransit.KiemTraNhomChucNang((int)type) == true)
                            {
                                if (j > gridButtonMain.ColumnDefinitions.Count - 1)
                                {
                                    i++;
                                    j = 0;
                                }
                                LookButton(btn, ctq.ChiTietQuyen.ChoPhep, i, j);
                                j++;
                            }
                            else
                                LookButton(btn, false, i, j);
                        }
                    }
                }
            }
        }

        private void LookButton(ControlLibrary.POSButtonMain btn, bool value, int row, int col)
        {
            if (value == true)
            {
                btn.Visibility = System.Windows.Visibility.Visible;
                Grid.SetRow(btn, row);
                Grid.SetColumn(btn, col);
            }
            else
            {
                btn.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void btnBaoCaoNgay_Click(object sender, RoutedEventArgs e)
        {
            Report.BaoCaoNgay.WindowBaoCaoNgay win = new Report.BaoCaoNgay.WindowBaoCaoNgay(mTransit);
            win.ShowDialog();
        }

        private void btnBaoCaoNhanVien_Click(object sender, RoutedEventArgs e)
        {
            Report.BaoCaoNhanVien.WindowBaoCaoNhanVien win = new Report.BaoCaoNhanVien.WindowBaoCaoNhanVien(mTransit);
            win.ShowDialog();
        }        

        private void btnLichSuInNhaBep_Click(object sender, RoutedEventArgs e)
        {
            Report.LichSuMayIn.WindowLichSuMayIn win = new Report.LichSuMayIn.WindowLichSuMayIn(mTransit);
            win.ShowDialog();
        }
    }
}
