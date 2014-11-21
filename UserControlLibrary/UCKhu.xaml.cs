using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCKhu.xaml
    /// </summary>
    public partial class UCKhu : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.KHU mItem = null;
        private List<Data.KHU> lsArrayDeleted = null;
        private Data.BOKhu mBOKhu;
        public UCKhu(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            mBOKhu = new Data.BOKhu(mTransit);
        }

        private void LoadDanhSach()
        {
            mBOKhu.Refresh();
            var lsArray = Data.BOKhu.GetAll(mTransit);
            lvData.Items.Clear();
            foreach (var item in lsArray)
            {
                AddList(item);
            }
        }

        private void AddList(Data.KHU item)
        {
            ListViewItem li = new ListViewItem();
            li.Content = item;
            li.Tag = item;
            lvData.Items.Add(li);
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.KHU)li.Tag;
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemKhu win = new UserControlLibrary.WindowThemKhu(mTransit);
            if (win.ShowDialog() == true)
            {
                AddList(win._Item);
                mBOKhu.Them(win._Item);
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.KHU)li.Tag;

                UserControlLibrary.WindowThemKhu win = new UserControlLibrary.WindowThemKhu(mTransit);
                win._Item = mItem;
                if (win.ShowDialog() == true)
                {
                    win._Item.Edit = true;
                    li.Tag = win._Item;
                    li.Content = win._Item;
                    mBOKhu.Sua(win._Item);
                    lvData.Items.Refresh();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mItem = (Data.KHU)((ListViewItem)lvData.SelectedItems[0]).Tag;
                if (lsArrayDeleted == null)
                {
                    lsArrayDeleted = new List<Data.KHU>();
                }
                if (mItem.KhuID > 0)
                    mBOKhu.Xoa(mItem);
                lvData.Items.Remove(lvData.SelectedItems[0]);
                if (lvData.Items.Count > 0)
                {
                    lvData.SelectedIndex = 0;
                }
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            mBOKhu.Commit();
            lvData.Items.Refresh();
            UserControlLibrary.WindowMessageBox messageBox = new UserControlLibrary.WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            messageBox.ShowDialog();
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnLuu_Click(null, null);
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
                btnSua_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                btnXoa_Click(null, null);
                return;
            }
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            mItem = null;
            lsArrayDeleted = null;
            LoadDanhSach();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }
    }
}