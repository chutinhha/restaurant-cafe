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
    /// Interaction logic for UCThuChi.xaml
    /// </summary>
    public partial class UCThuChi : UserControl
    {
        private Data.Transit mTransit = null;
        private List<Data.BOThuChi> lsArray = null;
        private Data.BOThuChi BOThuChi = null;
        private PrinterServer.ProcessPrinter mProcessPrinter;
        public UCThuChi(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            mProcessPrinter = new PrinterServer.ProcessPrinter(mTransit);
            BOThuChi = new Data.BOThuChi(transit);
            PhanQuyen();
        }
        Data.BOChiTietQuyen mPhanQuyen = null;

        private void PhanQuyen()
        {
            mPhanQuyen = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.ThuChi.ThuChi);
            //if (!mPhanQuyen.ChiTietQuyen.ChoPhep)
            //    btnDanhSach.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Them)
            {
                btnThemPhieuThu.Visibility = System.Windows.Visibility.Collapsed;
                btnThemPhieuChi.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (!mPhanQuyen.ChiTietQuyen.Sua)
                btnSua.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Xoa)
                btnXoa.Visibility = System.Windows.Visibility.Collapsed;
            //if (!mPhanQuyen.ChiTietQuyen.Them && !mPhanQuyen.ChiTietQuyen.Xoa && !mPhanQuyen.ChiTietQuyen.Sua)
            //    btnLuu.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void LoadDanhSach()
        {
            lvData.ItemsSource = lsArray = BOThuChi.GetAll().ToList();
            lvData.Items.Refresh();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                Data.BOThuChi item = (Data.BOThuChi)lvData.SelectedItems[0];
                UserControlLibrary.WindowPhieuThuChi win = new UserControlLibrary.WindowPhieuThuChi(mTransit, BOThuChi, (int)item.ThuChi.LoaiThuChiID);
                win._Item = item;
                if (win.ShowDialog() == true)
                {
                    lvData.Items.Refresh();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                if (lvData.SelectedItems.Count > 0)
                {
                    Data.BOThuChi item = (Data.BOThuChi)lvData.SelectedItems[0];
                    if (item.ThuChi.LoaiThuChiID > 0)
                    {
                        item.ThuChi.Deleted = true;
                    }
                    lsArray.Remove(item);
                    lvData.Items.Refresh();
                }
            }
        }

        private void Luu()
        {
            BOThuChi.Luu(lsArray);
            LoadDanhSach();
            UserControlLibrary.WindowMessageBox messageBox = new UserControlLibrary.WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            messageBox.ShowDialog();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (mPhanQuyen.ChiTietQuyen.DangNhap)
            {
                UserControlLibrary.WindowLoginDialog loginWindow = new UserControlLibrary.WindowLoginDialog(mTransit);
                if (loginWindow.ShowDialog() == true)
                {
                    Luu();
                }
            }
            else
                Luu();
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((mPhanQuyen.ChiTietQuyen.Them || mPhanQuyen.ChiTietQuyen.Xoa || mPhanQuyen.ChiTietQuyen.Sua) && e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnLuu_Click(null, null);
                return;
            }
            //if (mPhanQuyen.ChiTietQuyen.Them && e.Key == System.Windows.Input.Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            //{
            //    btnThem_Click(null, null);
            //    return;
            //}
            if (mPhanQuyen.ChiTietQuyen.ChoPhep && e.Key == System.Windows.Input.Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnDanhSach_Click(null, null);
                return;
            }
            if (mPhanQuyen.ChiTietQuyen.Sua && e.Key == System.Windows.Input.Key.F2)
            {
                btnSua_Click(null, null);
                return;
            }
            //if (mPhanQuyen.ChiTietQuyen.Xoa && e.Key == System.Windows.Input.Key.Delete)
            //{
            //    btnXoa_Click(null, null);
            //    return;
            //}
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            BOThuChi.Refresh();
            LoadDanhSach();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }

        private void btnThemPhieuThu_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowPhieuThuChi win = new UserControlLibrary.WindowPhieuThuChi(mTransit, BOThuChi, 1);
            if (win.ShowDialog() == true)
            {
                //lsArray.Add(win._Item);
                //lvData.Items.Refresh();
                LoadDanhSach();
            }
        }

        private void btnThemPhieuChi_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowPhieuThuChi win = new UserControlLibrary.WindowPhieuThuChi(mTransit, BOThuChi, 2);
            if (win.ShowDialog() == true)
            {
                //lsArray.Add(win._Item);
                //lvData.Items.Refresh();
                LoadDanhSach();
            }
        }

        private void btnInPhieu_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItem!=null)
            {
                var item = lvData.SelectedItem as Data.BOThuChi;
                mProcessPrinter.InPhieuThuChi(item.ThuChi.ID);
            }
        }
    }
}
