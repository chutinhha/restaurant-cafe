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
    /// Interaction logic for WindowThongTinCongTy.xaml
    /// </summary>
    public partial class WindowThongTinCongTy : Window
    {
        private Data.Transit mTransit = null;
        public WindowThongTinCongTy(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCTile.TenChucNang = "Cài đặt thông tin";
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

        private void btnCaiDatThongTinCongTy_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatThongTinDoanhNghiep win = new UserControlLibrary.WindowCaiDatThongTinDoanhNghiep(mTransit);
            win.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnCaiDatGiaoDienBanHang_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatGiaoDienBanHang win = new UserControlLibrary.WindowCaiDatGiaoDienBanHang(mTransit);
            win.ShowDialog();
        }

        private void btnCaiDatChucNangHienThi_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatChucNangHienThi win = new UserControlLibrary.WindowCaiDatChucNangHienThi(mTransit);
            win.ShowDialog();
        }

        private void btnCaiDatBan_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatBan win = new UserControlLibrary.WindowCaiDatBan(mTransit);
            win.ShowDialog();
        }

        private void btnCaiDatMayInNhaBep_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatMayInNhaBep win = new UserControlLibrary.WindowCaiDatMayInNhaBep(mTransit);
            win.ShowDialog();
        }

        private void btnCaiDatMayInHoaDon_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatMayInHoaDon win = new UserControlLibrary.WindowCaiDatMayInHoaDon(mTransit);
            win.ShowDialog();
        }

        private void btnCaiDatThucDon_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatThucDon win = new UserControlLibrary.WindowCaiDatThucDon(mTransit);
            win.ShowDialog();
        }
        private void SetTagButton()
        {
            btnCaiDatThongTinCongTy.Tag = Data.TypeChucNang.CaiDat.CaiDatThongTinCongTy;
            btnCaiDatGiaoDienBanHang.Tag = Data.TypeChucNang.CaiDat.CaiDatGiaoDienBanHang;
            btnCaiDatChucNangHienThi.Tag = Data.TypeChucNang.CaiDat.btnCaiDatChucNangHienThi;
            btnCaiDatBan.Tag = Data.TypeChucNang.CaiDat.CaiDatBan;
            btnCaiDatMayInNhaBep.Tag = Data.TypeChucNang.CaiDat.CaiDatMayInNhaBep;
            btnCaiDatMayInHoaDon.Tag = Data.TypeChucNang.CaiDat.CaiDatMayInHoaDon;
            btnCaiDatThucDon.Tag = Data.TypeChucNang.CaiDat.CaiDatThucDon;
        }

        private void PhanQuyen()
        {
            int i = 1, j = 0;
            foreach (var item in gridButtonMain.Children)
            {
                if (item is ControlLibrary.POSButtonMain)
                {
                    ControlLibrary.POSButtonMain btn = (ControlLibrary.POSButtonMain)item;
                    if (btn.Tag != null && btn.Tag is Data.TypeChucNang.CaiDat)
                    {
                        Data.TypeChucNang.CaiDat type = (Data.TypeChucNang.CaiDat)btn.Tag;
                        if (type != Data.TypeChucNang.CaiDat.None)
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
                                if (type != Data.TypeChucNang.CaiDat.btnCaiDatChucNangHienThi)
                                    LookButton(btn, ctq.ChiTietQuyen.ChoPhep, i, j);
                                else
                                {
                                    j--;
                                    LookButton(btn, false, i, j);
                                }
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

        private void btnXuatNhapDuLieu_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowMenuExport win = new UserControlLibrary.WindowMenuExport(mTransit);
            win.ShowDialog();
        }

        private void btnCaiDatBanHang_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowCaiDatBanHang win = new UserControlLibrary.WindowCaiDatBanHang(mTransit);
            win.ShowDialog();
        }

    }
}
