using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCMayIn.xaml
    /// </summary>
    public partial class UCMayIn : UserControl
    {
        private Data.Transit mTransit = null;
        private List<Data.MAYIN> lsArray = null;
        private Data.BOMayIn BOMayIn = null;

        public UCMayIn(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            BOMayIn = new Data.BOMayIn(mTransit);
            PhanQuyen();
        }

        Data.BOChiTietQuyen mPhanQuyen = null;

        private void PhanQuyen()
        {
            mPhanQuyen = mTransit.BOChiTietQuyen.KiemTraQuyen((int)Data.TypeChucNang.MayIn.CaiDatMayIn);
            if (!mPhanQuyen.ChiTietQuyen.ChoPhep)
                btnDanhSach.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Them)
                btnThem.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Sua)
                btnSua.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Xoa)
                btnXoa.Visibility = System.Windows.Visibility.Collapsed;
            if (!mPhanQuyen.ChiTietQuyen.Them && !mPhanQuyen.ChiTietQuyen.Xoa && !mPhanQuyen.ChiTietQuyen.Sua)
                btnLuu.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void LoadDanhSach()
        {
            lvData.ItemsSource = lsArray = BOMayIn.GetAll().ToList();
            lvData.Items.Refresh();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemMayIn win = new UserControlLibrary.WindowThemMayIn(mTransit);
            if (win.ShowDialog() == true)
            {
                lsArray.Add(win._Item);
                lvData.Items.Refresh();
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                UserControlLibrary.WindowThemMayIn win = new UserControlLibrary.WindowThemMayIn(mTransit);
                win._Item = (Data.MAYIN)lvData.SelectedItems[0];
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
                    Data.MAYIN item = (Data.MAYIN)lvData.SelectedItems[0];
                    if (item.MayInID > 0)
                    {
                        item.Deleted = true;
                    }
                    lsArray.Remove(item);
                    lvData.Items.Refresh();
                }
            }
        }

        private void Luu()
        {
            BOMayIn.Luu(lsArray);
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
            if (mPhanQuyen.ChiTietQuyen.Them && e.Key == System.Windows.Input.Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnThem_Click(null, null);
                return;
            }
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
            if (mPhanQuyen.ChiTietQuyen.Xoa && e.Key == System.Windows.Input.Key.Delete)
            {
                btnXoa_Click(null, null);
                return;
            }
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            BOMayIn.Refresh();
            LoadDanhSach();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }
    }
}