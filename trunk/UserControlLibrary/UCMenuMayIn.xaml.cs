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
    /// Interaction logic for UCMenuMayIn.xaml
    /// </summary>
    public partial class UCMenuMayIn : UserControl
    {
        private Data.Transit mTransit = null;
        public UCMenuMayIn(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mMenuItemMayIn = (Data.MENUITEMMAYIN)((ListViewItem)lvData.SelectedItems[0]).Tag;
                Data.BOMenuItemMayIn.Xoa(mMenuItemMayIn.MayInID, mMenuItemMayIn.MonID);
                lbStatus.Text = "Xóa thành công thành công";
                mMenuItemMayIn = null;
                LoadDanhSachMayIn();
                LoadMayIn();
                SetValues();
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            mMenuItemMayIn = new Data.MENUITEMMAYIN();
            GetValues();
            Data.BOMenuItemMayIn.Them(mMenuItemMayIn);
            mMenuItemMayIn = null;
            LoadDanhSachMayIn();
            LoadMayIn();
            SetValues();
            lbStatus.Text = "Thêm thành công";
        }

        private void GetValues()
        {
            mMenuItemMayIn.MonID = _Mon.MonID;
            mMenuItemMayIn.MayInID = (int)cbbMayIn.SelectedValue;
            mMenuItemMayIn.Deleted = false;
            mMenuItemMayIn.Visual = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UCMenu.OnEventMenu += new ControlLibrary.UCMenu.EventMenu(UCMenu_OnEventMenu);
            UCMenu.Init();

        }

        private Data.MENUMON _Mon = null;
        void UCMenu_OnEventMenu(object ob)
        {
            if (ob is Data.MENUMON)
            {
                _Mon = (Data.MENUMON)ob;
                txtTenMon.Text = _Mon.TenDai;
                LoadDanhSachMayIn();
                LoadMayIn();
                mMenuItemMayIn = null;
                SetValues();
            }
        }

        private void LoadDanhSachMayIn()
        {
            lvData.Items.Clear();
            if (_Mon != null)
            {
                System.Collections.Generic.List<Data.MENUITEMMAYIN> lsArray = Data.BOMenuItemMayIn.GetAll(_Mon.MonID);
                foreach (Data.MENUITEMMAYIN item in lsArray)
                {
                    ListViewItem li = new ListViewItem();
                    li.Content = item;
                    li.Tag = item;
                    lvData.Items.Add(li);
                }
            }
        }

        private void LoadMayIn()
        {
            cbbMayIn.ItemsSource = Data.BOMayIn.GetAll(GetIDsMayIn());
            if (cbbMayIn.Items.Count > 0)
            {
                cbbMayIn.SelectedIndex = 0;
                btnThem.IsEnabled = true;
                cbbMayIn.IsEnabled = true;
            }
            else
            {
                btnThem.IsEnabled = false;
                cbbMayIn.IsEnabled = false;
            }
        }

        private Data.MENUITEMMAYIN mMenuItemMayIn = null;
        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mMenuItemMayIn = (Data.MENUITEMMAYIN)((ListViewItem)lvData.SelectedItems[0]).Tag;
                SetValues();
            }
        }
        private void SetValues()
        {
            if (mMenuItemMayIn != null)
            {
                cbbMayIn.SelectedValue = mMenuItemMayIn.MayInID;
            }
            else
            {
                if (cbbMayIn.Items.Count > 0)
                {
                    cbbMayIn.SelectedIndex = 0;
                }
            }
        }

        private int[] GetIDsMayIn()
        {
            if (lvData.Items.Count > 0)
            {
                int[] IDs = new int[lvData.Items.Count];
                int i = 0;
                foreach (ListViewItem li in lvData.Items)
                {
                    Data.MENUITEMMAYIN ktm = (Data.MENUITEMMAYIN)li.Tag;
                    IDs[i++] = (int)ktm.MayInID;
                }
                return IDs;
            }
            else
                return null;
        }
    }
}
